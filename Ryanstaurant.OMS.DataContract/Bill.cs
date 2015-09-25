using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ryanstaurant.OMS.DataContract
{
    public class Bill:Order
    {
        public Bill(Order order)
        {
            this.ID = order.ID;
            this.Items = order.Items;
        }

        internal Bill() { }


        public decimal Discount { get; set; }

        public decimal Derate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }




    }
}
