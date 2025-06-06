using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace UltraView
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            if (Database_Helper.RegisterUser(txt_registerUsername.Text, txt_registerPassword.Text))
            {
                MessageBox.Show("Registration Successful! You can now log in.");
                txt_registerUsername.Clear();
                txt_registerPassword.Clear();
                this.Hide();
                new MainForm().Show();
            }
            else
            {
                MessageBox.Show("Registration Failed. Username may already exist.");
            }
        }
    }
}
