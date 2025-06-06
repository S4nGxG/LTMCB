﻿using KAutoHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace UltraView
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            //txtMyIP.Text = GetIP();
            lbStatus.Text = "Welcome..";

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, btnStatusTab2.Width, btnStatusTab2.Height);
            btnStatusTab2.Region = new Region(path);
            btnStatusTab2.BackColor = Color.Yellow;
        }

        //Tab1_Open connect for other device
        #region Tab1_Open connect for other device
        //public static int RemoteScreenFormCount = 0;
        //Lấy ra IP của card mạng đang dùng
        public string GetIP()
        {
            // lan_check ,network_check
            if (lan_check.Checked)
            {
                string output = GetLocalIPv4(NetworkInterfaceType.Wireless80211);
                if (output == "")
                {
                    output = GetLocalIPv4(NetworkInterfaceType.Ethernet);
                }
                return output;
            }
            else if(network_check.Checked)
            {
                string output = GetPublicIP();
                return output;
            } 
            else
            {
                MessageBox.Show("Tick LAN or Network");
                return "";
            }
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
        // ip public
        public static string GetPublicIP()
        {
            try
            {
                using (var client = new WebClient())
                {
                    string ip = client.DownloadString("https://api.ipify.org").Trim();
                    return ip;
                }
            }
            catch
            {
                return "Cannot get public IP";
            }
        }


        //RemoteScreenForm rmtScrForm;
        //ChatForm chatForm;
        private List<RemoteScreenForm> rmtScrForm = new List<RemoteScreenForm>();
        private List<ChatForm> chatForms = new List<ChatForm>();

        private int GetAvailablePort()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 0); // dùng port 0
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        // Proxy
 
        public void NotifyProxy(string proxyIP, int proxyPort, string myIP, int myPort)
            {
                try
                {
                    using (TcpClient client = new TcpClient())
                    {
                        client.Connect(proxyIP, proxyPort);
                        string message = $"{myIP}:{myPort}";
                        byte[] data = Encoding.UTF8.GetBytes(message);
                        client.GetStream().Write(data, 0, data.Length);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error notifying proxy: " + ex.Message);
                }
            }

        private void btnOpenConnect_Click_1(object sender, EventArgs e)
        {
            if ((lan_check.Checked == false && network_check.Checked == false)
                || (lan_check.Checked == true && network_check.Checked == true))
            {
                MessageBox.Show("please check LAN or Network");
            }
            else
            {
                try
                {
                    txtMyIP.Text = GetIP();
                    // Tạo port ngẫu nhiên cho remote screen
                    int screenPort = GetAvailablePort();
                    txtMyPort.Text = screenPort.ToString(); // Cập nhật GUI

                    // Tạo port ngẫu nhiên khác cho chat (cách nhau 1 để dễ đoán)
                    int chatPort = GetAvailablePort();
                    while (chatPort == screenPort) // tránh trùng
                        chatPort = GetAvailablePort();

                    // Mở ChatForm
                    ChatForm newChat = new ChatForm(0, txtMyIP.Text, chatPort);
                    newChat.Show();
                    chatForms.Add(newChat);

                    // Mở RemoteScreenForm
                    RemoteScreenForm newRemote = new RemoteScreenForm(screenPort);
                    newRemote.Show();
                    rmtScrForm.Add(newRemote);

                    newRemote.OnClosedPortCallback = (closedPort) =>
                    {
                        foreach (ListViewItem item in lvServers.Items)
                        {
                            if (item.SubItems[1].Text == closedPort.ToString())
                            {
                                item.SubItems[2].Text = "Closed";
                                break;
                            }
                        }
                    };
                    // Lưu server vào danh sách UI
                    ListViewItem newItem = new ListViewItem(txtMyIP.Text);
                    newItem.SubItems.Add(screenPort.ToString());
                    newItem.SubItems.Add("Running");
                    lvServers.Items.Add(newItem);
                    // gửi ip port cho proxy
                    string ip_p = txtMyIP.Text;
                    int port = int.Parse(txtMyPort.Text);
                    int port_p = 8888;
                    NotifyProxy(ip_p,port_p ,ip_p, port);

                    lbStatus.Text = $"Open success! Screen port: {screenPort}, Chat port: {chatPort}";
                    Writelogfile($"Open connect ports: {screenPort} / {chatPort} at {DateTime.Now.ToShortTimeString()}");
                }
                catch (Exception ex)
                {
                    lbStatus.Text = "Error opening server: " + ex.Message;
                    Writelogfile("Open server failed: " + ex.Message);
                }
            }

            
        }

        #endregion

        //Tab2_Connect to other device
        #region Tab2_Connect to other device_Có sửa đổi

        //private TcpClient client;
        //private NetworkStream ostream;
        private List<TcpClient> clients = new List<TcpClient>();
        private readonly object clientsLock = new object();
        private int portNumber;
        private int width;
        private int height;
        //Get screen size
        public Size GetDpiSafeResolution()
        {
            using (Graphics graphics = this.CreateGraphics())
            {
                return new Size((Screen.PrimaryScreen.Bounds.Width * (int)graphics.DpiX) / 96
                  , (Screen.PrimaryScreen.Bounds.Height * (int)graphics.DpiY) / 96);
            }
        }
        private Image CaptureScreen()
        {
            height = (int)(GetDpiSafeResolution().Height*getScalingFactor());//Đã sửa
            width = (int)(GetDpiSafeResolution().Width*getScalingFactor());//Đã sửa
            Rectangle bounds = new Rectangle(0, 0, width, height);
            Bitmap screenShot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(screenShot);
            graphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            return screenShot;
        }
        //xem xet co xoa k
        private Image CaptureScreen(int width, int height)
        {
            Rectangle bounds = new Rectangle(0, 0, width, height);
            Bitmap screenShot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(screenShot);
            graphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            return screenShot;
        }

        #region Get Scaling of Screen
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,

            // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
        }


        private float getScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

            return ScreenScalingFactor; // 1.25 = 125%
        }

        #endregion

        private void SendDesktopImage()
        {
            bool anyConnected = false;

            lock (clientsLock)
            {
                foreach (var c in clients.ToList())
                {
                    if (c.Connected)
                    {
                        anyConnected = true;
                        break;
                    }
                    else
                    {
                        clients.Remove(c);
                    }
                }
            }

            if (!anyConnected)
                return;


            Image img;
            if (txtWidth2.Text != "" && txtHeight2.Text != "")
            {
                int w = int.Parse(txtWidth2.Text);
                int h = int.Parse(txtHeight2.Text);
                img = CaptureScreen(w, h);
            }
            else
            {
                img = CaptureScreen();
            }

            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                byte[] encrypted = AES_Helper.EncryptBytes(ms.ToArray());
                byte[] lengthPrefix = BitConverter.GetBytes(encrypted.Length); // 4 bytes đầu

                //ostream = client.GetStream();
                //ostream.Write(lengthPrefix, 0, lengthPrefix.Length);           // Gửi độ dài trước
                //ostream.Write(encrypted, 0, encrypted.Length);                 // Gửi dữ liệu ảnh
                lock (clientsLock)
                {
                    foreach (var c in clients.ToList())
                    {
                        try
                        {
                            NetworkStream ostream = c.GetStream();
                            ostream.Write(lengthPrefix, 0, lengthPrefix.Length);
                            ostream.Write(encrypted, 0, encrypted.Length);
                        }
                        catch
                        {
                            clients.Remove(c); // Xóa client nếu lỗi
                        }
                    }
                }

            }
        }



        private bool isConnected = false;
        ChatForm chatForm2;
        private void btnConnect2_Click(object sender, EventArgs e)
        {
            //client = new TcpClient();
            TcpClient newClient = new TcpClient();
            Writelogfile("Try Connect " + "IP: " + txtMyIP.Text + ", Port: " + txtMyPort.Text +" "+ DateTime.Now.ToShortTimeString());
            if (intLoaiNN == 2)
            {
                lbStatus.Text = "Trying connect..";
            }
            else if (intLoaiNN == 1)
            {
                lbStatus.Text = "Đang kết nối..";
            }
            
            try
            {
                portNumber = int.Parse(txtPort2.Text);
                //client.Connect(txtIP2.Text, portNumber);
                newClient.Connect(txtIP2.Text, portNumber);
                lock (clientsLock)
                {
                    clients.Add(newClient);
                    Thread receiveThread = new Thread(() => ReceiveText(newClient));
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                }
                chatForm2 = new ChatForm(1, txtIP2.Text, portNumber+1);
                chatForm2.Show();
                Writelogfile("Connected " + "IP: " + txtMyIP.Text + ", Port: " + txtMyPort.Text + " " + DateTime.Now.ToShortTimeString());
                if (intLoaiNN == 2)
                {
                    lbStatus.Text = "Connected!";
                }
                else if (intLoaiNN == 1)
                {   
                    lbStatus.Text = "Đã kết nối..";
                }
                isConnected = true;
                btnStatusTab2.BackColor = Color.LawnGreen;
                if (intLoaiNN==2)
                { 
                    MessageBox.Show("Connected!");

                }
                else if (intLoaiNN == 1)
                {
                    MessageBox.Show("Đã kết nối!");

                }
                
            }
            catch (Exception)
            {
                Writelogfile("Faild connect " + "IP: " + txtMyIP.Text + ", Port: " + txtMyPort.Text + " " + DateTime.Now.ToShortTimeString());
                if (intLoaiNN == 2)
                {
                    lbStatus.Text = "Failed to connect...";
                }
                else if (intLoaiNN == 1)
                {
                    lbStatus.Text = "Kết nối thất bại...";
                }
                isConnected = false;
                btnStatusTab2.BackColor = Color.Yellow;
            }

        }
        //Share screen 
        private void btnShareScreen2_Click(object sender, EventArgs e)
        {
            timeOut = 0;
            if (!isConnected)
            {
                btnStatusTab2.BackColor = Color.Yellow;
                lbStatus.Text = "No connection!";
                return;
            }
            
            if (txtWidth2.Text != "" || txtHeight2.Text != "")
            {
                try
                {
                    int.Parse(txtWidth2.Text);
                    int.Parse(txtHeight2.Text);
                }
                catch
                {
                    if (intLoaiNN == 2)
                    {
                        lbStatus.Text = "You should fill width and height text box or you can empty both.";
                    }
                    else if (intLoaiNN == 1)
                    {
                        lbStatus.Text = "Bạn nên điền vào cả 2 box height and width hoặc để trống cả 2";
                    }
                    
                    return;
                }
            }
            if (btnShareScreen2.Text.StartsWith("Share")||btnShareScreen2.Text.StartsWith("Chia"))//Nếu btn...Text có dạng "Share%" thì... 
            {
                timer1.Start();
                btnShareScreen2.Text = "Stop sharing";
                if (intLoaiNN == 2)
                {
                    lbStatus.Text = "Sharing screen...";
                }
                else if (intLoaiNN == 1)
                {
                    lbStatus.Text = "Đang truyền màn hình...";
                }

                lock (clientsLock)
                {
                    foreach (var c in clients.ToList())
                    {
                        StartListenText(c);
                    }
                }

                //Listen texxt
                Writelogfile("ShareScreen: " + "IP: " + txtMyIP.Text + ", Port: " + txtMyPort.Text + " " + DateTime.Now.ToShortTimeString());
                
            }
            else
            {
                timer1.Stop();
                btnShareScreen2.Text = "Share your screen";
                if (intLoaiNN == 2)
                {
                    lbStatus.Text = "Stopped share screen";
                }
                else if (intLoaiNN == 1)
                {
                    lbStatus.Text = "Dừng truyền màn hình...";
                }
                
                Writelogfile("StopShareScreen: " + "IP: " + txtMyIP.Text + ", Port: " + txtMyPort.Text + " " + DateTime.Now.ToShortTimeString());
                
                //StopListenText();//Stop listen
            }
        }
        //đếm giờ, cài tick của nó khoảng 40 là vừa 
        private int timeOut = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                bool anyConnected = false;

                lock (clientsLock)
                {
                    foreach (var c in clients.ToList())
                    {
                        if (c.Connected)
                        {
                            anyConnected = true;
                            break;
                        }
                        else
                        {
                            clients.Remove(c);
                        }
                    }
                }

                if (anyConnected)
                {
                    SendDesktopImage();
                }
                else
                {
                    timeOut++;
                    if (timeOut == 200)
                    {
                        try
                        {
                            chatForm2?.StopListening();
                            chatForm2?.Close();
                        }
                        catch { }
                        pauseAll();
                        MessageBox.Show("Connection has been lost!");
                    }
                }
            }
            catch { }
        }

        private void pauseAll()
        {
            btnShareScreen2.Text = "Share your screen";
            //client.Close();
            //client.Dispose();
            timer1.Stop();
            if (intLoaiNN == 2)
            {
                lbStatus.Text = "Stopped share screen";
            }
            else if (intLoaiNN == 1)
            {
                lbStatus.Text = "Dừng truyền màn hình...";
            }
            isConnected = false;
            btnStatusTab2.BackColor = Color.Yellow;
            Writelogfile("StopShareScreen: " + "IP: " + txtMyIP.Text + ", Port: " + txtMyPort.Text + " " + DateTime.Now.ToShortTimeString());
            lock (clientsLock)
            {
                foreach (var c in clients)
                {
                    try { c.Close(); } catch { }
                }
                clients.Clear();
            }

        }

        #endregion

        #region Tuan_Menu_and_Ghilog




        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Writelogfile("btnExitClick" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
            if (intLoaiNN == 2)
            {
                lbStatus.Text = "Exit..";
            }
            else if (intLoaiNN == 1)
            {
                lbStatus.Text = "Thoát...";
            }
            Application.Exit();
        }

        //string logName = @"Log " + "Month " + DateTime.Now.Month.ToString() + ".txt";
        string logName = @"log.txt";
        int intLoaiNN = 0;//1 là Việt Nam 2 là English
        private void MainForm_Load(object sender, EventArgs e)
        {
            //Đọc file loại ngôn ngữ
            try
            {
                StreamReader reader = new StreamReader("NumberLang.txt");
                string loaiNN = reader.ReadToEnd();
                reader.Close();
                if (loaiNN == "1")
                {
                    intLoaiNN = 1;
                    Vietnames_Click(Vietnames, null);
                }
                    
                if (loaiNN == "2")
                {
                    intLoaiNN = 2;
                    English_Click(English, null);
                }
                    
                
            }
            catch
            {
                MessageBox.Show("ERR: Language file has been lost");
            }
            //Ghi log
            if (!File.Exists(logName))
            {
                File.Create(logName).Close();
            }
            Writelogfile(" ");
            Writelogfile("*LOAD*"+DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());


        }
        private void Writelogfile(string txt)
        {
            using (FileStream fs = new FileStream(logName, FileMode.Append))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    writer.WriteLine(txt);
                }
            }
        }
        #endregion
        #region MultiLanguage
        private void Vietnames_Click(object sender, EventArgs e)
        {
            SetLanguage("vi-VN");
            StaticLanguage(1);
        }

        private void English_Click(object sender, EventArgs e)
        {
            SetLanguage("en-US");
            StaticLanguage(2);
        }

        private void SetLanguage(string cultureName)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture(cultureName);
            ResourceManager rm = new
                ResourceManager("UltraView.Lang.MyResource", typeof(MainForm).Assembly);
            btnConnect2.Text = rm.GetString("connect", culture);
            btnOpenConnect.Text = rm.GetString("openconnect", culture);
            btnShareScreen2.Text = rm.GetString("sharescreen", culture);
            label2.Text = rm.GetString("cntoother", culture);
            label1.Text = rm.GetString("openconnect", culture);
            label5.Text = rm.GetString("waiting", culture);
            label14.Text = rm.GetString("fullscreen", culture);
            label3.Text = rm.GetString("myip", culture);
            label4.Text = rm.GetString("myport", culture);
            label6.Text = rm.GetString("partnerip", culture);
            label7.Text = rm.GetString("myport", culture);
            languagesToolStripMenuItem.Text = rm.GetString("language", culture);
            Vietnames.Text = rm.GetString("vietnamese", culture);
            English.Text = rm.GetString("english", culture);
            optionToolStripMenuItem.Text = rm.GetString("option", culture);
            exitToolStripMenuItem.Text = rm.GetString("exit", culture);


        }
        private void StaticLanguage(int LanguageNumber)
        {
            FileStream fs = new FileStream("NumberLang.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(LanguageNumber);
            sw.Close();
            fs.Close();
        }
        #endregion


        #region ReceiveClick and Keys
        private Thread ListeningToText;
        private NetworkStream instream;

        //import thu vien de xu ly click
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;

        private void StartListenText(TcpClient clientSocket)
        {
            ListeningToText = new Thread(() => ReceiveText(clientSocket));
            ListeningToText.IsBackground = true;
            ListeningToText.Start();
        }

        private void StopListenText()//btnShareScreen2_Click
        {
            if (ListeningToText != null && ListeningToText.IsAlive)
                ListeningToText.Abort();
        }

        private void ReceiveText(TcpClient clientSocket)
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            try
            {
                NetworkStream instream = clientSocket.GetStream();

                while (clientSocket.Connected)
                {
                    try
                    {
                        string str = (string)binFormatter.Deserialize(instream);
                        string[] strArr = str.Split(':');

                        if (!(strArr[0] == "KU" || strArr[0] == "KD"))
                        {
                            // Xử lý chuột
                            int x = (int)((width / getScalingFactor()) * Int32.Parse(strArr[1]) / Int32.Parse(strArr[3]));
                            int y = (int)((height / getScalingFactor()) * Int32.Parse(strArr[2]) / Int32.Parse(strArr[4]));

                            switch (strArr[0])
                            {
                                case "MM":
                                    Cursor.Position = new Point(x, y);
                                    break;
                                case "LD":
                                    Cursor.Position = new Point(x, y);
                                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                                    break;
                                case "LU":
                                    Cursor.Position = new Point(x, y);
                                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                    break;
                                case "RC":
                                    Cursor.Position = new Point(x, y);
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                    break;
                                case "MC":
                                    Cursor.Position = new Point(x, y);
                                    mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                                    break;
                                case "LC":
                                    Cursor.Position = new Point(x, y);
                                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                    break;
                                case "DL":
                                    Cursor.Position = new Point(x, y);
                                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                    break;
                                case "DR":
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                    break;
                            }
                        }
                        else
                        {
                            // Xử lý phím
                            if (strArr[0] == "KU")
                            {
                                AutoControl.SendKeyUp(getKeyCodeFromString(strArr[1]));
                            }
                            else if (strArr[0] == "KD")
                            {
                                AutoControl.SendKeyDown(getKeyCodeFromString(strArr[1]));
                            }
                        }
                    }
                    catch { /* lỗi trong 1 lần xử lý không ảnh hưởng vòng lặp */ }
                }
            }
            catch
            {
                // mất kết nối, không cần báo lỗi
            }
        }


        private KeyCode getKeyCodeFromString(string keystr)
        {
            switch (keystr)
            {
                case "3": { return KeyCode.CANCEL; }
                case "8": { return KeyCode.BACKSPACE; }
                case "9": { return KeyCode.TAB; }
                case "13": { return KeyCode.ENTER; }
                case "16": { return KeyCode.SHIFT; }
                case "17": { return KeyCode.CONTROL; }
                case "18": { return KeyCode.ALT; }
                case "20": { return KeyCode.CAPS_LOCK; }
                case "27": { return KeyCode.ESC; }
                case "32": { return KeyCode.SPACE_BAR; }
                case "33": { return KeyCode.PAGE_UP; }
                case "34": { return KeyCode.PAGEDOWN; }
                case "35": { return KeyCode.END; }
                case "36": { return KeyCode.HOME; }
                case "37": { return KeyCode.LEFT; }
                case "38": { return KeyCode.UP; }
                case "39": { return KeyCode.RIGHT; }
                case "40": { return KeyCode.DOWN; }
                case "44": { return KeyCode.SNAPSHOT; }
                case "45": { return KeyCode.INSERT; }
                case "46": { return KeyCode.DELETE; }
                case "48": { return KeyCode.KEY_0; }
                case "49": { return KeyCode.KEY_1; }
                case "50": { return KeyCode.KEY_2; }
                case "51": { return KeyCode.KEY_3; }
                case "52": { return KeyCode.KEY_4; }
                case "53": { return KeyCode.KEY_5; }
                case "54": { return KeyCode.KEY_6; }
                case "55": { return KeyCode.KEY_7; }
                case "56": { return KeyCode.KEY_8; }
                case "57": { return KeyCode.KEY_9; }
                case "65": { return KeyCode.KEY_A; }
                case "66": { return KeyCode.KEY_B; }
                case "67": { return KeyCode.KEY_C; }
                case "68": { return KeyCode.KEY_D; }
                case "69": { return KeyCode.KEY_E; }
                case "70": { return KeyCode.KEY_F; }
                case "71": { return KeyCode.KEY_G; }
                case "72": { return KeyCode.KEY_H; }
                case "73": { return KeyCode.KEY_I; }
                case "74": { return KeyCode.KEY_J; }
                case "75": { return KeyCode.KEY_K; }
                case "76": { return KeyCode.KEY_L; }
                case "77": { return KeyCode.KEY_M; }
                case "78": { return KeyCode.KEY_N; }
                case "79": { return KeyCode.KEY_O; }
                case "80": { return KeyCode.KEY_P; }
                case "81": { return KeyCode.KEY_Q; }
                case "82": { return KeyCode.KEY_R; }
                case "83": { return KeyCode.KEY_S; }
                case "84": { return KeyCode.KEY_T; }
                case "85": { return KeyCode.KEY_U; }
                case "86": { return KeyCode.KEY_V; }
                case "87": { return KeyCode.KEY_W; }
                case "88": { return KeyCode.KEY_X; }
                case "89": { return KeyCode.KEY_Y; }
                case "90": { return KeyCode.KEY_Z; }
                case "91": { return KeyCode.LWIN; }
                case "92": { return KeyCode.RWIN; }
                case "93": { return KeyCode.RightClick; }
                case "96": { return KeyCode.NUMPAD0; }
                case "97": { return KeyCode.NUMPAD1; }
                case "98": { return KeyCode.NUMPAD2; }
                case "99": { return KeyCode.NUMPAD3; }
                case "100": { return KeyCode.NUMPAD4; }
                case "101": { return KeyCode.NUMPAD5; }
                case "102": { return KeyCode.NUMPAD6; }
                case "103": { return KeyCode.NUMPAD7; }
                case "104": { return KeyCode.NUMPAD8; }
                case "105": { return KeyCode.NUMPAD9; }
                case "106": { return KeyCode.MULTIPLY; }
                case "107": { return KeyCode.ADD; }
                case "109": { return KeyCode.SUBTRACT; }
                case "110": { return KeyCode.DECIMAL; }
                case "111": { return KeyCode.DIVIDE; }
                case "112": { return KeyCode.F1; }
                case "113": { return KeyCode.F2; }
                case "114": { return KeyCode.F3; }
                case "115": { return KeyCode.F4; }
                case "116": { return KeyCode.F5; }
                case "117": { return KeyCode.F6; }
                case "118": { return KeyCode.F7; }
                case "119": { return KeyCode.F8; }
                case "120": { return KeyCode.F9; }
                case "121": { return KeyCode.F10; }
                case "122": { return KeyCode.F11; }
                case "123": { return KeyCode.F12; }
                case "124": { return KeyCode.F13; }
                case "125": { return KeyCode.F14; }
                case "126": { return KeyCode.F15; }
                case "127": { return KeyCode.F16; }
                case "128": { return KeyCode.F17; }
                case "129": { return KeyCode.F18; }
                case "130": { return KeyCode.F19; }
                case "131": { return KeyCode.F20; }
                case "132": { return KeyCode.F21; }
                case "133": { return KeyCode.F22; }
                case "134": { return KeyCode.F23; }
                case "135": { return KeyCode.F24; }
                case "144": { return KeyCode.NUMLOCK; }
                case "160": { return KeyCode.LSHIFT; }
                case "161": { return KeyCode.RSHIFT; }
                case "162": { return KeyCode.LCONTROL; }
                case "163": { return KeyCode.RCONTROL; }
                case "166": { return KeyCode.BROWSER_BACK; }
                case "167": { return KeyCode.BROWSER_FORWARD; }
                case "168": { return KeyCode.BROWSER_REFRESH; }
                case "169": { return KeyCode.BROWSER_STOP; }
                case "170": { return KeyCode.BROWSER_SEARCH; }
                case "171": { return KeyCode.BROWSER_FAVORITES; }
                case "172": { return KeyCode.BROWSER_HOME; }
                case "173": { return KeyCode.VOLUME_MUTE; }
                case "174": { return KeyCode.VOLUME_DOWN; }
                case "175": { return KeyCode.VOLUME_UP; }
                case "176": { return KeyCode.MEDIA_NEXT_TRACK; }
                case "177": { return KeyCode.MEDIA_PREV_TRACK; }
                case "178": { return KeyCode.MEDIA_STOP; }
                case "179": { return KeyCode.MEDIA_PLAY_PAUSE; }
                case "180": { return KeyCode.LAUNCH_MAIL; }
                case "181": { return KeyCode.LAUNCH_MEDIA_SELECT; }
                case "182": { return KeyCode.LAUNCH_APP1; }
                case "183": { return KeyCode.LAUNCH_APP2; }
                case "186": { return KeyCode.OEM_1; }
                case "187": { return KeyCode.OEM_PLUS; }
                case "188": { return KeyCode.OEM_COMMA; }
                case "189": { return KeyCode.OEM_MINUS; }
                case "190": { return KeyCode.OEM_PERIOD; }
                case "191": { return KeyCode.OEM_2; }
                case "192": { return KeyCode.OEM_3; }
                case "219": { return KeyCode.OEM_4; }
                case "220": { return KeyCode.OEM_5; }
                case "221": { return KeyCode.OEM_6; }
                case "222": { return KeyCode.OEM_7; }
                case "223": { return KeyCode.OEM_8; }
                case "226": { return KeyCode.OEM_102; }
                case "254": { return KeyCode.OEM_CLEAR; }
            }
            return KeyCode.NUMLOCK;
        }
        #endregion

        //Chua lam
        #region End

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Writelogfile("BtnFormClose " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
            lbStatus.Text = "Close..";
            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                timer1.Stop();
                StopListenText();

                // Dừng và đóng tất cả RemoteScreenForm
                foreach (var remoteForm in rmtScrForm.ToList())
                {
                    try
                    {
                        remoteForm.StopListening();
                        remoteForm.Close();
                    }
                    catch { }
                }

                // Dừng và đóng tất cả ChatForm
                foreach (var chat in chatForms.ToList())
                {
                    try
                    {
                        chat.StopListening();
                        chat.Close();
                    }
                    catch { }
                }

                // Dọn danh sách TcpClient
                lock (clientsLock)
                {
                    foreach (var c in clients)
                    {
                        try { c.Close(); } catch { }
                    }
                    clients.Clear();
                }
            }
            catch { }
        }

        #endregion


        #region Ràng buộc nhập số trong textbox -Fix Bug 2
        private void txtMyIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.')
            {
                int count = 0;
                foreach (char c in (sender as TextBox).Text)
                {
                    if (c == '.')
                    {
                        count++;
                    }
                }
                if (count >= 3)
                    e.Handled = true;
            }
        }

        private void txtIP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtMyIP_KeyPress(sender, e);
        }

        private void txtMyPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPort2_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtMyPort_KeyPress(sender, e);
        }

        private void txtWidth2_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtMyPort_KeyPress(sender, e);
        }

        private void txtHeight2_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtMyPort_KeyPress(sender, e);
        }




        #endregion

        private void tabTypeConnect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

