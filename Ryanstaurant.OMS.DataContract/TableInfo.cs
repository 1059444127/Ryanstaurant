using System.Runtime.Serialization;
namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
    public class TableInfo
    {
        public enum TableStatus
        {
            Disabled = 0,
            Spare = 1,
            Occupied = 2
        }
        [DataMember]
        public TableStatus CurrentStatus { get; set; }

        [DataMember]
        public int PosX { get; set; }

        [DataMember]
        public int PosY { get; set; }




    }
}
