using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model.UMSProxy;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class EmployeeListModel
    {
        public List<EmployeeModel> Collection { get; set; }

        protected readonly UMSServiceClient ServiceClient = new UMSServiceClient();

        public EmployeeListModel()
        {
            Collection = GetAllEmployees();
        }




        public  List<EmployeeModel> GetAllEmployees()
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
                Description = employee.Description,
                ID = employee.ID,
                LoginName = employee.LoginName,
                Name = employee.Name,
                Password = employee.Password
            }).ToList();

            return employeeList;

        }

    }
}
