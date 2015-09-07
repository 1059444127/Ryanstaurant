using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.Client;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class EmployeeListModel
    {
        public List<EmployeeModel> Collection { get; set; }

        protected readonly UMSClient ServiceClient = new UMSClient();

        public EmployeeListModel()
        {
            Collection = GetAllEmployees();
        }




        public  List<EmployeeModel> GetAllEmployees()
        {
            //获取EMP基本信息
            var result = ServiceClient.GetAllEmployees();
            if (result.State != ResultState.Success)
            {
                throw new Exception(result.ErrorMessage);
            }

            var employeesReturn = result.ResultObject.Cast<Employee>().ToList();

            //写入基本信息
            var employeeList = employeesReturn.Select(employee => new EmployeeModel
            {
                Description = employee.Description,
                ID = employee.ID,
                LoginName = employee.LoginName,
                Name = employee.Name,
                Password = employee.Password,
                Roles = (from r in employee.Roles
                    select new RoleModel
                    {
                        ID = r.ID,
                        Description = r.Description,
                        Name = r.Name,
                        Authority=r.Authority
                    }).ToList(),
               EmpAuthority = employee.EmpAuthority
            }).ToList();

            return employeeList;

        }

    }
}
