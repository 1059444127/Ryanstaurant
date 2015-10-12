using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
    public class Change
    {
        [Flags]
        public enum ChangeType
        {
            /// <summary>
            /// 现金
            /// </summary>
            Cash
        }

        [DataMember]
        public decimal Amount { get; set; }





    }
}
