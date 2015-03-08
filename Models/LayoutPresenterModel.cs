using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkSpace;
using BusinessModels;
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



        public DataTable dtTableSeats { get; set; }



        BusinessLogic bl = new BusinessLogic();


        public LayoutPresenterModel()
        {
            dtTableSeats = bl.GetFloorLayout();


        }







    }










}
