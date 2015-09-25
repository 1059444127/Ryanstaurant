using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ryanstaurant.OMS.DataContract
{
    public class Check:Bill
    {
        public Check(Bill bill)
        {
            this.ID = bill.ID;
            this.Items = bill.Items;
            this.Derate = bill.Derate;
            this.SubTotal = bill.SubTotal;
            this.Total = bill.Total;
            this.Discount = bill.Discount;
        }

        internal Check() { }

        public List<Charge> Charge { get; set; }

        public List<Change> Change { get; set; }

    }
}
