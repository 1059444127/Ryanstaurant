using System.Runtime.Serialization;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.DataContract
{
    [DataContract]
    public class Login:ItemContent
    {
        [DataMember]
        public string SessionToken { get; set; }



    }
}
