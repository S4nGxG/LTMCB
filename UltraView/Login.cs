using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltraView
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (Database_Helper.CheckLogin(txt_loginUsername.Text, txt_loginPassword.Text))
            {
                MessageBox.Show("Login Successful!");
                this.Hide();
                new MainForm().Show();
                new Proxy().Show();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password.");
            }
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Register().ShowDialog();
        }
    }
}
