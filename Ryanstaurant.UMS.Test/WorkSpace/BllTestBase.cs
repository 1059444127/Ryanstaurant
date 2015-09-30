using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ryanstaurant.UMS.DataAccess.EF;

namespace Ryanstaurant.UMS.Test
{
    [TestClass]
    public class BllTestBase
    {
        protected UmsEntity Entities;


        [TestInitialize]
        public void Initial()
        {
            Entities = new UmsEntity();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Entities.Dispose();
        }




    }
}
