using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkSpace;
using BusinessModels;
using System.Data;

namespace Models
{
    public class CashierPresenterModel
    {


        public List<Navigator> Navigation { get; set; }


        BusinessLogic bl = new BusinessLogic();

        public CashierPresenterModel()
        {
            DataTable dt = bl.GetNavigations();
            DataRow[] drParents = dt.Select("ParentID is null");

            Navigation = new List<Navigator>();

            foreach(DataRow dr in drParents)
            {
                DataRow[] drItems = dt.Select("ParentID = '" + dr["ID"].ToString() + "'", "SortNumber asc");

                if (drItems.Length == 0)
                    continue;

                Navigator nav = new Navigator();
                nav.ID =int.Parse( dr["ID"].ToString());
                nav.Label = dr["Label"].ToString();
                nav.AuthorityID = int.Parse(dr["AuthorityID"].ToString() == "" ? "0" : dr["AuthorityID"].ToString());
                nav.Child = new List<Navigator>();

                foreach(DataRow drItem in drItems)
                {
                    Navigator navchild = new Navigator();
                    navchild.ID = int.Parse(drItem["ID"].ToString());
                    navchild.Label = drItem["Label"].ToString();
                    navchild.AuthorityID = int.Parse(drItem["AuthorityID"].ToString());

                    nav.Child.Add(navchild);

                }

                Navigation.Add(nav);
            }

        }












    }
}
