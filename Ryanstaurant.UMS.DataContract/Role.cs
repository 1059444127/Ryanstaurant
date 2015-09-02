using System.Collections.Generic;
using System.Runtime.Serialization;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.DataContract
{
    [DataContract]
    public class Role : IDataContract
    {

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Exception { get; set; }
        [DataMember]
        public List<Authority> Authorities { get; set; } 
    }
}
