namespace UltraView
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabTypeConnect = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.network_check = new System.Windows.Forms.CheckBox();
            this.lan_check = new System.Windows.Forms.CheckBox();
            this.btnStatusTab2 = new System.Windows.Forms.Button();
            this.txtHeight2 = new System.Windows.Forms.TextBox();
            this.txtWidth2 = new System.Windows.Forms.TextBox();
            this.txtPort2 = new System.Windows.Forms.TextBox();
            this.txtIP2 = new System.Windows.Forms.TextBox();
            this.txtMyIP = new System.Windows.Forms.TextBox();
            this.txtMyPort = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShareScreen2 = new System.Windows.Forms.Button();
            this.btnConnect2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOpenConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Vietnames = new System.Windows.Forms.ToolStripMenuItem();
            this.English = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.process1 = new System.Diagnostics.Process();
            this.lvServers = new System.Windows.Forms.ListView();
            this.columnIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabTypeConnect.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabTypeConnect
            // 
            this.tabTypeConnect.Controls.Add(this.tabPage1);
            this.tabTypeConnect.Location = new System.Drawing.Point(312, 0);
            this.tabTypeConnect.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabTypeConnect.Name = "tabTypeConnect";
            this.tabTypeConnect.SelectedIndex = 0;
            this.tabTypeConnect.Size = new System.Drawing.Size(969, 563);
            this.tabTypeConnect.TabIndex = 2;
            this.tabTypeConnect.SelectedIndexChanged += new System.EventHandler(this.tabTypeConnect_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.network_check);
            this.tabPage1.Controls.Add(this.lan_check);
            this.tabPage1.Controls.Add(this.btnStatusTab2);
            this.tabPage1.Controls.Add(this.txtHeight2);
            this.tabPage1.Controls.Add(this.txtWidth2);
            this.tabPage1.Controls.Add(this.txtPort2);
            this.tabPage1.Controls.Add(this.txtIP2);
            this.tabPage1.Controls.Add(this.txtMyIP);
            this.tabPage1.Controls.Add(this.txtMyPort);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnShareScreen2);
            this.tabPage1.Controls.Add(this.btnConnect2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.btnOpenConnect);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPage1.Size = new System.Drawing.Size(961, 530);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Open Connect";
            // 
            // network_check
            // 
            this.network_check.AutoSize = true;
            this.network_check.Location = new System.Drawing.Point(254, 321);
            this.network_check.Name = "network_check";
            this.network_check.Size = new System.Drawing.Size(93, 24);
            this.network_check.TabIndex = 25;
            this.network_check.Text = "Network";
            this.network_check.UseVisualStyleBackColor = true;
            // 
            // lan_check
            // 
            this.lan_check.AutoSize = true;
            this.lan_check.Location = new System.Drawing.Point(111, 321);
            this.lan_check.Name = "lan_check";
            this.lan_check.Size = new System.Drawing.Size(66, 24);
            this.lan_check.TabIndex = 24;
            this.lan_check.Text = "LAN";
            this.lan_check.UseVisualStyleBackColor = true;
            // 
            // btnStatusTab2
            // 
            this.btnStatusTab2.BackColor = System.Drawing.Color.LawnGreen;
            this.btnStatusTab2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatusTab2.ForeColor = System.Drawing.Color.Lime;
            this.btnStatusTab2.Location = new System.Drawing.Point(765, 384);
            this.btnStatusTab2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStatusTab2.Name = "btnStatusTab2";
            this.btnStatusTab2.Size = new System.Drawing.Size(28, 31);
            this.btnStatusTab2.TabIndex = 23;
            this.btnStatusTab2.UseVisualStyleBackColor = false;
            // 
            // txtHeight2
            // 
            this.txtHeight2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtHeight2.Location = new System.Drawing.Point(742, 133);
            this.txtHeight2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtHeight2.Name = "txtHeight2";
            this.txtHeight2.Size = new System.Drawing.Size(68, 26);
            this.txtHeight2.TabIndex = 21;
            this.txtHeight2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHeight2_KeyPress);
            // 
            // txtWidth2
            // 
            this.txtWidth2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtWidth2.Location = new System.Drawing.Point(580, 133);
            this.txtWidth2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtWidth2.Name = "txtWidth2";
            this.txtWidth2.Size = new System.Drawing.Size(68, 26);
            this.txtWidth2.TabIndex = 19;
            this.txtWidth2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWidth2_KeyPress);
            // 
            // txtPort2
            // 
            this.txtPort2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtPort2.Location = new System.Drawing.Point(544, 258);
            this.txtPort2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtPort2.Name = "txtPort2";
            this.txtPort2.Size = new System.Drawing.Size(266, 26);
            this.txtPort2.TabIndex = 16;
            this.txtPort2.Text = "8888";
            this.txtPort2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort2_KeyPress);
            // 
            // txtIP2
            // 
            this.txtIP2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtIP2.Location = new System.Drawing.Point(544, 189);
            this.txtIP2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtIP2.Name = "txtIP2";
            this.txtIP2.Size = new System.Drawing.Size(266, 26);
            this.txtIP2.TabIndex = 14;
            this.txtIP2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIP2_KeyPress);
            // 
            // txtMyIP
            // 
            this.txtMyIP.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMyIP.Location = new System.Drawing.Point(134, 186);
            this.txtMyIP.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtMyIP.Name = "txtMyIP";
            this.txtMyIP.ReadOnly = true;
            this.txtMyIP.Size = new System.Drawing.Size(205, 26);
            this.txtMyIP.TabIndex = 1;
            this.txtMyIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMyIP_KeyPress);
            // 
            // txtMyPort
            // 
            this.txtMyPort.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMyPort.Location = new System.Drawing.Point(134, 258);
            this.txtMyPort.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtMyPort.Name = "txtMyPort";
            this.txtMyPort.Size = new System.Drawing.Size(205, 26);
            this.txtMyPort.TabIndex = 4;
            this.txtMyPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMyPort_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label9.Location = new System.Drawing.Point(680, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 20);
            this.label9.TabIndex = 20;
            this.label9.Text = "Height";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label8.Location = new System.Drawing.Point(513, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 20);
            this.label8.TabIndex = 18;
            this.label8.Text = "Width";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(447, 82);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(467, 25);
            this.label14.TabIndex = 17;
            this.label14.Text = "If you can\'t see full screen please type your screen....";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(442, 262);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 20);
            this.label7.TabIndex = 15;
            this.label7.Text = "Port:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(442, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Partner IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(572, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 32);
            this.label2.TabIndex = 12;
            this.label2.Text = "Connect To Other";
            // 
            // btnShareScreen2
            // 
            this.btnShareScreen2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnShareScreen2.Location = new System.Drawing.Point(580, 313);
            this.btnShareScreen2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnShareScreen2.Name = "btnShareScreen2";
            this.btnShareScreen2.Size = new System.Drawing.Size(225, 38);
            this.btnShareScreen2.TabIndex = 11;
            this.btnShareScreen2.Text = "Share your screen";
            this.btnShareScreen2.UseVisualStyleBackColor = false;
            this.btnShareScreen2.Click += new System.EventHandler(this.btnShareScreen2_Click);
            // 
            // btnConnect2
            // 
            this.btnConnect2.AutoSize = true;
            this.btnConnect2.BackColor = System.Drawing.Color.Gainsboro;
            this.btnConnect2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnConnect2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect2.Location = new System.Drawing.Point(595, 364);
            this.btnConnect2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnConnect2.Name = "btnConnect2";
            this.btnConnect2.Size = new System.Drawing.Size(210, 65);
            this.btnConnect2.TabIndex = 10;
            this.btnConnect2.Text = "CONNECT";
            this.btnConnect2.UseVisualStyleBackColor = false;
            this.btnConnect2.Click += new System.EventHandler(this.btnConnect2_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(380, 55);
            this.label5.TabIndex = 5;
            this.label5.Text = "Waiting other device connect and share screen to your device.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOpenConnect
            // 
            this.btnOpenConnect.BackColor = System.Drawing.Color.Gainsboro;
            this.btnOpenConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpenConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenConnect.Location = new System.Drawing.Point(133, 367);
            this.btnOpenConnect.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnOpenConnect.Name = "btnOpenConnect";
            this.btnOpenConnect.Size = new System.Drawing.Size(206, 62);
            this.btnOpenConnect.TabIndex = 6;
            this.btnOpenConnect.Text = "OPEN CONNECT";
            this.btnOpenConnect.UseVisualStyleBackColor = false;
            this.btnOpenConnect.Click += new System.EventHandler(this.btnOpenConnect_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(128, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Open Connect";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(48, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "My IP:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(48, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "My Port:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1282, 33);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languagesToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(84, 29);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // languagesToolStripMenuItem
            // 
            this.languagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Vietnames,
            this.English});
            this.languagesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("languagesToolStripMenuItem.Image")));
            this.languagesToolStripMenuItem.Name = "languagesToolStripMenuItem";
            this.languagesToolStripMenuItem.Size = new System.Drawing.Size(191, 34);
            this.languagesToolStripMenuItem.Text = "Language";
            this.languagesToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Vietnames
            // 
            this.Vietnames.Name = "Vietnames";
            this.Vietnames.Size = new System.Drawing.Size(192, 34);
            this.Vietnames.Text = "Tiếng Việt";
            this.Vietnames.Click += new System.EventHandler(this.Vietnames_Click);
            // 
            // English
            // 
            this.English.Name = "English";
            this.English.Size = new System.Drawing.Size(192, 34);
            this.English.Text = "English";
            this.English.Click += new System.EventHandler(this.English_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(191, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 562);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1282, 32);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbStatus
            // 
            this.lbStatus.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbStatus.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(60, 25);
            this.lbStatus.Text = "Status";
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // lvServers
            // 
            this.lvServers.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lvServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnIP,
            this.columnPort,
            this.columnStatus});
            this.lvServers.HideSelection = false;
            this.lvServers.Location = new System.Drawing.Point(0, 31);
            this.lvServers.Name = "lvServers";
            this.lvServers.Size = new System.Drawing.Size(315, 528);
            this.lvServers.TabIndex = 6;
            this.lvServers.UseCompatibleStateImageBehavior = false;
            this.lvServers.View = System.Windows.Forms.View.Details;
            // 
            // columnIP
            // 
            this.columnIP.Text = "IP";
            this.columnIP.Width = 103;
            // 
            // columnPort
            // 
            this.columnPort.Text = "Port";
            this.columnPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnPort.Width = 67;
            // 
            // columnStatus
            // 
            this.columnStatus.Text = "Status";
            this.columnStatus.Width = 175;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1282, 594);
            this.Controls.Add(this.lvServers);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabTypeConnect);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = " UltraView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabTypeConnect.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabTypeConnect;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Vietnames;
        private System.Windows.Forms.ToolStripMenuItem English;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnStatusTab2;
        private System.Windows.Forms.TextBox txtHeight2;
        private System.Windows.Forms.TextBox txtWidth2;
        private System.Windows.Forms.TextBox txtPort2;
        private System.Windows.Forms.TextBox txtIP2;
        private System.Windows.Forms.TextBox txtMyIP;
        private System.Windows.Forms.TextBox txtMyPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnShareScreen2;
        private System.Windows.Forms.Button btnConnect2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOpenConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Diagnostics.Process process1;
        private System.Windows.Forms.ListView lvServers;
        private System.Windows.Forms.ColumnHeader columnIP;
        private System.Windows.Forms.ColumnHeader columnPort;
        private System.Windows.Forms.ColumnHeader columnStatus;
        private System.Windows.Forms.CheckBox network_check;
        private System.Windows.Forms.CheckBox lan_check;
    }
}