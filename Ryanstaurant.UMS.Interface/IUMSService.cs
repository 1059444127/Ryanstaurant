using System.Collections.Generic;
using System.ServiceModel;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.Interface
{
    [ServiceContract]
    public interface IUMSService
    {
        [OperationContract]
        ResultEntity Execute(RequestEntity requestEntitiy);

        [OperationContract]
        ResultEntity Query(RequestEntity requestEntitiy);

        [OperationContract]
        ResultEntity Login(string userName, string password);


        [OperationContract]
        bool NewToken(string tokenKey, byte[] clientTokenBytes);

    }
}
