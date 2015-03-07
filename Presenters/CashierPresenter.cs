using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Views;
using Models;

namespace Presenters
{
    public class CashierPresenter
    {
        public CashierView View { get; set; }



        public CashierPresenter(CashierView view)
        {
            this.View = view;
            LoadNavigator();
            ShowFloor();
            this.View.ShowDialog();
        }



        public void ShowFloor()
        {
            LayoutPresenter pre = new LayoutPresenter(new LayoutView());
            this.View.ShiftView(pre.View);

        }


        public void LoadNavigator()
        {
            CashierPresenterModel model = new CashierPresenterModel();
            this.View.LoadNavigator(model.Navigation);
        }




    }
}
