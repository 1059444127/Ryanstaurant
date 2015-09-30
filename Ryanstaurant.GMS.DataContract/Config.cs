using System;
using System.Runtime.Serialization;

namespace Ryanstaurant.GMS.DataContract
{
    [DataContract]
    public class Config
    {
        [DataMember]
        public string ShortCall { get; set; }


        [DataMember]
        public string ConfigValue { get; set; }


        [DataMember]
        public string Description { get; set; }

    }
}
