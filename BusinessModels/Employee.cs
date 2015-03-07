using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessModels
{
    public class Employee
    {
        private string _Name = "";

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }


        private string _ID = "";

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        private string _Password = "";

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }


        private int _Authority = 0;

        public int Authority
        {
            get { return _Authority; }
            set { _Authority = value; }
        }











    }
}
