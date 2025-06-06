using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace UltraView
{
    public partial class Proxy: Form
    {
        public Proxy()
        {
            InitializeComponent();
        }
        // khai bao biet mang chung day dung khai bao ham lung tung
        private List<IPEndPoint> serverList = new List<IPEndPoint>();
        private List<IPEndPoint> clientList = new List<IPEndPoint>();
        private int rrIndex = 0;
        public string GetIP()
        {
            // lan_check ,network_check
           
                string output = GetLocalIPv4(NetworkInterfaceType.Wireless80211);
                if (output == "")
                {
                    output = GetLocalIPv4(NetworkInterfaceType.Ethernet);
                }
                return output;
            
        }
        private string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }

        private IPEndPoint GetNextServer()
        {
            if (serverList.Count == 0) return null;
            var endpoint = serverList[rrIndex % serverList.Count];
            rrIndex++;
            return endpoint;
        }

        private void StartProxy()
        {
            string ip_px = GetIP();
            int port_px = 8888;
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Parse(ip_px), port_px);
                listener.Start();
                Invoke(new Action(() =>
                {
                    seen_px.AppendText($"Proxy server is listening on {ip_px}:{port_px}\n");
                }));

                Task.Run(() =>
                {
                    while (true)
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Task.Run(() => HandleClient(client));
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting proxy server: " + ex.Message);
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Nếu là đăng ký server (gửi IP:Port)
                string[] parts = received.Split(':');
                if (parts.Length == 2 && IPAddress.TryParse(parts[0], out var ip) && int.TryParse(parts[1], out var p))
                {
                    var endpoint = new IPEndPoint(ip, p);
                    if (!serverList.Contains(endpoint))
                    {
                        serverList.Add(endpoint);
                        Invoke(new Action(() =>
                        {
                            seen_px.AppendText($"Server registered: {endpoint}\n");
                        }));
                    }
                }

                // Nếu là client (không phải đăng ký server) thì chuyển tiếp
                ForwardToServer(client,buffer, bytesRead);
            }
            catch
            {
                try { client.Close(); } catch { }
            }
        }
        private void ForwardToServer(TcpClient client, byte[] firstBuffer = null, int firstBytes = 0)
        {
            IPEndPoint serverEP = null;
            TcpClient server = null;
            NetworkStream clientStream = client.GetStream();
            NetworkStream serverStream = null;

            byte[] buffer = firstBuffer ?? new byte[4096];
            int bufferLength = firstBytes;

            while (true) // vòng lặp thử server mới nếu thất bại
            {
                serverEP = GetNextServer();
                try
                {
                    server = new TcpClient();
                    server.Connect(serverEP.Address, serverEP.Port);
                    serverStream = server.GetStream();

                    Invoke(new Action(() =>
                    {
                        seen_px.AppendText($"Forwarding client to server {serverEP}\n");
                    }));

                    // Gửi dữ liệu đầu nếu có
                    if (buffer != null && bufferLength > 0)
                    {
                        serverStream.Write(buffer, 0, bufferLength);
                    }

                    var t1 = Task.Run(() => ForwardStream(clientStream, serverStream));
                    var t2 = Task.Run(() => ForwardStream(serverStream, clientStream));

                    Task.WaitAny(t1, t2); // chỉ cần 1 chiều ngắt là dừng
                }
                catch
                {
                    serverList.Remove(serverEP);
                    Invoke(new Action(() =>
                    {
                        seen_px.AppendText($"Server {serverEP} unreachable, trying next...\n");
                    }));
                    continue;
                }

                break; // nếu không lỗi thì thoát vòng lặp
            }
        }

        private void ForwardStream(NetworkStream from, NetworkStream to)
        {
            byte[] buffer = new byte[4096];
            int bytesRead;
            try
            {
                while ((bytesRead = from.Read(buffer, 0, buffer.Length)) > 0)
                {
                    to.Write(buffer, 0, bytesRead);
                }
            }
            catch
            {
              
            }
        }
        private void lisent_px_Click(object sender, EventArgs e)
        {
            StartProxy();
        }
    }
}
