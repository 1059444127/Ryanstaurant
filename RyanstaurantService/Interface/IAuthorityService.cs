using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using RyanstaurantDataContract;

namespace RyanstaurantService.Interface
{
    public interface IAuthorityService
    {
        [OperationContract]
        EmployeeEntity GetEmployee(string employeeName);


    }
}
