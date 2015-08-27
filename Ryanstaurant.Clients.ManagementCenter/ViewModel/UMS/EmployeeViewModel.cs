using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MicroMvvm;
using Ryanstaurant.Clients.ManagementCenter.Models.UMS;
using Ryanstaurant.Clients.ManagementCenter.Proxy.UMS;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.ManagementCenter.ViewModels.UMS
{
    public class EmployeeViewModel:ObservableObject
    {


        public EmployeeViewModel()
        {




        }

        private int _id;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
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
            set
            {
                Employee.LoginName = value;
                RaisePropertyChanged("LoginName");
                
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
            set
            {
                Employee.Password = value;
                RaisePropertyChanged("Password");
            }
        }

        public string Description
        {
            get { return Employee.Description; }
            set
            {
                Employee.Description = value;
                RaisePropertyChanged("Description");
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
