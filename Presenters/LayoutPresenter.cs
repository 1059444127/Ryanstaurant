using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Views;
using System.Data;
using Models;

namespace Presenters
{
    public class LayoutPresenter
    {

        private List<Table> Tables = new List<Table>();
        private List<Seat> Seats = new List<Seat>();


        private LayoutPresenterModel model = new LayoutPresenterModel();


        public LayoutPresenter(LayoutView view)
        {
            this.View = view;
            LoadLayout();

            this.View.Tables = Tables;
            this.View.Seats = Seats;
            this.View.BackGroundURL = model.BackGroundURL;
            this.View.LinkButtonClickEvent();
            this.View.ButtonClicked += delegate
            {
                if (this.View.ClickedButton.Tag is Table)
                {
                    this.View.ShowMessageBox("选中了" + (this.View.ClickedButton.Tag as Table).TableNo + "号桌");
                }

                if (this.View.ClickedButton.Tag is Seat)
                {
                    this.View.ShowMessageBox("选中了" + (this.View.ClickedButton.Tag as Seat).TableNo + "号桌" + (this.View.ClickedButton.Tag as Seat).SeatNo + "号位");
                }
            };

        }



        public LayoutView View { get; set; }





        private void LoadLayout()
        {
            Tables.Clear();
            Seats.Clear();
            DataTable dt = model.dtTableSeats;

            DataTable dtTables = dt.DefaultView.ToTable(true, "TableNo").Copy();
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
                foreach (DataRow dr in drSeats)
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
