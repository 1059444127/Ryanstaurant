using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ryanstaurant.OMS.DataContract
{
    public class Change
    {
        [Flags]
        public enum ChangeType
        {
            /// <summary>
            /// 现金
            /// </summary>
            Cash,
            /// <summary>
            /// 充值卡
            /// </summary>
            DebitCard,
            /// <summary>
            /// 优惠券
            /// </summary>
            Coupon
        }

        [DataMember]
        public decimal Amount { get; set; }





    }
}
