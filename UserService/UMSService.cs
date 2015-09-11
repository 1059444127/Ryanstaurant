using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.Interface;


namespace Ryanstaurant.UMS.Service
{
    public class UMSService:IUMSService
    {


        protected ResultEntity LoadBusinessMethod(Func<List<ItemContent>> methodHandler)
        {
            try
            {
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

                return bllModule.GetType()
                    .GetMethod("Query" + requestTypeName)
                    .Invoke(bllModule, new object[] { requestEntitiy }) as List<ItemContent>;
            });
        }
    }
}
