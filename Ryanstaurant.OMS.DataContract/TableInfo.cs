using System;
using System.Runtime.Serialization;
namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
    public class TableInfo
    {
        /// <summary>
        /// 状态(0:空闲,1:有人,2:预定,3:被合并,4:被拆分,5:损坏)
        /// </summary>
        public enum TableStatus
        {
            Spare = 0,
            Occupied = 1,
            Reserved = 2,
            Combined = 3,
            Splited = 4,
            NotAvaliable = 5
        }

        [DataMember]
        public TableStatus CurrentStatus { get; set; }

        [DataMember]
        public int PosX { get; set; }

        [DataMember]
        public int PosY { get; set; }

        [DataMember]
        public int Length { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 获取默认实例
        /// </summary>
        /// <returns></returns>
        public static TableInfo GetDefault()
        {
            return new TableInfo
            {
                CurrentStatus = TableStatus.Spare,
                PosX = 0,
                PosY = 0,
                Length = 50,
                Width = 50
            };
        }


    }


    public static class TableStatusAdaptor
    {
        public static int ToInt(this TableInfo.TableStatus status)
        {
            return int.Parse(status.ToString());
        }
    }



}
