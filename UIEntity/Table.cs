using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIEntity
{
    public class Table
    {


        public Table()
        {
        }



        public string PicURL { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }
        public int TableNo { get; set; }
        public List<Seat> SeatList { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        //public Status CurrentStatus { get; set; }
        //public enum Status
        //{
        //    Unknown = -1,
        //    /// <summary>
        //    /// 空闲
        //    /// </summary>
        //    Vocation = 0,
        //    /// <summary>
        //    /// 已占
        //    /// </summary>
        //    Occupied = 1,
        //    /// <summary>
        //    /// 损坏待修
        //    /// </summary>
        //    Broken = 2
        //}

    }



    
}
