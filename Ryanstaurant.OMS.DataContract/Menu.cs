using System.Collections.Generic;

namespace Ryanstaurant.OMS.DataContract
{
    public class Menu
    {

        public string ID { get; set; }

        public string Name { get; set; }

        public IList<Item> Items { get; set; }

        public string Description { get; set; }

    }
}
