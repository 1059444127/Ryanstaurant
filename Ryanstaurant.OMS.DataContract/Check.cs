using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
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

        [DataMember]
        public List<Charge> Charge { get; set; }

        [DataMember]
        public List<Change> Change { get; set; }

    }
}
