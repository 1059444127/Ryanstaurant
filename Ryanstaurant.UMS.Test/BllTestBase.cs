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
        protected UmsEntities Entities;


        [TestInitialize]
        public void Initial()
        {
            Entities = new UmsEntities();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Entities.Dispose();
        }




    }
}
