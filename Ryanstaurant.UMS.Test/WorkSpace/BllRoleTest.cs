using System.Collections.Generic;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.WorkSpace;

namespace Ryanstaurant.UMS.Test
{
    [TestClass]
    public class BllRoleTest:BllTestBase
    {
        [TestMethod]
        public void QueryRoleTest()
        {

            using (var trans = new TransactionScope())
            {
                var bllRole = new BllRole { Entities = base.Entities };
                var result = bllRole.QueryRole(new List<ItemContent>
                {
                    new Role
                    {
                        ID = 1,
                        CommandInfo=new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    },
                    new Role
                    {
                        Name = "ADMINISTRATOR",
                        CommandInfo=new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });

                Assert.AreEqual(1, result.Count, "返回了错误的查询个数:" + result.Count);

                Assert.AreEqual(1, (result[0] as Role).ID, "返回了错误的ID:" + (result[0] as Role).ID);

                trans.Dispose();
            }
        }


        [TestMethod]
        public void ExecuteRoleAddTest()
        {
            using (var trans = new TransactionScope())
            {
                var bllRole = new BllRole { Entities = base.Entities };

                var result = bllRole.ExecuteRole(new List<ItemContent>
                {
                    new Role
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
        public void ExecuteRoleModifyTest()
        {
            using (var trans = new TransactionScope())
            {
                var bllRole = new BllRole { Entities = base.Entities };
                var aimRole = bllRole.QueryRole(new List<ItemContent>
                {
                    new Role
                    {
                        ID = 7,
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });


                var targetRole = aimRole[0] as Role;
                targetRole.Description = "Chef1";
                targetRole.SetModifyState();

                var result = bllRole.ExecuteRole(new List<ItemContent>
                {
                   targetRole
                });

                aimRole = bllRole.QueryRole(new List<ItemContent>
                {
                    new Role
                    {
                        ID = 7,
                        Description = "Chef1",
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });

                Assert.AreEqual(ResultState.Success, result[0].CommandInfo.State,
                    "返回了错误的修改状态:" + result[0].CommandInfo.Exception);

                Assert.AreEqual(1, result.Count, "返回了错误的查询个数:" + result.Count);
                Assert.AreEqual("Chef1", (aimRole[0] as Role).Description,
                    "返回了错误的修改信息:" + (aimRole[0] as Role).Description);

                trans.Dispose();
            }
        }

        [TestMethod]
        public void ExecuteRoleDeleteTest()
        {
            using (var trans = new TransactionScope())
            {
                var bllRole = new BllRole { Entities = base.Entities };
                var aimRole = bllRole.QueryRole(new List<ItemContent>
                {
                    new Role
                    {
                        ID = 7,
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });


                var targetRole = aimRole[0] as Role;
                targetRole.SetDeleteState();

                var result = bllRole.ExecuteRole(new List<ItemContent>
                {
                   targetRole
                });

                aimRole = bllRole.QueryRole(new List<ItemContent>
                {
                    new Role
                    {
                        ID = 7,
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });

                Assert.AreEqual(1, result.Count, "返回了错误的查询个数:" + result.Count);
                Assert.AreEqual(ResultState.Success, result[0].CommandInfo.State,
                    "返回了错误的删除状态:" + result[0].CommandInfo.Exception);
                Assert.AreEqual(0, aimRole.Count, "返回了错误的查询个数:" + result.Count);

                trans.Dispose();
            }
        }

    }
}
