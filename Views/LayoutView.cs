using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Views
{
    public partial class LayoutView : Form
    {
        public LayoutView()
        {
            InitializeComponent();
        }



        public EventHandler ButtonClicked;




        public Button ClickedButton
        {
            get;
            set;
        }


        private List<Table> _Tables = new List<Table>();
        public List<Table> Tables
        {
            get { return _Tables; }
            set
            {
                _Tables = value;
                PutTables(_Tables);
            }
        }



        private List<Seat> _Seats = new List<Seat>();

        public List<Seat> Seats
        {
            get { return _Seats; }
            set
            {
                _Seats = value;
                PutSeats(_Seats);
            }
        }

        








        private void PutTables(List<Table> tables)
        {
            foreach (Table t in tables)
            {
                PutTable(t);
            }
        }



        private void PutSeats(List<Seat> seats)
        {

            foreach (Seat s in seats)
            {
                PutSeat(s);
            }
        }


        private void PutTable(Table table)
        {
            Button btnTable = new Button();
            //string strURL = table.PicURL;
            //btnTable.Image = Image.FromFile(strURL);
            btnTable.Location = new Point(table.PositionX, table.PositionY);
            btnTable.Width = table.Width;
            btnTable.Height = table.Length;
            btnTable.Text = table.TableNo.ToString();
            btnTable.Tag = table;
            plFloor.Controls.Add(btnTable);
        }


        private void PutSeat(Seat seat)
        {
            Button btnSeat = new Button();
           // string strURL = seat.PicURL;
            //btnSeat.Image = Image.FromFile(strURL);
            btnSeat.Location = new Point(seat.PositionX, seat.PositionY);
            btnSeat.Width = seat.Width;
            btnSeat.Height = seat.Length;
            btnSeat.Text = seat.SeatNo.ToString();
            btnSeat.Tag = seat;
            plFloor.Controls.Add(btnSeat);
        }

        private void PutTableWithSeats(Table table)
        {
            PutTable(table);
            foreach (Seat se in table.SeatList)
            {
                PutSeat(se);
            }
        }


        public void LinkButtonClickEvent()
        {
            foreach (Control ctrl in plFloor.Controls)
            {
                if (ctrl is Button)
                {
                    Button btn = ctrl as Button;
                    btn.Click += btn_Click;
                }
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ClickedButton = btn;
            if (ButtonClicked != null)
                ButtonClicked(sender, e);
        }


        public void ShowMessageBox(string strMessage)
        {
            MessageBox.Show(strMessage);
        }



        private string _BackGroundURL = "";

        public string BackGroundURL
        {
            get { return _BackGroundURL; }
            set
            {
                _BackGroundURL = value;
                if (!string.IsNullOrEmpty(_BackGroundURL))
                    this.BackgroundImage = Image.FromFile(_BackGroundURL);
            }
        }







    }




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
            NotExist = -1,
            /// <summary>
            /// 空闲
            /// </summary>
            Vocation = 0,
            /// <summary>
            /// 已占
            /// </summary>
            Occupied = 1,
            /// <summary>
            /// 损坏待修
            /// </summary>
            Broken = 2
        }
    }




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
