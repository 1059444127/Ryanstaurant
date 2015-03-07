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


        public DataTable dtNavigations { get; set; }


        BusinessLogic bl = new BusinessLogic();

        public CashierPresenterModel()
        {
            dtNavigations = bl.GetNavigations();
            

        }












    }
}
