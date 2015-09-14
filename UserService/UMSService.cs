using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.Interface;
using Ryanstaurant.UMS.WorkSpace;


namespace Ryanstaurant.UMS.Service
{
    public class UMSService:IUMSService
    {


        protected ResultEntity LoadBusinessMethod(Func<List<ItemContent>> methodHandler)
        {
            try
            {
                var tokenIndex = OperationContext.Current.IncomingMessageHeaders.FindHeader("SessionToken",
                    "Ryanstaurant.UMS");

                if (tokenIndex == -1)
                {
                    return new ResultEntity
                    {
                        InnerErrorMessage = string.Empty,
                        ErrorMessage = "协议中没有令牌内容，无法执行操作\r\n如需要令牌，请使用LOGIN接口进行登录",
                        ResultObject = new List<ItemContent>(),
                        State = ResultState.Fail
                    };
                }


                var sessionToken = OperationContext.Current.IncomingMessageHeaders[tokenIndex] == null ? string.Empty : OperationContext.Current.IncomingMessageHeaders[tokenIndex].ToString();
                string exception;
                if (!new BllLogin().ValidToken(sessionToken, out exception))
                {
                    return new ResultEntity
                    {
                        InnerErrorMessage = string.Empty,
                        ErrorMessage = exception,
                        ResultObject = new List<ItemContent>(),
                        State = ResultState.Fail
                    };
                }

                return new ResultEntity
                {
                    ErrorMessage = string.Empty,
                    ResultObject = methodHandler(),
                    State = ResultState.Success
                };
            }
            catch (Exception ex)
            {
                return new ResultEntity
                {
                    InnerErrorMessage = ex.InnerException == null ? string.Empty : ex.InnerException.Message,
                    ErrorMessage = ex.Message,
                    ResultObject = new List<ItemContent>(),
                    State = ResultState.Fail
                };
            }
        }




        public ResultEntity Execute(List<ItemContent> requestEntitiy)
        {
            return LoadBusinessMethod(() =>
            {
                var sampleObject = requestEntitiy.FirstOrDefault();
                if (sampleObject == null)
                {
                    throw new Exception("错误的参数", new Exception("RequestContents没有元素"));
                }


                var requestTypeName = sampleObject.GetType().Name;
                var bllModule = Assembly.Load("Ryanstaurant.UMS.WorkSpace").CreateInstance("Ryanstaurant.UMS.WorkSpace.Bll" + requestTypeName);

                if (bllModule == null)
                {
                    throw new Exception("没有找到相关模块", new Exception(requestTypeName));
                }

                return bllModule.GetType()
                    .GetMethod("Execute" + requestTypeName)
                    .Invoke(bllModule, new object[] {requestEntitiy}) as List<ItemContent>;
            });
        }

        public ResultEntity Query(List<ItemContent> requestEntitiy)
        {
            return LoadBusinessMethod(() =>
            {
                var sampleObject = requestEntitiy.FirstOrDefault();
                if (sampleObject == null)
                {
                    throw new Exception("错误的参数", new Exception("RequestContents没有元素"));
                }


                var requestTypeName = sampleObject.GetType().Name;
                var bllModule = Assembly.Load("Ryanstaurant.UMS.WorkSpace").CreateInstance("Ryanstaurant.UMS.WorkSpace.Bll" + requestTypeName);

                if (bllModule == null)
                {
                    throw new Exception("没有找到相关模块", new Exception(requestTypeName));
                }


                if (bllModule.GetType()
                    .GetMethod("Query" + requestTypeName) == null)
                {
                    throw new Exception("没有找到相关方法", new Exception(requestTypeName));
                }

                return bllModule.GetType()
                    .GetMethod("Query" + requestTypeName)
                    .Invoke(bllModule, new object[] { requestEntitiy }) as List<ItemContent>;
            });
        }


        public ResultEntity Login(string userName, string password)
        {
            try
            {
                var result = new BllLogin().GetToken(userName, password);

                if (string.IsNullOrEmpty(result))
                    return new ResultEntity
                    {
                        InnerErrorMessage = "Login",
                        ErrorMessage = "登录失败",
                        ResultObject = null ,
                        State = ResultState.Fail
                    };

                return new ResultEntity
                    {
                        InnerErrorMessage = string.Empty,
                        ErrorMessage = string.Empty,
                        ResultObject = new List<ItemContent>
                        {
                            new Login
                            {
                                SessionToken=result,
                            }
                        } ,
                        State = ResultState.Success
                    };
            }
            catch (Exception ex)
            {
                return new ResultEntity
                {
                    InnerErrorMessage = ex.InnerException == null ? string.Empty : ex.InnerException.Message,
                    ErrorMessage = ex.Message,
                    ResultObject = new List<ItemContent>(),
                    State = ResultState.Fail
                };
            }
        }
    }
}
