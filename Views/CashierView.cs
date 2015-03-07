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
    public partial class CashierView : Form
    {
        public CashierView()
        {
            InitializeComponent();
        }


        public void ShiftView(Form form)
        {
            form.TopLevel = false;
            form.Parent = this.plMain;
            form.Show();
        }


        private List<Navigator> _Navigators = new List<Navigator>();

        public List<Navigator> Navigators
        {
            get { return _Navigators; }
            set
            {
                _Navigators = value;
                foreach (Navigator nav in _Navigators)
                {
                    BSE.Windows.Forms.XPanderPanel xp = new BSE.Windows.Forms.XPanderPanel();
                    xp.Text = nav.Label;
                    xp.Tag = nav;
                    xp.Dock = DockStyle.Fill;

                    DevComponents.DotNetBar.ItemPanel ip = new DevComponents.DotNetBar.ItemPanel();
                    ip.Dock = DockStyle.Fill;
                    xp.Controls.Add(ip);

                    if (nav.Child.Count > 0)
                    {
                        foreach (Navigator navchild in nav.Child)
                        {
                            DevComponents.DotNetBar.ButtonItem bi = new DevComponents.DotNetBar.ButtonItem();
                            bi.Text = navchild.Label;
                            bi.Name = "BTN_" + navchild.ID.ToString();
                            ip.Items.Add(bi);
                        }

                        xplMainMenu.XPanderPanels.Add(xp);
                    }


                }

                if (xplMainMenu.XPanderPanels.Count > 0)
                    xplMainMenu.XPanderPanels[0].Select();
            }
        }




    }




    public class Navigator
    {

        public string Label { get; set; }

        public int ID { get; set; }

        public int AuthorityID { get; set; }

        public List<Navigator> Child { get; set; }
    }



}
