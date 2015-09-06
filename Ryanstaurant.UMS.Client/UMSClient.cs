using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.Client
{
    public class UMSClient:IUMSService
    {
        private readonly ServiceClient _serviceClient;


        public UMSClient(string endpointAddress)
        {
            Binding binding;
            if (endpointAddress.StartsWith("http://"))
            {
                binding = new BasicHttpBinding();
            }
            else if (endpointAddress.StartsWith("net.tcp://"))
            {
                binding = new NetTcpBinding();
            }
            else
            {
                throw new Exception("Invalid Address Format!");
            }

            _serviceClient = new ServiceClient(binding, new EndpointAddress(endpointAddress));
        }





        public DataContract.Utility.ResultEntity Execute(List<DataContract.Utility.ItemContent> requestEntitiy)
        {
            return _serviceClient.Execute(requestEntitiy);
        }

        public DataContract.Utility.ResultEntity Query(List<DataContract.Utility.ItemContent> requestEntitiy)
        {
            return _serviceClient.Query(requestEntitiy);
        }
    }
}
