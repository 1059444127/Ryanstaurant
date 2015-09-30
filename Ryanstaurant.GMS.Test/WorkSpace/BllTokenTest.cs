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


            string plaintext = "a79d4359-360d-4c9e-8a1e-21678fc4f8d1";
            rsa.FromXmlString("<RSAKeyValue><Modulus>mwG+aunAtNGDAea4QTrCNAI+wqRUiH0/9Z/gbuC7bqGVCyaNAm1YkcrSfqrfRjJ/gjgZX/A8Z3wdYFNykaxQJm1ID/YIZ8xRXgkHQR26E/enpCdQx8QCytL3o7zZOBTYVstl1de14xKd0OUX2TJEsZOMONNPMEwlIHCGWMM6VAc=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");
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
            rsa.FromXmlString(@"<RSAKeyValue><Modulus>mwG+aunAtNGDAea4QTrCNAI+wqRUiH0/9Z/gbuC7bqGVCyaNAm1YkcrSfqrfRjJ/gjgZX/A8Z3wdYFNykaxQJm1ID/YIZ8xRXgkHQR26E/enpCdQx8QCytL3o7zZOBTYVstl1de14xKd0OUX2TJEsZOMONNPMEwlIHCGWMM6VAc=</Modulus><Exponent>AQAB</Exponent><P>1BnCfo8rDP6blpfKG/ArQJuNYptbuC/ysGVf9qi2+UQfwGyULXjgpxae/63/GLm3dxmoSgyqaRAZscLUCyuSYQ==</P><Q>uxbYNSBrClFDqHOv5PeHtuKW5Xzhe2IqiL4Whqux40od8pPeH35/YkIZN3qS+PMEKNjTE06AhZq7VmRvlRbPZw==</Q><DP>CkaBNK5s8IE5Om96HCZjggrHK7rJE8BG6qLOpl2AF81kGGdqOlW71Hx1EX/1dtYwfDWjAItiihp8lC8TJMNkoQ==</DP><DQ>Udvd/+jOTPZflx3/MTzJCdE82u54Lic2mxwo+QW40M1GKzRdtQQBMnnfVLHyCrEx+bldKY5ol4yrOVK6q84OMw==</DQ><InverseQ>b9OwhcldxFpGKIZ8sey7Xr6kiTTKuVoGkcp+Z+/IwVPJVr6E7wYUbqRYWIKqXwyKM38Z3k7m09ouFJRj2OBKfg==</InverseQ><D>fhNwBtOzyx6x+QTpDx00wkqlM3mnzBBbynPMf0LJENXaPWSQws2fgY2/oglna1g9f/QTJ4ZmCyHXXvbgs/28fhAqclSRm5gm7DJrWGtn7j/MgVZVTdbn3zvEaT1Fz6oQEtNV/B3okPokOkuW2Hp5PzJw9LoQzaBXjSgKVk3H8EE=</D></RSAKeyValue>");
            byte[] byteEn = rsa.Encrypt(Encoding.UTF8.GetBytes("a"), false);
            string[] sBytes = "120,55,55,240,107,102,216,173,166,23,107,202,185,191,196,225,33,143,54,20,205,57,125,175,237,61,148,164,55,228,169,164,239,196,161,128,46,173,32,142,81,188,244,146,197,179,101,53,216,236,25,249,162,190,148,33,255,25,118,183,49,6,71,226,3,203,142,189,66,22,109,117,114,1,46,221,232,65,182,218,7,17,55,141,168,214,230,144,74,179,60,131,117,10,167,49,138,118,223,150,159,192,125,243,181,250,75,191,249,75,64,77,201,9,70,181,32,182,77,78,174,47,194,154,23,138,243,69,".Split(',');

 

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
