using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIEntity
{
    public class Seat
    {
        public Seat()
        {
        }


        public string PicURL { get; set; }

        public enum Direction
        {
            North = 0,
            East = 90,
            South = 180,
            West = 270
        }

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int SeatNo { get; set; }
        public int TableNo { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public Status CurrentStatus { get; set; }
        public Direction CurrentDirection { get; set; }

        public enum Status
        {
            /// <summary>
            /// 不存在
            /// </summary>
            NotExist=-1,
            /// <summary>
            /// 空闲
            /// </summary>
            Vocation=0,
            /// <summary>
            /// 已占
            /// </summary>
            Occupied=1,
            /// <summary>
            /// 损坏待修
            /// </summary>
            Broken=2
        }
    }

}
