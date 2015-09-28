using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ryanstaurant.OMS.DataContract
{
    [DataContract]
    public class Table
    {
        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string No { get; set; }

        [DataMember]
        public TableInfo Information { get; set; }

        public bool IsInUse
        {
            get
            {
                return Information.CurrentStatus == TableInfo.TableStatus.Spare
                       || Information.CurrentStatus == TableInfo.TableStatus.Reserved
                       || Information.CurrentStatus == TableInfo.TableStatus.Occupied;
            }
        }

    }
}
