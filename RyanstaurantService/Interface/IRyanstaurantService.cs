using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using RyanstaurantDataContract;

namespace RyanstaurantService.Interface
{
    [ServiceContract]
    public interface IRyanstaurantService
    {
        

        [OperationContract]
        DataTable GetFloorLayout();

        [OperationContract]
        DataTable GetNavigations();
    }
}
