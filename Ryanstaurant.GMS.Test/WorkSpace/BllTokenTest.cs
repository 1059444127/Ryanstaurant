using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ryanstaurant.GMS.DataAccess.EF;
using Ryanstaurant.GMS.WorkSpace;

namespace Ryanstaurant.GMS.Test.WorkSpace
{
    [TestClass]
    public class BllTokenTest
    {
        [TestMethod]
        public void TestLogin()
        {
            var bll =new BllToken(new GmsEntity());
            var result = bll.Login("Ryan", "123456");
            var guid = Guid.Empty;

            GenerateKeys();

            Assert.IsTrue(Guid.TryParse(result,out guid));
            Assert.AreEqual(guid.ToString().ToUpper(), result.ToUpper());

        }



        public static void GenerateKeys()
        {
            string[] sKeys = new String[2];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            sKeys[0] = rsa.ToXmlString(true);
            sKeys[1] = rsa.ToXmlString(false);


            string plaintext = "a08cc634-1cd3-4e97-802f-5a0e0beb30a5";
            rsa.FromXmlString("<RSAKeyValue><Modulus>kE0wvy74FfVhfX6/g/e9nI+9uMinpNtgEfvaYEopC2FBnZ41IRjFZkryC1v51gPrsLR3HhI5pUN55jmvSt5eb0Jo0DhgEkKBTmPr3p6tIW8zT+94n4Cd7xwM/ebMjelIV7FaoFCqKlIcC+EOl1GjBmkNMYZlSdl6DBk4UPDH1rM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
            byte[] cipherbytes;
            byte[] byteEn = rsa.Encrypt(Encoding.UTF8.GetBytes("a"), false);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(plaintext), false);



            StringBuilder sbString = new StringBuilder();
            for (int i = 0; i < cipherbytes.Length; i++)
            {
                sbString.Append(cipherbytes[i] + ",");
            }

            sbString.ToString();

            Decrypte();
        }




        public static void Decrypte()
        {
             RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
             rsa.FromXmlString(@"<RSAKeyValue><Modulus>kE0wvy74FfVhfX6/g/e9nI+9uMinpNtgEfvaYEopC2FBnZ41IRjFZkryC1v51gPrsLR3HhI5pUN55jmvSt5eb0Jo0DhgEkKBTmPr3p6tIW8zT+94n4Cd7xwM/ebMjelIV7FaoFCqKlIcC+EOl1GjBmkNMYZlSdl6DBk4UPDH1rM=</Modulus><Exponent>AQAB</Exponent><P>wT4r9/eFiHUXsi46NI6wg8nSMa4ENoCNOrTcU1j4h4lA7WLBhJRnhomefkawoqCASJ/mMTSLEw3I45BDLslg7w==</P><Q>vyoiGhsaWmO69Kl1KtgoBI5D5ADWqNV3cmhUhltvnzfkkOeqDDIlQebxIDKs2R5MM3os4HueTiJesa/u22CefQ==</Q><DP>KKiJHcIxkVV5p968FmTTVNc454UCk+kWTfzRwqOcnv/M7mVx7BBBe/gmpdD/xyfX9e/WwhGZFWg4FVE8IXLzdw==</DP><DQ>g3boSOfGsc7QLZ20NCi/LJLh4ZUqCqXzJTzWlCehle+MQpLvAMAjmzTFMo1nDVGmuegVoLOi5L0CPlRtgXTqyQ==</DQ><InverseQ>vmUWP5omXlysoPz1bFMBDCg7qnCytT2DgdqjeyEEeNVjHWtXx0IKYG5r3DLpuS1yEMZR49ekC/5D4Wfyb4hTGw==</InverseQ><D>Jb3asxZg0rV0QzOEecqxMCK0V4E7v7WVAf8iVa7v30cXt1bqxHZLIv8VIX1z8dLisTvyL85Kf3wPhPSvcWCJyCFGqsPD29dDJOIGRnlBM84VpqLcz+3y9lD/rVzwZOflHm8WnQVXThv02zI6/35/C9t6lwulWBG+GeOeYM6ml+k=</D></RSAKeyValue>");
            byte[] byteEn = rsa.Encrypt(Encoding.UTF8.GetBytes("a"), false);
            string[] sBytes = "132,104,240,244,162,215,98,133,235,0,186,131,17,113,68,95,45,64,25,169,29,82,96,62,182,217,250,56,179,122,48,222,208,157,115,181,191,80,215,22,157,249,176,137,22,190,58,211,69,27,98,106,82,16,238,3,2,152,196,221,188,185,152,80,182,115,179,234,17,188,113,5,26,205,200,146,82,112,245,209,191,2,131,139,63,71,244,39,187,244,167,133,25,231,92,135,175,44,19,149,47,229,21,25,84,0,54,245,77,46,250,170,71,175,98,7,38,217,142,65,74,244,23,75,35,154,214,226,".Split(',');

 

            for (int j = 0; j < sBytes.Length; j++)
            {
                if (sBytes[j] != "")
                {
                    byteEn[j] = Byte.Parse(sBytes[j]);
                }
            }
            byte[] plaintbytes = rsa.Decrypt(byteEn, false);
            var test =  Encoding.UTF8.GetString(plaintbytes);
        }



    }
}
