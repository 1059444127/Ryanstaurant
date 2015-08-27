using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ryanstaurant.UMS.DAL;
using Ryanstaurant.UMS.Entity;

namespace UnitTestDAL
{
    [TestClass]
    public class DalAuthorityTest
    {
        [TestMethod]
        public void TestGetMethod()
        {
            var entities = new DalAuthority().Get(new List<Authority>
            {
                new Authority
                {
                    ID = -1,
                    Name = "RYAN"
                }
            });

            Assert.AreEqual(1, entities.FirstOrDefault().ID);
            Assert.AreEqual("RYAN", entities.FirstOrDefault().Name);
        }


        public void TestAddMethod()
        {
            var entities = new DalAuthority().Add(new List<Authority>
            {
                new Authority
                {
                    Name = "TESTUSER",
                    Description = "delete me"
                }
            });

            Assert.AreEqual(string.Empty, entities.FirstOrDefault().Exception,"保存操作错误");

            var findEntities = new DalAuthority().Get(new List<Authority>
            {
                new Authority
                {
                    ID = -1,
                    Name = "TESTUSER"
                }
            });

            Assert.AreEqual(entities.FirstOrDefault().ID, entities.FirstOrDefault().ID, "保存返回ID错误");


        }




    }
}
