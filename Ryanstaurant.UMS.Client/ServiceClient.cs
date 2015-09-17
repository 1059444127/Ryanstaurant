using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.Client
{
    internal class ServiceClient : ClientBase<IUMSService>, IUMSService
    {
        public ServiceClient(Binding binding, EndpointAddress edpAddr)
            : base(binding, edpAddr)
        {
        }


        public string SessionToken { get; protected set; }



        public ResultEntity Execute(List<ItemContent> requestEntitiy)
        {
            OperationContext.Current.OutgoingMessageHeaders.Add(MessageHeader.CreateHeader("SessionToken",
                "Ryanstaurant.UMS", SessionToken));
            return Channel.Execute(requestEntitiy);
        }

        public ResultEntity Query(List<ItemContent> requestEntitiy)
        {
            OperationContext.Current.OutgoingMessageHeaders.Add(MessageHeader.CreateHeader("SessionToken",
                "Ryanstaurant.UMS", SessionToken));
            return Channel.Query(requestEntitiy);
        }


        public ResultEntity Login(string userName, string password)
        {
            var result = Channel.Login(userName, password);
            if (result.State == ResultState.Success)
            {
                var tokIndex = OperationContext.Current.OutgoingMessageHeaders.FindHeader("SessionToken",
                    "Ryanstaurant.UMS");

                if (tokIndex == -1)
                    return result;

                SessionToken = OperationContext.Current.OutgoingMessageHeaders[tokIndex].ToString();
            }


            return result;
        }
    }
}
