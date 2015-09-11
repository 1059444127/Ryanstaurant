using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Ryanstaurant.UMS.Service
{
    public class WcfServiceMessageInspector:IDispatchMessageInspector
    {

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            Console.WriteLine("服务器端：接收到的请求：\n{0}", request.ToString());
            return null;  
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            Console.WriteLine("服务器即将作出以下回复：\n{0}", reply.ToString()); 
        }
    }
}
