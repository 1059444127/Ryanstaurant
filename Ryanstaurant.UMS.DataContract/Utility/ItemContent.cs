using System.Runtime.Serialization;

namespace Ryanstaurant.UMS.DataContract.Utility
{
    [DataContract]
    [KnownType(typeof(Employee))]
    [KnownType(typeof(Role))]
    [KnownType(typeof(Authority))]
    [KnownType(typeof(Login))]
    public class ItemContent
    {
        [DataMember]
        public ResultContent ResultInfo { get; set; }

        [DataMember]
        public RequestContent RequestInfo { get; set; }

    }
}
