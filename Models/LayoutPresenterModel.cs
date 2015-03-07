using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkSpace;
using BusinessModels;
using UIEntity;
using System.Data;

namespace Models
{
    public class LayoutPresenterModel
    {

        private string _BackGroundURL = "";

        public string BackGroundURL
        {
            get { return _BackGroundURL; }
            set { _BackGroundURL = value; }
        }



        public List<Table> Tables = new List<Table>();

        public List<Seat> Seats = new List<Seat>();

        BusinessLogic bl = new BusinessLogic();


        public LayoutPresenterModel()
        {
            LoadLayout();
        }

        private void LoadLayout()
        {
            Tables.Clear();
            Seats.Clear();
            DataTable dt = new DataTable();
            dt = bl.GetFloorLayout();

            DataTable dtTables =  dt.DefaultView.ToTable(true, "TableNo").Copy();
            for (int i = 0; i < dtTables.Rows.Count; i++)
            {

                Table t = new Table();
                t.Length = int.Parse(dt.Rows[i]["TableLength"].ToString());
                t.PositionX = int.Parse(dt.Rows[i]["TablePositionX"].ToString());
                t.PositionY = int.Parse(dt.Rows[i]["TablePositionY"].ToString());
                t.Width = int.Parse(dt.Rows[i]["TableWidth"].ToString());
                t.TableNo = int.Parse(dt.Rows[i]["TableNo"].ToString());
                t.SeatList = new List<Seat>();
                DataRow[] drSeats = dt.Select("TableNo = '" + dtTables.Rows[i]["TableNo"].ToString() + "'");
                foreach(DataRow dr in drSeats)
                {
                    Seat s = new Seat();
                    s.CurrentDirection = (Seat.Direction)Enum.Parse(typeof(Seat.Direction), dr["Direction"].ToString());
                    s.CurrentStatus = (Seat.Status)Enum.Parse(typeof(Seat.Status), dr["CurrentStatus"].ToString());
                    s.Length = int.Parse(dr["SeatLength"].ToString());
                    s.PositionX = int.Parse(dr["SeatPositionX"].ToString());
                    s.PositionY = int.Parse(dr["SeatPositionY"].ToString());
                    s.Width = int.Parse(dr["SeatWidth"].ToString());
                    s.TableNo = int.Parse(dr["SeatNo"].ToString());
                    s.SeatNo = int.Parse(dr["SeatNo"].ToString());

                    t.SeatList.Add(s);
                    Seats.Add(s);
                }

                Tables.Add(t);
            }
        }






    }










}
