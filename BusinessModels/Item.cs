using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModels
{
    public class Item
    {

        private string _Name = "";

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }


        private double _Price = 0;

        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }


        private double _Amount = 0;

        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }




        public double Total
        {
            get { return Rounding(_Price * _Amount); }
        }



        protected virtual double Rounding(double Amt)
        {
            return Math.Round(Amt, 2);
        }




    }
}
