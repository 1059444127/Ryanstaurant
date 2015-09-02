using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ryanstaurant.UMS.DataContract.Utility
{
    [DataContract]
    public class RequestContent<T>
    {
        [DataMember]
        public T RequestObject { get; set; }
        [DataMember]
        public RequestOperation Operation { get; set; }

    }
}
