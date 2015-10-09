using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ryanstaurant.UMS.DataAccess;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.WorkSpace;

namespace Ryanstaurant.UMS.Test.WorkSpace
{
    [TestClass]
    public class BllTokenTest
    {
        [TestMethod]
        public void NewTokenTest()
        {
            IList<UMS_Token> tables = new List<UMS_Token>();
            var data = new List<UMS_Token>().AsQueryable();
            var mockSet = new Mock<DbSet<UMS_Token>>();
            mockSet.As<IQueryable<UMS_Token>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<UMS_Token>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<UMS_Token>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<UMS_Token>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<UmsEntity>();
            mockContext.Setup(c => c.UMS_Tokens).Returns(mockSet.Object);
            var bll = new BllToken {Entity = mockContext.Object};
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(@"<RSAKeyValue><Modulus>kE0wvy74FfVhfX6/g/e9nI+9uMinpNtgEfvaYEopC2FBnZ41IRjFZkryC1v51gPrsLR3HhI5pUN55jmvSt5eb0Jo0DhgEkKBTmPr3p6tIW8zT+94n4Cd7xwM/ebMjelIV7FaoFCqKlIcC+EOl1GjBmkNMYZlSdl6DBk4UPDH1rM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
            var result = bll.NewToken("a08cc634-1cd3-4e97-802f-5a0e0beb30a6", rsa.Encrypt(Encoding.UTF8.GetBytes("a08cc634-1cd3-4e97-802f-5a0e0beb30a5"), false));
            mockSet.Verify(m => m.Add(It.IsAny<UMS_Token>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
            Assert.IsTrue(result);
        }


    }
}
