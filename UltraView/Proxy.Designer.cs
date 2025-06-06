namespace UltraView
{
    partial class Proxy
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
            this.lisent_px = new System.Windows.Forms.Button();
            this.seen_px = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lisent_px
            // 
            this.lisent_px.Location = new System.Drawing.Point(41, 29);
            this.lisent_px.Name = "lisent_px";
            this.lisent_px.Size = new System.Drawing.Size(111, 42);
            this.lisent_px.TabIndex = 0;
            this.lisent_px.Text = "Listen";
            this.lisent_px.UseVisualStyleBackColor = true;
            this.lisent_px.Click += new System.EventHandler(this.lisent_px_Click);
            // 
            // seen_px
            // 
            this.seen_px.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.seen_px.Location = new System.Drawing.Point(41, 107);
            this.seen_px.Name = "seen_px";
            this.seen_px.Size = new System.Drawing.Size(708, 507);
            this.seen_px.TabIndex = 1;
            this.seen_px.Text = "";
            // 
            // Proxy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 663);
            this.Controls.Add(this.seen_px);
            this.Controls.Add(this.lisent_px);
            this.Name = "Proxy";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button lisent_px;
        private System.Windows.Forms.RichTextBox seen_px;
    }
}