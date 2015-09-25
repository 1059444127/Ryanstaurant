using System;
using System.Collections.Generic;

namespace Ryanstaurant.OMS.DataContract
{
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


        public string ID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public ItemType Type { get; set; }
        public IList<Item> Items { get; set; } 


    }
}
