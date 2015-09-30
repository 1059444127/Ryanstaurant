using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Ryanstaurant.GMS.Interface
{
    [ServiceContract]
    public interface IGMSService
    {
        [OperationContract]
        string Login(string userName, string password);

        [OperationContract]
        string ValidToken(string userToken);

        [OperationContract]
        string GetConfig(string shortCall);

    }
}
