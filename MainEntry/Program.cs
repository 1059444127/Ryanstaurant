using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Presenters;
using Views;

namespace MainEntry
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginPresenter presenter = new LoginPresenter(new LoginView());
            Application.Run(presenter.View);
        }
    }
}
