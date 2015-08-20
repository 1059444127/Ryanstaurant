using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RyanstaurantDataContract
{
    public class MenuEntity
    {
        private List<ItemEntity> _Items = new List<ItemEntity>();

        public List<ItemEntity> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }




    }
}
