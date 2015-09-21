using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.Client
{
    internal class ServiceClient : ClientBase<IUMSService>,IUMSService
    {
        public ServiceClient(Binding binding, EndpointAddress edpAddr)
            : base(binding, edpAddr)
        {
        }


        public ResultEntity Execute(RequestEntity requestEntitiy)
        {
            return Channel.Execute(requestEntitiy);
        }

        public ResultEntity Query(RequestEntity requestEntitiy)
        {
            return Channel.Query(requestEntitiy);
        }

        public ResultEntity Login(string userName, string password)
        {
            return Channel.Login(userName, password);
        }
    }
}
