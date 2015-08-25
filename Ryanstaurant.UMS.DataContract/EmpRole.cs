using System.Runtime.Serialization;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.DataContract
{
    [DataContract]
    public class EmpRole : IDataContract
    {
        [DataMember]
        public int EmpID { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public string Exception { get; set; }
    }
}
