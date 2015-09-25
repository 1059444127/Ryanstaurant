using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
    public class Item
    {
        [Flags]
        public enum ItemType
        {
            Drink ,
            MainCourse,
            Appetizer ,
            Dessert ,
            Snack ,
            Set,
            Other
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


    }
}
