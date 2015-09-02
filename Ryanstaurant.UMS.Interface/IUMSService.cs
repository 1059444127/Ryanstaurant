using System.Collections.Generic;
using System.ServiceModel;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.Interface
{
    [ServiceContract]
    public interface IUMSService
    {
        [OperationContract]
        ResultEntity<Employee> ExecuteEmployees(RequestEntitiy<Employee> employees);

        [OperationContract]
        ResultEntity<Employee> QueryEmployees(RequestEntitiy<Employee> employees);
            
            
            


    }
}
