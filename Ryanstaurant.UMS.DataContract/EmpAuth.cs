using System.Runtime.Serialization;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.DataContract
{
    [DataContract]
    public class EmpAuth :ItemContent
    {

        [DataMember]
        public int EmpID { get; set; }
        [DataMember]
        public int AuthID { get; set; }
        [DataMember]
        public string Exception { get; set; }
        public RequestOperation Operation
        { get; set; }
    }
}
