using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
    public class Item
    {
        public enum OrderItemStatus
        {
            Normal=0,
            Pending=1,
            Cancel=2
        }



        [Flags]
        public enum ItemType
        {
            Other,
            Drink,
            MainCourse,
            Appetizer,
            Dessert,
            Snack,
            Set
        }


        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Price { get; set; }
        [DataMember]
        public string Quantity { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public ItemType Type { get; set; }
        [DataMember]
        public IList<Item> Items { get; set; }
        [DataMember]
        public OrderItemStatus Status { get; set; }


    }
}
