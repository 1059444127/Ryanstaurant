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
    public class BllEmployeeTest
    {

        private UmsEntities entities;

        [TestInitialize]
        public void Initial()
        {
            entities = new UmsEntities();
        }

        [TestCleanup]
        public void CleanUp()
        {
            entities.Dispose();
        }



        [TestMethod]
        public void QueryEmployeeTestFail()
        {
            using (var trans = new TransactionScope())
            {
                var bllEmployee = new BllEmployee();
                //var result = bllEmployee.QueryEmployee(null);

                //Assert.AreEqual(ResultState.Fail, result[0].ResultInfo.State, "返回了错误的状态:" + result[0].ResultInfo.Exception);
                //result = bllEmployee.QueryEmployee(new List<Employee>().Cast<ItemContent>().ToList());
                //Assert.AreEqual(ResultState.Fail, result[0].ResultInfo.State, "返回了错误的状态:" + result[0].ResultInfo.Exception);
                var result = bllEmployee.QueryEmployee(new List<ItemContent>
                {
                    new Employee
                    {
                        ID = 1,
                        RequestInfo=new RequestContent
                        {
                            Operation = RequestOperation.Query
                        }
                    },
                    new Employee
                    {
                        Name = "RYAN",
                        RequestInfo=new RequestContent
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







    }
}
