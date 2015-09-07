using System.Collections.Generic;
using System.Runtime.Serialization;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.DataContract
{
    [DataContract]
    public class Role : ItemContent
    {

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public long Authority { get; set; }
    }
}
