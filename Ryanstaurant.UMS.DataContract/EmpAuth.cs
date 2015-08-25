using System.Runtime.Serialization;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.DataContract
{
    [DataContract]
    public class EmpAuth : IDataContract
    {

        [DataMember]
        public int EmpID { get; set; }
        [DataMember]
        public int AuthID { get; set; }
        [DataMember]
        public string Exception { get; set; }
    }
}
