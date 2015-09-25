using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ryanstaurant.OMS.DataContract
{
    public class Order
    {
        public string ID { get; set; }

        public IList<Item> Items { get; set; }


    }
}
