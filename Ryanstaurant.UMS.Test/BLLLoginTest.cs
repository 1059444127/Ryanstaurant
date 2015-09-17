using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.WorkSpace;

namespace Ryanstaurant.UMS.Test
{
    [TestClass]
    public class BLLLoginTest
    {
        private UmsEntities _entities;


        [TestInitialize]
        public void Initial()
        {
            _entities = new UmsEntities();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _entities.Dispose();
        }



        [TestMethod]
        public void TestGetTokenFail()
        {
            using (var transaction = new TransactionScope())
            {
                var login = new BllLogin {Entities = _entities};
                var result = login.GetToken("", "");
                Assert.IsTrue(string.IsNullOrEmpty(result), "错误的验证信息得到了有效的令牌");

                transaction.Dispose();
            }

        }
        
        
        [TestMethod]
        public void TestGetTokenSuccess()
        {
            using (var transaction = new TransactionScope())
            {
                var login = new BllLogin {Entities = _entities};

                var result = login.GetToken("RYAN", "123456");
                Assert.IsFalse(string.IsNullOrEmpty(result), "正确验证信息得到了错误的结果");

                transaction.Dispose();
            }

        }



        [TestMethod]
        public void TestValidTokenSuccess()
        {
            using (var transaction = new TransactionScope())
            {
                var login = new BllLogin { Entities = _entities };

                var result = login.GetToken("RYAN", "123456");

                string exception;
                var valid =login.ValidToken(result, out exception);
                Assert.IsTrue(valid, "正确的令牌[" + result + "]没有通过验证:" + exception);


                transaction.Dispose();
            }

        }

        [TestMethod]
        public void TestValidTokenFail()
        {
            using (var transaction = new TransactionScope())
            {
                var login = new BllLogin { Entities = _entities };

                var result = login.GetToken("", "");

                string exception;
                var valid = login.ValidToken(result, out exception);
                Assert.IsFalse(valid, "错误的令牌[" + result + "]却通过验证:" + exception);


                transaction.Dispose();
            }

        }


    }
}
