using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.Interface;
using Ryanstaurant.UMS.WorkSpace;


namespace Ryanstaurant.UMS.Service
{
    public class UMSService:IUMSService
    {
        public ResultEntity<Employee> QueryEmployees(RequestEntitiy<Employee> employees)
        {
            return LoadBusinessMethod(() => new BllEmployee().QueryEmployees(employees));
        }



        public ResultEntity<Employee> ExecuteEmployees(RequestEntitiy<Employee> employees)
        {
            return LoadBusinessMethod(() => new BllEmployee().ExcuteEmployees(employees));
        }


        protected ResultEntity<T> LoadBusinessMethod<T>(Func<List<ResultContent<T>>> methodHandler)
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
                    InnerErrorMessage = ex.InnerException == null ? string.Empty : ex.InnerException.Message,
                    ErrorMessage = ex.Message,
                    ResultObject = new List<ResultContent<T>>(),
                    State = ResultState.Fail
                };
            }
        }
    }
}
