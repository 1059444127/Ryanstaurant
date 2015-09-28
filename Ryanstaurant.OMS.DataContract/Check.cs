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
            this.Derates = bill.Derates;
            this.SubTotal = bill.SubTotal;
            this.Total = bill.Total;
        }

        internal Check() { }

        [DataMember]
        public List<Charge> Charge { get; set; }

        [DataMember]
        public List<Change> Changes { get; set; }

    }
}
