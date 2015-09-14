using System.Collections.Generic;
using System.ServiceModel;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.Interface
{
    [ServiceContract]
    public interface IUMSService
    {
        [OperationContract]
        ResultEntity Execute(List<ItemContent> requestEntitiy);

        [OperationContract]
        ResultEntity Query(List<ItemContent> requestEntitiy);

        [OperationContract]
        ResultEntity Login(string userName, string password);

    }
}
