using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.Client
{
    public class UMSClient:IUMSClient
    {
        private ServiceClient _serviceClient;


        public ServiceClient ServiceClient
        {
            get
            {
                return _serviceClient;
            }
            set
            {
                _serviceClient = value;
            }
        }



        public string SessionToken { get; protected set; }

        public string ErrorMessage { get; private set; }

        public ResultState State { get; private set; }

        public string InnerErrorMessage { get; private set; }



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



        protected List<ItemContent> LoadServiceMethod(List<ItemContent> requestObjects, Func<RequestEntity, ResultEntity> serviceMethodHandler)
        {
            var request = new RequestEntity
            {
                RequestObjects = requestObjects,
                SessionToken = SessionToken
            };

            var result = serviceMethodHandler(request);
            ErrorMessage = result.ErrorMessage;
            InnerErrorMessage = result.InnerErrorMessage;
            SessionToken = result.SessionToken;
            State = result.State;
            return result.ResultObject;
        }



        public List<ItemContent> Execute(List<ItemContent> requestObjects)
        {
            return LoadServiceMethod(requestObjects, _serviceClient.Execute);

        }

        public List<ItemContent> Query(List<ItemContent> requestObjects)
        {
            return LoadServiceMethod(requestObjects, _serviceClient.Query);
        }


        public List<ItemContent> GetAllEmployees()
        {
            return LoadServiceMethod(new List<ItemContent>
            {
                new Employee
                {
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Query
                    }
                }
            }, _serviceClient.Query);

        }
        public List<ItemContent> GetAllAuthorities()
        {
            return LoadServiceMethod(new List<ItemContent>
            {
                new Authority
                {
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Query
                    }
                }
            }, _serviceClient.Query);
        }

        public List<ItemContent> GetAllRoles()
        {
            return LoadServiceMethod(new List<ItemContent>
            {
                new Role
                {
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Query
                    }
                }
            }, _serviceClient.Query);
        }

        public bool Login(string userName, string password)
        {
            var result = _serviceClient.Login(userName, password);
            return !string.IsNullOrEmpty(result.SessionToken);
        }


        public bool NewToken(string tokenKey, byte[] clientTokenBytes)
        {
            return _serviceClient.NewToken(tokenKey, clientTokenBytes);
        }


    }
}
