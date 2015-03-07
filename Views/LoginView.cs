using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Views
{
    public partial class LoginView : Form
    {
        public LoginView()
        {
            InitializeComponent();
            this.btnLogin.Click += btnLogin_Click;
            this.btnExit.Click += btnExit_Click;
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            if (btnExitClick != null)
                btnExitClick(sender, e);
        }


        private string _LoginName = "";
        public string LoginName
        {
            get
            {
                _LoginName = txtUserName.Text;
                return _LoginName;
            }
            set
            {
                _LoginName = value;
                txtUserName.Text = _LoginName;
            }
        }


        private string _LoginPassword = "";

        public string LoginPassword
        {
            get
            {
                _LoginPassword = txtPassword.Text;
                return _LoginPassword;
            }
            set
            {
                _LoginPassword = value;
                txtPassword.Text = _LoginPassword;
            }
        }



        public void ShowMessageBox(string strMessage)
        {
            MessageBox.Show(strMessage);

        }




        public EventHandler btnLoginClick;




        void btnLogin_Click(object sender, EventArgs e)
        {

            if (btnLoginClick != null)
                btnLoginClick(sender, e);
        }




        public EventHandler btnExitClick;




        



    }
}
