using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using MicroMvvm;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel
{
    public class EmployeeViewModel:ViewModelBase
    {



        public EmployeeViewModel()
        {
            Employee = new EmployeeModel();
        }


        public bool IsModify
        {
            get
            {
                return Operation == OperationType.Modify;
            }
        }



        public OperationType Operation
        {
            get; set;
        }


        public EmployeeModel Employee { get; set; }


        public int ID
        {
            get
            {
                return Employee.ID;
            }
            set
            {
                Employee.ID = value;
                RaisePropertyChanged("ID");
            }
        }

        public string Name
        {
            get
            {
                return Employee.Name;
            }
            set
            {
                Employee.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string LoginName
        {
            get
            {
                return Employee.LoginName;
            }
            set
            {
                Employee.LoginName = value;
                RaisePropertyChanged("LoginName");
            }
        }
        public string Password
        {
            get
            {
                return Employee.Password;
            }
            set
            {
                Employee.Password = value;
                RaisePropertyChanged("Password");
            }
        }
        public string Description
        {
            get
            {
                return Employee.Description;
            }
            set
            {
                Employee.Description = value;
                RaisePropertyChanged("Description");
            }
        }

        public long EmpAuthority
        {
            get
            {
                return Employee.EmpAuthority;
            }
            set
            {
                Employee.EmpAuthority = value;
                RaisePropertyChanged("EmpAuthority");

            }
        }

        public long Authority
        {
            get
            {
                return EmpAuthority | RoleList.Aggregate((long) 0, (current, role) => current | role.Authority);
            }
        }


        private Brush _forColor = new SolidColorBrush(Color.FromRgb(0x00, 0x50, 0xef));

        public Brush ForeColor
        {
            get
            {
                return _forColor;
            }
            set
            {
                _forColor = value;
                RaisePropertyChanged("ForeColor");
            }
        }

        private string _information = string.Empty;

        public string Information
        {
            get { return _information; }
            set { _information = value; RaisePropertyChanged("Information"); }
            
        }



        public List<RoleViewModel> RoleList
        {
            get
            {
                return (from a in Employee.Roles
                    select new RoleViewModel
                    {
                        Description = a.Description,
                        ID = a.ID,
                        Name = a.Name,
                        Authority = a.Authority
                    }).ToList();
            }
        }


        public void AddRole(RoleViewModel role)
        {
            Employee.Roles.Add(new RoleModel
            {
                Authority = role.Authority,
                Description = role.Description,
                ID = role.ID,
                Name = role.Name
            });

            Employee.Modify();
        }


        public void DeleteRole(RoleViewModel role)
        {
            var modelRole = (from r in Employee.Roles where r.ID == role.ID select r).FirstOrDefault();

            if (modelRole == null)
                return;

            Employee.Roles.Remove(modelRole);

            Employee.Modify();
        }


        public void AddAuthority(long authority)
        {
            Employee.EmpAuthority |= authority;
            Employee.Modify();
        }


        public void DeleteAuthority(long authority)
        {
            Employee.EmpAuthority &= ~authority;
            Employee.Modify();
        }



        public void AddEmployee()
        {
            Employee.Add();
        }

        public void DeleteEmployee()
        {
            Employee.Delete();
        }

        public void ModifyEmployee()
        {
            Employee.Modify();
        }


        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {

                        switch (Operation)
                        {
                            case OperationType.Add:
                                AddEmployee();
                                break;
                            case OperationType.Modify:
                                ModifyEmployee();
                                break;
                        }

                        if (Close != null)
                            Close();

                    }
                    catch (Exception ex)
                    {
                        ForeColor = new SolidColorBrush(Color.FromRgb(0xe5, 0x14, 0x00));
                        Information = ex.Message;
                    }




                });
            }
        }

        public event Action Close; 

    }
}
