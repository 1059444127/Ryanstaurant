using System;
using System.Collections.Generic;
using Ryanstaurant.DataContract.Utility;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.Interface;
using Ryanstaurant.UMS.WorkSpace;


namespace Ryanstaurant.UMS.Service
{
    public class UMSService:IUMSService
    {

        public ResultEntity<Employee> GetEmployee(string employeeName)
        {
            try
            {
                var employee = new Employee {Name = employeeName, ID = -1};
                employee = new BllEmployee().Get(employee);
                return new ResultEntity<Employee>
                {
                    ErrorMessage = string.Empty,
                    ResultObject = employee,
                    State = ResultState.Success
                };
            }
            catch (Exception ex)
            {
                return new ResultEntity<Employee>
                {
                    ErrorMessage = ex.Message,
                    ResultObject = null,
                    State = ResultState.Fail
                };
            }
        }


        public ResultEntity<List<Employee>> AddEmployees(List<Employee> employees)
        {

            return LoadBusinessMethod(() => new BllEmployee().AddEmployees(employees));
        }

        public ResultEntity<List<Employee>> ModifyEmployees(List<Employee> employees)
        {
            return LoadBusinessMethod(() => new BllEmployee().ModifyEmployees(employees));
        }

        public ResultEntity<List<Employee>> DeleteEmployees(List<Employee> employees)
        {
            return LoadBusinessMethod(() => new BllEmployee().DeleteEmployees(employees));
        }



        protected ResultEntity<T> LoadBusinessMethod<T>(Func<T> methodHandler)
        {
            try
            {
                return new ResultEntity<T>
                {
                    ErrorMessage = string.Empty,
                    ResultObject = methodHandler(),
                    State = ResultState.Success
                };
            }
            catch (Exception ex)
            {
                return new ResultEntity<T>
                {
                    ErrorMessage = ex.Message,
                    ResultObject = default(T),
                    State = ResultState.Fail
                };
            }
        }


        public ResultEntity<List<Employee>> GetEmployees(List<Employee> employees)
        {
            return LoadBusinessMethod(() => new BllEmployee().Get(employees));
        }
    }
}
