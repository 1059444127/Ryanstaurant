using System.Collections.Generic;
using System.ServiceModel;
using Ryanstaurant.Utility.DataContract;
using Ryanstaurant.UMS.DataContract;

namespace Ryanstaurant.UMS.Interface
{
    [ServiceContract]
    public interface IUMSService
    {
        [OperationContract]
        ResultEntity<Employee> GetEmployee(string employeeName);

        [OperationContract]
        ResultEntity<List<Employee>> AddEmployees(List<Employee> employees);

        [OperationContract]
        ResultEntity<List<Employee>> ModifyEmployees(List<Employee> employees);

        [OperationContract]
        ResultEntity<List<Employee>> DeleteEmployees(List<Employee> employees);


    }
}
