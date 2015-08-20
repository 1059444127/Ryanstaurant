using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RyanstaurantDataContract
{
    public class BillEntity
    {
        
        private List<ItemEntity> _items = new List<ItemEntity>();

        public List<ItemEntity> Items
        {
            get { return _items; }
            set { _items = value; }
        }



        



    }
}
