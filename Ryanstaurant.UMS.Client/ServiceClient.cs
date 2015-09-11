using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.Client
{
    internal class ServiceClient : ClientBase<IUMSService>, IUMSService
    {
        public ServiceClient(Binding binding, EndpointAddress edpAddr)
            : base(binding, edpAddr)
        {
        }


        public DataContract.Utility.ResultEntity Execute(List<DataContract.Utility.ItemContent> requestEntitiy)
        {
            return Channel.Execute(requestEntitiy);
        }

        public DataContract.Utility.ResultEntity Query(List<DataContract.Utility.ItemContent> requestEntitiy)
        {
            return Channel.Query(requestEntitiy);
        }
    }
}
