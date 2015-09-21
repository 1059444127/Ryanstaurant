using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.Client;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class EmployeeModel
    {
        private int _id = -1;
        private string _name = string.Empty;
        private string _loginName = string.Empty;
        private string _password = string.Empty;
        private string _description = string.Empty;
        protected IUMSClient ServiceClient = new UMSClient();

        public EmployeeModel()
        {
            EmpAuthority = 0;
        }


        public IUMSClient UmsClient
        {
            get
            {
                return ServiceClient;
            }
            set
            {
                ServiceClient = value;
            }
        }


        #region 属性
        public int ID { get { return _id; } set { _id = value; } }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string LoginName
        {
            get
            {
                return _loginName;
            }
            set
            {
                _loginName = value;
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public long EmpAuthority { get; set; }

        public long Authority
        {
            get
            {
                return EmpAuthority | Roles.Aggregate((long) 0, (current, r) => current | r.Authority);
            }
        }

        private List<RoleModel> _roles = new List<RoleModel>();

        public List<RoleModel> Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                _roles = value;
            }
        }





        #endregion


        public void Add()
        {
            var arrEmployee = new List<ItemContent>
            {
                new Employee
                {
                    Description = Description,
                    ID = ID,
                    LoginName = LoginName,
                    Name = Name,
                    Password = Password,
                    EmpAuthority = EmpAuthority,
                    Roles = (from r in Roles select new Role
                    {
                        ID = r.ID
                    }).ToList(),
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Add
                    }
                }
            };

            var results = ServiceClient.Execute(arrEmployee);

            if (ServiceClient.State == ResultState.Fail)
                throw new Exception(ServiceClient.ErrorMessage);

            Refresh();
        }


        public void Refresh()
        {
            var arrEmployee = new List<ItemContent>
            {
                new Employee
                {
                    ID = ID,
                    Name = Name,
                    CommandInfo = new CommandInformation
                    {
                        Operation=RequestOperation.Query
                    }
                }
            };

            var results = ServiceClient.Query(arrEmployee);
            if (ServiceClient.State == ResultState.Fail)
                throw new Exception(ServiceClient.ErrorMessage);

            var empReturn = results.FirstOrDefault() as Employee;


            if (empReturn == null)
            {
                throw new Exception(ID == -1 ? "没有找到名称为[" + Name + "]对应的人员" : "没有找到ID为[" + ID + "]对应的人员");
            }

            ID = empReturn.ID;
            Description = empReturn.Description;
            LoginName = empReturn.LoginName;
            Name = empReturn.Name;
            Password = empReturn.Password;
            EmpAuthority = empReturn.EmpAuthority;
        }

    

        public void Modify()
        {
            var arrEmployee = new List<ItemContent>
            {
                new Employee
                {
                    Description = Description,
                    ID = ID,
                    LoginName = LoginName,
                    Name = Name,
                    Password = Password,
                    EmpAuthority = EmpAuthority,
                    Roles = (from r in Roles select new Role
                    {
                        ID = r.ID
                    }).ToList(),
                    CommandInfo=new CommandInformation
                    {
                        Operation = RequestOperation.Modify
                    }
                }
            };



            var results = ServiceClient.Execute(arrEmployee);

            if (ServiceClient.State == ResultState.Fail)
                throw new Exception(ServiceClient.ErrorMessage);

            Refresh();
        }

        public void Delete()
        {
            var arrEmployee = new List<ItemContent>
            {
                new Employee
                {
                    Description = Description,
                    ID = ID,
                    LoginName = LoginName,
                    Name = Name,
                    Password = Password,
                    EmpAuthority = EmpAuthority,
                    Roles = (from r in Roles select new Role
                    {
                        ID = r.ID
                    }).ToList(),
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Delete
                    }
                }
            };



            var results = ServiceClient.Execute(arrEmployee);

            if (ServiceClient.State == ResultState.Fail)
                throw new Exception(ServiceClient.ErrorMessage);

           Reset();
        }


        public void Reset()
        {
            _description = string.Empty;
            _id = -1;
            _loginName = string.Empty;
            _name = string.Empty;
            _password = string.Empty;
            Roles = new List<RoleModel>();
            EmpAuthority = 0;
        }


        public List<EmployeeModel> GetAllEmmployees()
        {

            var results = ServiceClient.GetAllEmployees();

            if (ServiceClient.State == ResultState.Fail)
                throw new Exception(ServiceClient.ErrorMessage);

            Reset();

            return (from e in results.Cast<Employee>().ToList()
                select new EmployeeModel
                {
                    Description = e.Description,
                    ID = e.ID,
                    LoginName = e.LoginName,
                    Password = e.Password,
                    EmpAuthority = e.EmpAuthority,
                    Name = e.Name,
                    Roles = (from r in e.Roles
                        select new RoleModel
                        {
                            Authority = r.Authority,
                            Description = r.Description,
                            ID = r.ID,
                            Name = r.Name
                        }).ToList()
                }).ToList();
        }

    }
}
