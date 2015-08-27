using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MicroMvvm;
using Ryanstaurant.Clients.WCFClient.Models.UMS;

namespace Ryanstaurant.Clients.WCFClient.ViewModels.UMS
{
    public class EmployeeViewModel:ObservableObject
    {


        public EmployeeViewModel()
        {
            _employee = new Employee
            {
                ID=1,
                Description = "SimUser",
                LoginName = "RYAN",
                Name = "RYAN",
                Password = "123456"
            };
        }





        private Employee _employee;

        public Employee Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
            }
        }


        public string LoginName
        {
            get { return Employee.LoginName; }
            set { Employee.LoginName = value;
                
            }
        }


        public string Name
        {
            get { return Employee.Name; }
            set
            {
                Employee.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Password
        {
            get { return Employee.Password; }
            set { Employee.Password = value; }
        }

        public string Description
        {
            get { return Employee.Description; }
            set { Employee.Description = value; }
        }

        public int AuthorityCode
        {
            get
            {
                var iCode = 0;
                foreach (var authority in this.Employee.AuthorityList)
                {
                    iCode |= authority.KeyCode;
                }
                return iCode;
            }
        }



        public ICommand updateName
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Name = Guid.NewGuid().ToString();
                });
            }
        }



    }
}
