using System.Collections.Generic;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.WorkSpace;

namespace Ryanstaurant.UMS.Test
{
    [TestClass]
    public class BllAuthorityTest:BllTestBase
    {

        [TestMethod]
        public void QueryAuthorityTest()
        {

            using (var trans = new TransactionScope())
            {
                var bllAuth = new BllAuthority { Entities = base.Entities };
                var result = bllAuth.QueryAuthority(new List<ItemContent>
                {
                    new Authority
                    {
                        ID = 1,
                        CommandInfo= new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    },
                    new Authority
                    {
                        Name = "PrintCheck",
                        CommandInfo=new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });

                Assert.AreEqual(1, result.Count, "返回了错误的查询个数:" + result.Count);

                Assert.AreEqual(1, (result[0] as Authority).ID, "返回了错误的ID:" + (result[0] as Authority).ID);

                trans.Dispose();
            }
        }


        [TestMethod]
        public void ExecuteAuthorityAddTest()
        {
            using (var trans = new TransactionScope())
            {
                var bllAuth = new BllAuthority { Entities = base.Entities };

                var result = bllAuth.ExecuteAuthority(new List<ItemContent>
                {
                    new Authority
                    {
                        ID = 4,
                        Name = "TestAdd",
                        Description = "TestAdd",
                        CommandInfo =new CommandInformation
                        {
                            Operation = RequestOperation.Add
                        }
                    }
                });

                Assert.AreEqual(1, result.Count, "返回了错误的查询个数:" + result.Count);
                Assert.AreEqual(ResultState.Success, result[0].CommandInfo.State,
                    "返回了错误的添加状态:" + result[0].CommandInfo.Exception);
                trans.Dispose();
            }
        }


        [TestMethod]
        public void ExecuteAuthorityModifyTest()
        {
            using (var trans = new TransactionScope())
            {
                var bllAuth = new BllAuthority { Entities = base.Entities };
                var aimAuth = bllAuth.QueryAuthority(new List<ItemContent>
                {
                    new Authority
                    {
                        ID = 1,
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });


                var targetAuth = aimAuth[0] as Authority;
                targetAuth.Description = "1";
                targetAuth.SetModifyState();

                var result = bllAuth.ExecuteAuthority(new List<ItemContent>
                {
                   targetAuth
                });

                aimAuth = bllAuth.QueryAuthority(new List<ItemContent>
                {
                    new Authority
                    {
                        ID = 1,
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });

                Assert.AreEqual(ResultState.Success, result[0].CommandInfo.State,
                    "返回了错误的修改状态:" + result[0].CommandInfo.Exception);

                Assert.AreEqual(1, result.Count, "返回了错误的查询个数:" + result.Count);
                Assert.AreEqual("1", (aimAuth[0] as Authority).Description,
                    "返回了错误的修改信息:" + (aimAuth[0] as Authority).Description);

                trans.Dispose();
            }
        }

        [TestMethod]
        public void ExecuteAuthorityDeleteTest()
        {
            using (var trans = new TransactionScope())
            {
                var bllAuthority = new BllAuthority { Entities = base.Entities };
                var aimAuthority = bllAuthority.QueryAuthority(new List<ItemContent>
                {
                    new Authority
                    {
                        ID = 1,
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });


                var targetAuthority = aimAuthority[0] as Authority;
                targetAuthority.SetDeleteState();

                var result = bllAuthority.ExecuteAuthority(new List<ItemContent>
                {
                   targetAuthority
                });

                aimAuthority = bllAuthority.QueryAuthority(new List<ItemContent>
                {
                    new Authority
                    {
                        ID = 1,
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });

                Assert.AreEqual(1, result.Count, "返回了错误的查询个数:" + result.Count);
                Assert.AreEqual(ResultState.Success, result[0].CommandInfo.State,
                    "返回了错误的删除状态:" + result[0].CommandInfo.Exception);
                Assert.AreEqual(0, aimAuthority.Count, "返回了错误的查询个数:" + result.Count);

                trans.Dispose();
            }
        }





    }
}
