using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ryanstaurant.UMS.DataContract.Utility
{
    public class RequestEntity
    {
        public List<ItemContent> RequestObjects { get; set; }


        [DataMember]
        public string SessionToken { get; set; }


        [DataMember]
        public string Verification { get; set; }

    }
}
