using System;
using System.Runtime.Serialization;

namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
    public class Charge
    {
        /// <summary>
        /// 支付方式
        /// </summary>
        [Flags]
        public enum ChargeType
        {
            /// <summary>
            /// 现金
            /// </summary>
            Cash,
            /// <summary>
            /// 信用卡
            /// </summary>
            CreditCard,
            /// <summary>
            /// 支票
            /// </summary>
            Check,
            /// <summary>
            /// 充值卡
            /// </summary>
            DebitCard,
            /// <summary>
            /// 优惠券
            /// </summary>
            Coupon,
            /// <summary>
            /// 支付宝
            /// </summary>
            Alipay
        }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string Description { get; set; }

    }
}
