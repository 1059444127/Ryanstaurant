using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model.UMSProxy;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class EmployeeModel:ModelBase
    {
        private int _id = -1;
        private string _name = string.Empty;
        private string _loginName = string.Empty;
        private string _password = string.Empty;
        private string _description = string.Empty;
        protected readonly UMSServiceClient ServiceClient = new UMSServiceClient();
        private List<RoleModel> _roleList;
        private List<AuthorityModel> _authorityList;


        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
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
                OnPropertyChanged("LoginName");
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
                OnPropertyChanged("Password");
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
                OnPropertyChanged("Description");
            }
        }

        public List<RoleModel> RoleList
        {
            get
            {
                return _roleList;
            }
            set
            {
                _roleList = value;
                OnPropertyChanged("RoleList");
            }
        }


        public List<AuthorityModel> AuthorityList
        {
            get
            {
                return _authorityList;
            }
            set
            {
                _authorityList = value;
                OnPropertyChanged("AuthorityList");
            }
        }





        public List<EmployeeModel> GetAllEmployees()
        {
            //获取EMP基本信息
            var result = ServiceClient.GetEmployees(null);
            if (result.State != ResultState.Success)
            {
                throw new Exception(result.ErrorMessage);
            }

            var employeesReturn = result.ResultObject.ToList();

            //写入基本信息
            var employeeList = employeesReturn.Select(employee => new EmployeeModel
            {
                _description = employee.Description,
                _id = employee.ID,
                _loginName = employee.LoginName,
                _name = employee.Name,
                _password = employee.Password
            }).ToList();


            //获取EMP相关的角色信息
            var roles = employeesReturn.Select(e => new EmpRole
            {
                EmpID = e.ID
            }).Select(er => new Role
            {
                ID = er.RoleID
            }).ToList();

             ServiceClient.GetRoles(roles.ToArray());



            //获取EMP相关权限信息
            var auths = employeesReturn.Select(e => new EmpAuth
            {
                EmpID = e.ID
            }).Select(er => new Authority
            {
                ID = er.AuthID
            }).ToList();

            //获取用户角色相关信息
            var roleauths = roles.Select(r => new RoleAuth
            {
                RoleID = r.ID
            }).Select(ra => new Authority
            {
                ID = ra.AuthID
            }).ToList();

            auths = auths.Concat(roleauths).ToList();






            return null;

        }



        public void AddEmployees(List<EmployeeModel> employeeModels)
        {
            var results = ServiceClient.AddEmployees(employeeModels.Select(employeeModel => new Employee
            {
                ID = employeeModel.ID,
                Description = employeeModel.Description,
                LoginName = employeeModel.LoginName,
                Name = employeeModel.Name,
                Password = employeeModel.Password
            }).ToArray());

        }


    }
}
