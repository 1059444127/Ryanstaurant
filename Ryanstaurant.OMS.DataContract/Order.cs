using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
    public class Order
    {
        [Flags]
        public enum OrderStatus
        {
            //订单状态:1预定2点单4送单8印单16结账32暂停64退单128取消
            /// <summary>
            /// 预定中
            /// </summary>
            Reserved,
            /// <summary>
            /// 已点单
            /// </summary>
            Ordering,
            /// <summary>
            /// 已送单
            /// </summary>
            Sent,
            /// <summary>
            /// 已打印
            /// </summary>
            Printed,
            /// <summary>
            /// 已结账
            /// </summary>
            Checked,
            /// <summary>
            /// 暂停
            /// </summary>
            Pending,
            /// <summary>
            /// 退单
            /// </summary>
            Revoke,
            /// <summary>
            /// 取消
            /// </summary>
            Cancel
        }



        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public IList<Item> Items { get; set; }

        [DataMember]
        public IList<Discount> Discounts { get; set; }
 

        [DataMember]
        public string TableId { get; set; }

        [DataMember]
        public string IsReserved { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string PendingReason { get; set; }

        [DataMember]
        public string RevokeReason { get; set; }

        [DataMember]
        public string CancelReason { get; set; }






    }
}
