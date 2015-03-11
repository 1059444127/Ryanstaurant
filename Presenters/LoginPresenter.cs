using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Views;
using Models;




namespace Presenters
{
    public class LoginPresenter
    {
        public LoginPresenter(LoginView view)
        {
            this.View = view;
            this.View.btnLoginClick += delegate
            {
                LoginPresenterModel model = new LoginPresenterModel();
                model.LoginName = this.View.LoginName;
                model.Password = this.View.LoginPassword;

                string strResult = model.CheckUserPassword();
                this.View.ShowMessageBox(strResult);
                if (strResult.Contains("成功"))
                {
                    CashierPresenter capre = new CashierPresenter(new CashierView());
                    this.View.Close();
                }

            };


            this.View.btnExitClick += delegate
            {
                this.View.Close();
            };
        }



        public LoginView View { get; set; }






    }
}
