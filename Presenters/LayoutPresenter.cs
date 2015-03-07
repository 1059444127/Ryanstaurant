using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Views;
using UIEntity;
using Models;

namespace Presenters
{
    public class LayoutPresenter
    {

        public LayoutPresenter(LayoutView view)
        {
            this.View = view;

            LayoutPresenterModel model = new LayoutPresenterModel();

            this.View.PutTables(model.Tables);
            this.View.PutSeats(model.Seats);
            this.View.LinkButtonClickEvent();
            this.View.ButtonClicked += delegate
            {
                if (this.View.ClickedButton.Tag is Table)
                {
                    this.View.ShowMessageBox("选中了" + (this.View.ClickedButton.Tag as Table).TableNo + "号桌");
                }

                if (this.View.ClickedButton.Tag is Seat)
                {
                    this.View.ShowMessageBox("选中了" + (this.View.ClickedButton.Tag as Seat).TableNo + "号桌" + (this.View.ClickedButton.Tag as Seat).SeatNo + "号位");
                }
            };

        }



        public LayoutView View { get; set; }









    }
}
