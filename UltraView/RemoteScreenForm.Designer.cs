namespace UltraView
{
    partial class RemoteScreenForm
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
            this.picShowScreen = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbMouseMove = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbxMouse = new System.Windows.Forms.CheckBox();
            this.cbxKeyBoard = new System.Windows.Forms.CheckBox();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picShowScreen)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picShowScreen
            // 
            this.picShowScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picShowScreen.Location = new System.Drawing.Point(0, 0);
            this.picShowScreen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picShowScreen.Name = "picShowScreen";
            this.picShowScreen.Size = new System.Drawing.Size(1037, 638);
            this.picShowScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picShowScreen.TabIndex = 0;
            this.picShowScreen.TabStop = false;
            this.picShowScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picShowScreen_MouseClick);
            this.picShowScreen.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picShowScreen_MouseDoubleClick);
            this.picShowScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picShowScreen_MouseDown);
            this.picShowScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picShowScreen_MouseMove);
            this.picShowScreen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picShowScreen_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbSize,
            this.lbMouseMove,
            this.lbPort,
            this.toolStripStatusLabel3,
            this.lbStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 638);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1037, 42);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbSize
            // 
            this.lbSize.Name = "lbSize";
            this.lbSize.Size = new System.Drawing.Size(57, 32);
            this.lbSize.Text = "Size";
            // 
            // lbMouseMove
            // 
            this.lbMouseMove.Name = "lbMouseMove";
            this.lbMouseMove.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbMouseMove.Size = new System.Drawing.Size(68, 32);
            this.lbMouseMove.Text = "Point";
            // 
            // lbStatus
            // 
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(83, 32);
            this.lbStatus.Text = "Status:";
            // 
            // lbPort
            // 
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(56, 32);
            this.lbPort.Text = "Port";
            // 
            // cbxMouse
            // 
            this.cbxMouse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxMouse.AutoSize = true;
            this.cbxMouse.Location = new System.Drawing.Point(613, 651);
            this.cbxMouse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxMouse.Name = "cbxMouse";
            this.cbxMouse.Size = new System.Drawing.Size(181, 29);
            this.cbxMouse.TabIndex = 6;
            this.cbxMouse.Text = "Mouse remote";
            this.cbxMouse.UseVisualStyleBackColor = true;
            this.cbxMouse.CheckedChanged += new System.EventHandler(this.cbxMouse_CheckedChanged);
            // 
            // cbxKeyBoard
            // 
            this.cbxKeyBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxKeyBoard.AutoSize = true;
            this.cbxKeyBoard.Location = new System.Drawing.Point(801, 651);
            this.cbxKeyBoard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxKeyBoard.Name = "cbxKeyBoard";
            this.cbxKeyBoard.Size = new System.Drawing.Size(208, 29);
            this.cbxKeyBoard.TabIndex = 7;
            this.cbxKeyBoard.Text = "Keyboard remote";
            this.cbxKeyBoard.UseVisualStyleBackColor = true;
            this.cbxKeyBoard.CheckedChanged += new System.EventHandler(this.cbxKeyBoard_CheckedChanged);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(35, 32);
            this.toolStripStatusLabel3.Text = "   ";
            // 
            // RemoteScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(1037, 680);
            this.Controls.Add(this.cbxKeyBoard);
            this.Controls.Add(this.cbxMouse);
            this.Controls.Add(this.picShowScreen);
            this.Controls.Add(this.statusStrip1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RemoteScreenForm";
            this.Text = "RemoteScreen";
            this.SizeChanged += new System.EventHandler(this.RemoteScreenForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RemoteScreenForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RemoteScreenForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.picShowScreen)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picShowScreen;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbSize;
        private System.Windows.Forms.ToolStripStatusLabel lbMouseMove;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.CheckBox cbxMouse;
        private System.Windows.Forms.CheckBox cbxKeyBoard;
        private System.Windows.Forms.ToolStripStatusLabel lbPort;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
    }
}

