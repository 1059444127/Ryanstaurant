using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ryanstaurant.UMS.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ryanstaurant.UMS.Entity;

namespace Ryanstaurant.UMS.DAL.Tests
{
    [TestClass()]
    public class DalAuthorityTests
    {
        [TestMethod()]
        public void GetTest()
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
    }
}
