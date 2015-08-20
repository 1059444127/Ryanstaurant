using System;
using System.ServiceModel;
using System.Windows.Forms;
using Ryanstaurant.UMS.Service;

namespace Ryanstaurant.Server
{
    public partial class UMS : Form
    {
        public UMS()
        {
            InitializeComponent();
        }

        readonly ServiceHost _host = new ServiceHost(typeof(UMSService));
        private void button1_Click(object sender, EventArgs e)
        {
            if (_host.State == CommunicationState.Closed)
                _host.Open();

            textBox1.Text += string.Format("[{0}]Server Started!\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_host.State != CommunicationState.Closed)
            {
                _host.Close();
            }
            textBox1.Text += string.Format("[{0}]Server Stopped!\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        }
    }
}
