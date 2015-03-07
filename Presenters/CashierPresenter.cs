using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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
            DataRow[] drParents = model.dtNavigations.Select("ParentID is null");

            List<Navigator> Navigations = new List<Navigator>();
            

            foreach (DataRow dr in drParents)
            {
                DataRow[] drItems = model.dtNavigations.Select("ParentID = '" + dr["ID"].ToString() + "'", "SortNumber asc");

                if (drItems.Length == 0)
                    continue;

                Navigator nav = new Navigator();
                nav.ID = int.Parse(dr["ID"].ToString());
                nav.Label = dr["Label"].ToString();
                nav.AuthorityID = int.Parse(dr["AuthorityID"].ToString() == "" ? "0" : dr["AuthorityID"].ToString());
                nav.Child = new List<Navigator>();

                foreach (DataRow drItem in drItems)
                {
                    Navigator navchild = new Navigator();
                    navchild.ID = int.Parse(drItem["ID"].ToString());
                    navchild.Label = drItem["Label"].ToString();
                    navchild.AuthorityID = int.Parse(drItem["AuthorityID"].ToString());

                    nav.Child.Add(navchild);

                }

                Navigations.Add(nav);
            }

            this.View.Navigators = Navigations;
        }




    }
}
