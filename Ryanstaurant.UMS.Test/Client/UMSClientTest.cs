using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ryanstaurant.UMS.Client;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.Test.Client
{
    [TestClass]
    public class UMSClientTest
    {
        private Mock<ServiceClient> umsServiceMock = new Mock<ServiceClient>();

        private readonly RequestEntity _normalQueryEmployeeEntity = new RequestEntity
        {
            RequestObjects = new List<ItemContent>
            {
                new Employee
                {
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Query
                    },
                    ID = 1
                }
            }
        };

        private readonly RequestEntity _failedQueryEmployeeEntity = new RequestEntity
        {
            RequestObjects = new List<ItemContent>
            {
                new Employee
                {
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Query
                    },
                    ID = 999
                }
            }
        };


        private readonly RequestEntity _normalExuteEmployeeEntity = new RequestEntity
        {
            RequestObjects = new List<ItemContent>
            {
                new Employee
                {
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Add
                    },
                    ID = 99
                }
            }
        };


        private readonly RequestEntity _failExuteEmployeeEntity = new RequestEntity
        {
            RequestObjects = new List<ItemContent>
            {
                new Employee
                {
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Add
                    },
                    ID = 1
                }
            }
        };



        private readonly ResultEntity _normalresultEntity = new ResultEntity
        {
            ResultObject = new List<ItemContent>
                {
                    new Employee
                    {
                        CommandInfo =new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        },
                        ID = 1,
                        LoginName = "RYAN",
                        Password = "123456",
                        EmpAuthority = 3,
                        Name = "RYAN",
                        Description = "ADMIN",
                        Roles = new List<Role>
                        {
                            new Role
                            {
                                Authority = 3,
                                Description = "ADMINISTRATOR",
                                ID = 1,
                                Name = "ADMINISTRATOR"
                            }
                        }
                    }
                }
        };

        private readonly ResultEntity _failresultEntity = new ResultEntity
        {
            ResultObject = new List<ItemContent>
            {
                new Employee
                {
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Query
                    },
                    ID = 1,
                    LoginName = "RYAN",
                    Password = "123456",
                    EmpAuthority = 3,
                    Name = "RYAN",
                    Description = "ADMIN",
                    Roles = new List<Role>
                    {
                        new Role
                        {
                            Authority = 3,
                            Description = "ADMINISTRATOR",
                            ID = 1,
                            Name = "ADMINISTRATOR"
                        }
                    }
                }
            },
            ErrorMessage = "ERROR"
        };






        [TestInitialize]
        public void Initial()
        {
            umsServiceMock.Setup(service => service.Execute(_normalExuteEmployeeEntity))
                .Returns(_normalresultEntity);
            umsServiceMock.Setup(service => 
                service.Query(_failedQueryEmployeeEntity)
                )
                .Returns(_failresultEntity);
            umsServiceMock.Setup(service => service.Execute(_failExuteEmployeeEntity))
                .Returns(_failresultEntity);
        }


        [TestMethod]
        public void QueryTest()
        {

            umsServiceMock.Setup(service => service.Query(_normalQueryEmployeeEntity))
                .Returns(_normalresultEntity);

            var umsClient = new UMSClient("http://www.baidu.com");
            var result = umsClient.Query(_normalQueryEmployeeEntity.RequestObjects);

            Assert.AreEqual((_normalresultEntity.ResultObject[0] as Employee).ID, (result[0] as Employee).ID,
                "正确操作返回了不同的ID");


            result = umsClient.Query(_failedQueryEmployeeEntity.RequestObjects);
            Assert.AreEqual((_failresultEntity.ResultObject[0] as Employee).ID, (result[0] as Employee).ID,
    "错误操作返回了不同的ID");
        }

        [TestMethod]
        public void ExecuteTest()
        {
            var umsClient = new UMSClient { ServiceClient = umsServiceMock.Object };
            var result = umsClient.Execute(_normalExuteEmployeeEntity.RequestObjects);

            Assert.AreEqual((_normalresultEntity.ResultObject[0] as Employee).ID, (result[0] as Employee).ID,
                "正确操作返回了不同的ID");


            result = umsClient.Execute(_failExuteEmployeeEntity.RequestObjects);
            Assert.AreEqual((_failresultEntity.ResultObject[0] as Employee).ID, (result[0] as Employee).ID,
    "错误操作返回了不同的ID");
        }

    }
}
