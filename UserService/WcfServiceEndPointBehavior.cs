using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;

namespace Ryanstaurant.UMS.Service
{
    public class WcfServiceEndPointBehavior:IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            // 植入“偷听器” 服务器端  
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new WcfServiceMessageInspector()); 
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
