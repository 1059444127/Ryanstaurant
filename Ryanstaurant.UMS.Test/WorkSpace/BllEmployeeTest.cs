using System.Collections.Generic;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.WorkSpace;

namespace Ryanstaurant.UMS.Test
{
    [TestClass]
    public class BllEmployeeTest:BllTestBase
    {

        [TestMethod]
        public void QueryEmployeeTestSuccess()
        {
            using (var trans = new TransactionScope())
            {
                var bllEmployee = new BllEmployee { Entities = base.Entities };
                //var result = bllEmployee.QueryEmployee(null);

                //Assert.AreEqual(ResultState.Fail, result[0].ResultInfo.State, "返回了错误的状态:" + result[0].ResultInfo.Exception);
                //result = bllEmployee.QueryEmployee(new List<Employee>().Cast<ItemContent>().ToList());
                //Assert.AreEqual(ResultState.Fail, result[0].ResultInfo.State, "返回了错误的状态:" + result[0].ResultInfo.Exception);
                var result = bllEmployee.QueryEmployee(new List<ItemContent>
                {
                    new Employee
                    {
                        ID = 1,
                        CommandInfo=new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    },
                    new Employee
                    {
                        Name = "RYAN",
                        CommandInfo=new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });

                Assert.AreEqual(1, result.Count, "返回了错误的查询个数:" + result.Count);

                Assert.AreEqual(1, (result[0] as Employee).ID, "返回了错误的ID:" + (result[0] as Employee).ID);

                trans.Dispose();
            }




        }


        [TestMethod]
        public void ExecuteEmployeeAddTest()
        {
            using (var trans = new TransactionScope())
            {
                var bllEmployee = new BllEmployee { Entities = base.Entities };

                var result = bllEmployee.ExecuteEmployee(new List<ItemContent>
                {
                    new Employee
                    {
                        ID = 4,
                        Name = "TestAdd",
                        Description = "TestAdd",
                        EmpAuthority = 7,
                        LoginName = "USER3",
                        Password = "3",
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
        public void ExecuteEmployeeModifyTest()
        {
            using (var trans = new TransactionScope())
            {
                var bllEmployee = new BllEmployee { Entities = base.Entities };
                var aimEmployee = bllEmployee.QueryEmployee(new List<ItemContent>
                {
                    new Employee
                    {
                        ID = 16,
                        Name = "11",
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });


                var targetEmployee = aimEmployee[0] as Employee;
                targetEmployee.Password = "*******";
                targetEmployee.SetModifyState();

                var result = bllEmployee.ExecuteEmployee(new List<ItemContent>
                {
                   targetEmployee
                });

                aimEmployee = bllEmployee.QueryEmployee(new List<ItemContent>
                {
                    new Employee
                    {
                        ID = 16,
                        Name = "11",
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });

                Assert.AreEqual(ResultState.Success, result[0].CommandInfo.State,
                    "返回了错误的修改状态:" + result[0].CommandInfo.Exception);

                Assert.AreEqual(1, result.Count, "返回了错误的查询个数:" + result.Count);
                Assert.AreEqual("*******", (aimEmployee[0] as Employee).Password,
                    "返回了错误的修改信息:" + (aimEmployee[0] as Employee).Password);

                trans.Dispose();
            }
        }

        [TestMethod]
        public void ExecuteEmployeeDeleteTest()
        {
            using (var trans = new TransactionScope())
            {
                var bllEmployee = new BllEmployee { Entities = base.Entities };
                var aimEmployee = bllEmployee.QueryEmployee(new List<ItemContent>
                {
                    new Employee
                    {
                        ID = 16,
                        Name = "11",
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });


                var targetEmployee = aimEmployee[0] as Employee;
                targetEmployee.SetDeleteState();

                var result = bllEmployee.ExecuteEmployee(new List<ItemContent>
                {
                   targetEmployee
                });

                aimEmployee = bllEmployee.QueryEmployee(new List<ItemContent>
                {
                    new Employee
                    {
                        ID = 16,
                        Name = "11",
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query
                        }
                    }
                });

                Assert.AreEqual(1, result.Count, "返回了错误的查询个数:" + result.Count);
                Assert.AreEqual(ResultState.Success, result[0].CommandInfo.State,
                    "返回了错误的删除状态:" + result[0].CommandInfo.Exception);
                Assert.AreEqual(0, aimEmployee.Count, "返回了错误的查询个数:" + result.Count);

                trans.Dispose();
            }
        }
    }
}
