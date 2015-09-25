using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public IList<Item> Items { get; set; }


    }
}
