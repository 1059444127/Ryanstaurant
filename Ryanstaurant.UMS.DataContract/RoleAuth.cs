using System.Runtime.Serialization;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.DataContract
{
    [DataContract]
    public class RoleAuth : ItemContent
    {
        [DataMember]
        public int RoleID { get; set; }
        [DataMember]
        public int AuthID { get; set; }
        [DataMember]
        public string Exception { get; set; }
        public RequestOperation Operation
        { get; set; }
    }
}
