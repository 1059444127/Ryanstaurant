using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Ryanstaurant.UMS.Client
{
    public class WcfClientInterpector : IClientMessageInspector  
    {

        public string UserName { get; set; }

        public string Password { get; set; }


        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            Console.WriteLine("客户端接收到的回复：\n{0}", reply.ToString());  
        }

        public object BeforeSendRequest(ref Message request, System.ServiceModel.IClientChannel channel)
        {
            var userNameHeader = MessageHeader.CreateHeader("OperationUserName", "http://tempuri.org", UserName, false, "");
            var pwdNameHeader = MessageHeader.CreateHeader("OperationPwd", "http://tempuri.org", Password, false, "");
            request.Headers.Add(userNameHeader);
            request.Headers.Add(pwdNameHeader);
            Console.WriteLine("客户端发送请求前的SOAP消息：\n{0}", request.ToString());
            return null;  
        }
    }
}
