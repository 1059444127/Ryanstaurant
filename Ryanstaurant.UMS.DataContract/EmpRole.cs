using System.Runtime.Serialization;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.DataContract
{
    [DataContract]
    public class EmpRole : ItemContent
    {
        [DataMember]
        public int EmpID { get; set; }
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public string Exception { get; set; }
    }
}
