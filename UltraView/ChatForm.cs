using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace UltraView
{
    public partial class ChatForm : Form
    {
        //Chú ý: test trên visual& trên 1 máy sẽ không chạy được tính năng này đâu=> nên copy file debug của nó ra 2 cái rồi mở lên test
        //Ý tưởng: 
        //Loai 0: khi mở form này lên, nếu mở form này bằng btn OpenConnect bên main form thì sẽ là server, chờ kết nối từ phía client
        //Loai 1:Nếu mở bằng btn Connect thì sẽ là client và lấy các chỉ số ip, port đã nhập sẵn truyền vô sài!
        //Khai báo kết nối, có cả 2 cái

        private TcpClient client = null;
        private List<TcpClient> clients = new List<TcpClient>();

        TcpListener server;
        private readonly Thread Listening;
        private readonly Thread GetText;
        private int port; //đem cái port gửi hình +1 để ra cái port khác sài cho gọn đường
        private byte loai;
        private string ip;
        public ChatForm(byte Loai, string IP, int Port) //loai form= 0 server, 1 client
        {

            loai = Loai;
            port = Port;
            ip = IP;
            if (loai == 0) // server
            {
                Listening = new Thread(StartListening);
            }
            else // client
            {
                client = new TcpClient();
                GetText = new Thread(() => ReceiveText(client));
            }
            InitializeComponent();
            Writelogfile("OpenChatForm" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        }
        protected override void OnLoad(EventArgs e)
        {
            
            base.OnLoad(e);
            if(loai==0)
            {
                server = new TcpListener(IPAddress.Any, port);
                Listening.Start();
                
            }
            else
            {
                try
                {
                    client.Connect(ip, port);
                    GetText.Start();
                }
                catch (Exception)
                {
                }
            }
        }
        //Bắt kết nối và ngắt kết nối
        private void StartListening()
        {
            try
            {
                server.Start();
                while(true)
                {
                    TcpClient newClient = server.AcceptTcpClient();
                    clients.Add(newClient);

                    Thread receiveThread = new Thread(() => ReceiveText(newClient));
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                }
            }
            catch
            {
                // MessageBox.Show("Listening failed!");
                StopListening();
            }

        }
        public void StopListening()
        {
            try
            {
                if (loai == 0) // server
                {
                    foreach (var c in clients)
                    {
                        try { c.Close(); } catch { }
                    }
                    clients.Clear();
                    server?.Stop();
                }
                else if (loai == 1 && client != null)
                {
                    client.Close();
                    client = null;
                }
            }
            catch { }
            // MessageBox.Show("Disconnect success!");
        }

        //Nhận tin nhắn
        private NetworkStream istream;
        private void ReceiveText(TcpClient clientSocket)
        {
            try
            {
                NetworkStream istream = clientSocket.GetStream();

                while (clientSocket.Connected)
                {
                    byte[] lengthBuffer = new byte[4];
                    int readLen = istream.Read(lengthBuffer, 0, 4);
                    if (readLen < 4) continue;

                    int msgLen = BitConverter.ToInt32(lengthBuffer, 0);
                    byte[] encrypted = new byte[msgLen];

                    int totalRead = 0;
                    while (totalRead < msgLen)
                    {
                        int bytesRead = istream.Read(encrypted, totalRead, msgLen - totalRead);
                        if (bytesRead == 0) break;
                        totalRead += bytesRead;
                    }

                    byte[] decrypted = AES_Helper.DecryptBytes(encrypted);
                    string str = AES_Helper.BytesToString(decrypted);

                    if (str.StartsWith("MS:"))
                    {
                        string msg = str.Substring(3);
                        tbxShowMessage.Invoke((MethodInvoker)(() =>
                        {
                            tbxShowMessage.AppendText("\nThey: " + msg);
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ReceiveText error: " + ex.Message);
            }
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            StopListening();
            Writelogfile("FormChatClose" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
        }
        
        //Gửi tin nhắn
        private NetworkStream stream;
        
        private void btnSend_Click_1(object sender, EventArgs e)
        {
            if (tbxMessage.Text != "")
            {
                try
                {
                    sendText("MS:" + tbxMessage.Text);
                }
                catch
                {
                    tbxShowMessage.Text += "\nTin nhắn không gửi được!";
                    return;
                }
             
                tbxShowMessage.Text += "\nMe: " + tbxMessage.Text;
                
                tbxMessage.Clear();
                tbxMessage.Focus();
            }

        }
        private void sendText(string str)
        {
            byte[] rawBytes = AES_Helper.StringToBytes(str);
            byte[] encrypted = AES_Helper.EncryptBytes(rawBytes);
            byte[] lengthPrefix = BitConverter.GetBytes(encrypted.Length);

            if (loai == 0) // server: gửi cho tất cả client
            {
                foreach (var c in clients.ToList())
                {
                    try
                    {
                        NetworkStream stream = c.GetStream();
                        stream.Write(lengthPrefix, 0, lengthPrefix.Length);
                        stream.Write(encrypted, 0, encrypted.Length);
                    }
                    catch
                    {
                        clients.Remove(c);
                    }
                }
            }
            else if (loai == 1 && client != null && client.Connected) // client: gửi đến server
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(lengthPrefix, 0, lengthPrefix.Length);
                    stream.Write(encrypted, 0, encrypted.Length);
                }
                catch
                {
                    MessageBox.Show("Không gửi được tin nhắn.");
                }
            }
        }



        //WriteLog
        private void Writelogfile(string txt)
        {
            using (FileStream fs = new FileStream(@"log.txt", FileMode.Append))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    writer.WriteLine(txt);
                }
            }
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            tbxMessage.Focus();
            tbxShowMessage.SelectionColor = Color.Blue;
            

        }
    }
}



