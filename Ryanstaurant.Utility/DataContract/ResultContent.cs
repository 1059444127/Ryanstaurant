using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ryanstaurant.Utility.DataContract
{
    [DataContract]
    public class ResultItem<T>
    {
        [DataMember]
        public T Item { get; set; }


        [DataMember]
        public string Exception { get; set; }


    }
}
