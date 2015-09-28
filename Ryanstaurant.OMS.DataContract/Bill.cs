using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
    public class Bill:Order
    {
        public Bill(Order order)
        {
            this.ID = order.ID;
            this.Items = order.Items;
        }

        internal Bill() { }

        [DataMember]
        public IList<Derate> Derates { get; set; }

        [DataMember]
        public decimal SubTotal { get; set; }

        [DataMember]
        public decimal Total { get; set; }




    }
}
