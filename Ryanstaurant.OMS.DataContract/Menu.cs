using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
    public class Menu
    {

        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IList<Item> Items { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string SubDescription { get; set; }

        [DataMember]
        public string SortOrder { get; set; }

    }
}
