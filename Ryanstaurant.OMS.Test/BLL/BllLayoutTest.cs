using System;
using System.Data.Entity;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ryanstaurant.OMS.DataAccess;
using Ryanstaurant.OMS.DataContract;
using Ryanstaurant.OMS.WorkSpace;

namespace Ryanstaurant.OMS.Test
{
    /// <summary>
    /// BllLayoutTest 的摘要说明
    /// </summary>
    [TestClass]
    public class BllLayoutTest
    {
        public BllLayoutTest()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
        public void TestGetTables()
        {
            var data = new List<Tables>
            {
                new Tables
                {
                    ID = Guid.Parse("8C536DBB-F806-4AAD-A0F1-A43C3E686C54"),
                    CurrentStatus = 1,
                    Disabled = 0,
                    DisplayNo = "1",
                    Length = 50,
                    PosX = 0,
                    PosY = 0,
                    Width = 50
                },
                new Tables
                {
                    ID = Guid.Parse("8C536DBB-F806-4AAD-A0F1-A43C3E686C55"),
                    CurrentStatus = 1,
                    Disabled = 0,
                    DisplayNo = "2",
                    Length = 50,
                    PosX = 0,
                    PosY = 0,
                    Width = 50
                },
                new Tables
                {
                    ID = Guid.Parse("8C536DBB-F806-4AAD-A0F1-A43C3E686C56"),
                    CurrentStatus = 1,
                    Disabled = 0,
                    DisplayNo = "3",
                    Length = 50,
                    PosX = 0,
                    PosY = 0,
                    Width = 50
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Tables>>();
            mockSet.As<IQueryable<Tables>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tables>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tables>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tables>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<OmsEntity>();
            mockContext.Setup(c => c.Tables).Returns(mockSet.Object);


            var bll = new BllLayout(mockContext.Object);
            var tables = bll.GetTables(null);

            Assert.AreEqual(3, tables.Count);
            Assert.AreEqual("8C536DBB-F806-4AAD-A0F1-A43C3E686C54", tables[0].ID.ToUpper());
            Assert.AreEqual("8C536DBB-F806-4AAD-A0F1-A43C3E686C55", tables[1].ID.ToUpper());
            Assert.AreEqual("8C536DBB-F806-4AAD-A0F1-A43C3E686C56", tables[2].ID.ToUpper()); 

        }


        [TestMethod]
        public void SaveTables_Add()
        {
            IList<Table> tables = new List<Table>
            {
                Table.GetDefault("8C536DBB-F806-4AAD-A0F1-A43C3E686C54","1"),
                Table.GetDefault("8C536DBB-F806-4AAD-A0F1-A43C3E686C55","2"),
                Table.GetDefault("8C536DBB-F806-4AAD-A0F1-A43C3E686C56","3")
            };



            var data = new List<Tables>().AsQueryable();

            var mockSet = new Mock<DbSet<Tables>>();
            mockSet.As<IQueryable<Tables>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tables>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tables>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tables>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var mockContext = new Mock<OmsEntity>();
            mockContext.Setup(c => c.Tables).Returns(mockSet.Object);


            var bll = new BllLayout(mockContext.Object);
             bll.SaveTables(tables);


            mockSet.Verify(m => m.Add(It.IsAny<Tables>()), Times.AtLeastOnce);
            mockContext.Verify(m => m.SaveChanges(), Times.AtLeastOnce()); 

        }



        [TestMethod]
        public void CombineTable()
        {
            IList<Table> tables = new List<Table>
            {
                Table.GetDefault("8C536DBB-F806-4AAD-A0F1-A43C3E686C54","1"),
                Table.GetDefault("8C536DBB-F806-4AAD-A0F1-A43C3E686C55","2"),
                Table.GetDefault("8C536DBB-F806-4AAD-A0F1-A43C3E686C56","3")
            };

            var combtable = Table.GetDefault("8C536DBB-F806-4AAD-A0F1-A43C3E686C57", "4");


            var data = new List<TableRelations>().AsQueryable();

            var mockSet = new Mock<DbSet<TableRelations>>();
            mockSet.As<IQueryable<TableRelations>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TableRelations>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TableRelations>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TableRelations>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var data1 = new List<Tables>
            {
                new Tables
                {
                    ID = Guid.Parse("8C536DBB-F806-4AAD-A0F1-A43C3E686C54"),
                    CurrentStatus = 1,
                    Disabled = 0,
                    DisplayNo = "1",
                    Length = 50,
                    PosX = 0,
                    PosY = 0,
                    Width = 50
                },

                new Tables
                {
                    ID = Guid.Parse("8C536DBB-F806-4AAD-A0F1-A43C3E686C55"),
                    CurrentStatus = 1,
                    Disabled = 0,
                    DisplayNo = "2",
                    Length = 50,
                    PosX = 0,
                    PosY = 0,
                    Width = 50
                },
                new Tables
                {
                    ID = Guid.Parse("8C536DBB-F806-4AAD-A0F1-A43C3E686C56"),
                    CurrentStatus = 1,
                    Disabled = 0,
                    DisplayNo = "3",
                    Length = 50,
                    PosX = 0,
                    PosY = 0,
                    Width = 50
                }
            }.AsQueryable();

            var mockSet1 = new Mock<DbSet<Tables>>();
            mockSet1.As<IQueryable<Tables>>().Setup(m => m.Provider).Returns(data1.Provider);
            mockSet1.As<IQueryable<Tables>>().Setup(m => m.Expression).Returns(data1.Expression);
            mockSet1.As<IQueryable<Tables>>().Setup(m => m.ElementType).Returns(data1.ElementType);
            mockSet1.As<IQueryable<Tables>>().Setup(m => m.GetEnumerator()).Returns(data1.GetEnumerator());



            var mockContext = new Mock<OmsEntity>();
            mockContext.Setup(c => c.TableRelations).Returns(mockSet.Object);
            mockContext.Setup(c => c.Tables).Returns(mockSet1.Object);


            var bll = new BllLayout(mockContext.Object);
            bll.CombineTable(tables, combtable);

            mockSet1.Verify(m => m.Add(It.IsAny<Tables>()), Times.Once);
            mockSet.Verify(m => m.Add(It.IsAny<TableRelations>()), Times.Exactly(tables.Count));
            mockContext.Verify(m => m.SaveChanges(), Times.Once);

        }




    }
}
