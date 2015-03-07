using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModels
{
    public class Menu
    {
        private List<Item> _Items = new List<Item>();

        public List<Item> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }




    }
}
