using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Ryanstaurant.UMS.Entity
{
    [DataContract]
    public class Employee
    {
        private string _Name = "";
        [DataMember]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }


        private string _ID = "";

        [DataMember]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        private string _Password = "";

        [DataMember]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }


        private int _Authority = 0;

        [DataMember]
        public int Authority
        {
            get { return _Authority; }
            set { _Authority = value; }
        }











    }
}
