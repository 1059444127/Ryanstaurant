using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.Client
{
    public class UMSClient:IUMSService
    {
        private readonly ServiceClient _serviceClient;


        public UMSClient()
        {
            var endpointAddress = ConfigurationManager.AppSettings["UMSServiceAddress"];
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


        public ResultEntity Execute(List<ItemContent> requestEntitiy)
        {
            return _serviceClient.Execute(requestEntitiy);
        }

        public ResultEntity Query(List<ItemContent> requestEntitiy)
        {
            return _serviceClient.Query(requestEntitiy);
        }


        public ResultEntity GetAllEmployees()
        {
            return _serviceClient.Query(new List<ItemContent>
            {
                new Employee()
            });
        }
        public ResultEntity GetAllAuthorities()
        {
            return _serviceClient.Query(new List<ItemContent>
            {
                new Authority()
            });
        }

        public ResultEntity GetAllRoles()
        {
            return _serviceClient.Query(new List<ItemContent>
            {
                new Role()
            });
        }

        



    }
}
