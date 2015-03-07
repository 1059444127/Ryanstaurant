using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModels
{
    public class Bill
    {
        
        private List<Item> _items = new List<Item>();

        public List<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }



        



    }
}
