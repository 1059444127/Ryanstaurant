using System;
using System.Globalization;
using System.Runtime.Serialization;
using Ryanstaurant.OMS.DataContract.Utility;

namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
    public class Table
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
        [EntityMapping(MapType = typeof(Guid))]
        public string ID { get; set; }

        [DataMember]
        [EntityMapping(MapName = "DisplayNo")]
        public string No { get; set; }

        [DataMember]
        public TableStatus CurrenEnumStatus { get; set; }

        [EntityMapping]
        public int CurrentStatus {
            get
            {
                return (int)CurrenEnumStatus;
            }
            set
            {
                CurrenEnumStatus = (TableStatus) Enum.Parse(typeof (TableStatus), value.ToString(CultureInfo.InvariantCulture));
            }
        }

        [DataMember]
        [EntityMapping]
        public int PosX { get; set; }

        [DataMember]
        [EntityMapping]
        public int PosY { get; set; }

        [DataMember]
        [EntityMapping]
        public int Length { get; set; }

        [DataMember]
        [EntityMapping]
        public int Width { get; set; }


        public bool IsInUse
        {
            get
            {
                return CurrenEnumStatus == TableStatus.Spare
                       || CurrenEnumStatus == TableStatus.Reserved
                       || CurrenEnumStatus == TableStatus.Occupied;
            }
        }


        public static Table GetDefault(string id)
        {
            return new Table
            {
                CurrenEnumStatus = TableStatus.Spare,
                ID = id,
                Length = 50,
                No = "0",
                PosX = 0,
                PosY = 0,
                Width = 50
            };
        }

        public static Table GetDefault(string id,string no)
        {
            return new Table
            {
                CurrenEnumStatus = TableStatus.Spare,
                ID = id,
                Length = 50,
                No = no,
                PosX = 0,
                PosY = 0,
                Width = 50
            };
        }




        public static Table ConvertFromEntity<T>(T entity) where T : new()
        {
            return DataContractAdaptor.LoadFrom<Table>(entity, typeof(T));
        }


        public T ToEntity<T>() where T : new()
        {
            return DataContractAdaptor.ConvertTo<T>(this, GetType());
        }



    }



}
