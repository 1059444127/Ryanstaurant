using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ryanstaurant.OMS.DataContract.Utility;

namespace Ryanstaurant.OMS.Test.DataContract
{
    /// <summary>
    /// DataContractAdaptorTest 的摘要说明
    /// </summary>
    [TestClass]
    public class DataContractAdaptorTest
    {
        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestToEntity()
        {
            var testdata = new TestDataContract
            {
                Amout = "1",
                Change = "12.5",
                Description = "aaaaa",
                ID = "8C536DBB-F806-4AAD-A0F1-A43C3E686C54",
                Time = DateTime.Now.ToString("2000-01-01"),
                Record = 1
            };
            

            var testEntity = DataContractAdaptor.ConvertTo<TestEntity>(testdata, testdata.GetType());

            Assert.AreEqual(1, testEntity.Amout);
            Assert.AreEqual(12.5M, testEntity.Change);
            Assert.AreEqual("aaaaa", testEntity.Description);
            Assert.AreEqual(Guid.Parse("8C536DBB-F806-4AAD-A0F1-A43C3E686C54"), testEntity.ID);
            Assert.AreEqual(DateTime.Parse("2000-01-01"), testEntity.TimeStamp);
            

        }


        [TestMethod]
        public void TestLoadFrom()
        {
            var testdata = new TestEntity
            {
                Amout = 1,
                Change = 12.5M,
                Description = "aaaaa",
                ID = Guid.Parse("8C536DBB-F806-4AAD-A0F1-A43C3E686C54"),
                TimeStamp = DateTime.Parse("2000-01-01"),
                Record = 1
            };

            var testEntity = DataContractAdaptor.LoadFrom<TestDataContract>(testdata, testdata.GetType());

            Assert.AreEqual("1", testEntity.Amout);
            Assert.AreEqual("12.5", testEntity.Change);
            Assert.AreEqual("aaaaa", testEntity.Description);
            Assert.AreEqual("8C536DBB-F806-4AAD-A0F1-A43C3E686C54", testEntity.ID.ToUpper());
            Assert.AreEqual(DateTime.Parse("2000-01-01"),DateTime.Parse(testEntity.Time));
        }







    }


    public class TestEntity
    {
        public Guid ID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Description { get; set; }
        public int Amout { get; set; }
        public decimal Change { get; set; }
        public int Record { get; set; }
    }


    public class TestDataContract
    {
        [EntityMapping(MapName = "ID",MapType = typeof(Guid))]
        public string ID { get; set; }

        [EntityMapping(MapName = "TimeStamp", MapType = typeof(DateTime))]
        public string Time { get; set; }

        [EntityMapping(MapName = "Description", MapType = typeof(string))]
        public string Description { get; set; }

        [EntityMapping(MapName = "Amout", MapType = typeof(int))]
        public string Amout { get; set; }

        [EntityMapping(MapName = "Change", MapType = typeof(decimal))]
        public string Change { get; set; }

        [EntityMapping]
        public int Record { get; set; }
    }



}
