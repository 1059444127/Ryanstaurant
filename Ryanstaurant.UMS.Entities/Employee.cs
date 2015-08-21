using System.Runtime.Serialization;

namespace Ryanstaurant.UMS.Entity
{
    [DataContract]
    public class Employee
    {
        [DataMember]
        public string Name { get; set; }


        [DataMember]
        public string ID { get; set; }


        [DataMember]
        public string Password { get; set; }


        [DataMember]
        public int Authority { get; set; }


        [DataMember]
        public string Exception { get; set; }
    }
}
