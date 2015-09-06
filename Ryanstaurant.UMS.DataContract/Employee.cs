using System.Collections.Generic;
using System.Runtime.Serialization;
using Ryanstaurant.UMS.DataContract.Utility;


namespace Ryanstaurant.UMS.DataContract
{
    [DataContract]
    public class Employee : ItemContent
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string LoginName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Exception { get; set; }
        [DataMember]
        public List<Role> Roles { get; set; }
        [DataMember]
        public List<Authority> Authorities { get; set; }
        public RequestOperation Operation
        { get; set; }
    }
}
