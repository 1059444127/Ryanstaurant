using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Ryanstaurant.OMS.DataAccess;

namespace Ryanstaurant.OMS.WorkSpace
{
    public class BllToken : BllBase
    {
        private const string PrivateKey =
            @"<RSAKeyValue><Modulus>mwG+aunAtNGDAea4QTrCNAI+wqRUiH0/9Z/gbuC7bqGVCyaNAm1YkcrSfqrfRjJ/gjgZX/A8Z3wdYFNykaxQJm1ID/YIZ8xRXgkHQR26E/enpCdQx8QCytL3o7zZOBTYVstl1de14xKd0OUX2TJEsZOMONNPMEwlIHCGWMM6VAc=</Modulus><Exponent>AQAB</Exponent><P>1BnCfo8rDP6blpfKG/ArQJuNYptbuC/ysGVf9qi2+UQfwGyULXjgpxae/63/GLm3dxmoSgyqaRAZscLUCyuSYQ==</P><Q>uxbYNSBrClFDqHOv5PeHtuKW5Xzhe2IqiL4Whqux40od8pPeH35/YkIZN3qS+PMEKNjTE06AhZq7VmRvlRbPZw==</Q><DP>CkaBNK5s8IE5Om96HCZjggrHK7rJE8BG6qLOpl2AF81kGGdqOlW71Hx1EX/1dtYwfDWjAItiihp8lC8TJMNkoQ==</DP><DQ>Udvd/+jOTPZflx3/MTzJCdE82u54Lic2mxwo+QW40M1GKzRdtQQBMnnfVLHyCrEx+bldKY5ol4yrOVK6q84OMw==</DQ><InverseQ>b9OwhcldxFpGKIZ8sey7Xr6kiTTKuVoGkcp+Z+/IwVPJVr6E7wYUbqRYWIKqXwyKM38Z3k7m09ouFJRj2OBKfg==</InverseQ><D>fhNwBtOzyx6x+QTpDx00wkqlM3mnzBBbynPMf0LJENXaPWSQws2fgY2/oglna1g9f/QTJ4ZmCyHXXvbgs/28fhAqclSRm5gm7DJrWGtn7j/MgVZVTdbn3zvEaT1Fz6oQEtNV/B3okPokOkuW2Hp5PzJw9LoQzaBXjSgKVk3H8EE=</D></RSAKeyValue>";

        private const string PrivateSystemToken = @"a79d4359-360d-4c9e-8a1e-21678fc4f8d1";



        public BllToken()
        {

        }


        public BllToken(OmsEntity entity)
            : base(entity)
        {
        }





        public bool NewToken(string tokenKey, byte[] clientTokenBytes)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PrivateKey);
            var clientToken = rsa.Decrypt(clientTokenBytes, false);

            if (PrivateSystemToken != Encoding.UTF8.GetString(clientToken)) return false;

            Entity.OMS_Token.Add(new OMS_Token
            {
                ExpireTime = DateTime.MaxValue,
                TokenKey = Guid.Parse(tokenKey)
            });
            Entity.SaveChanges();
            return true;
        }
    }
}
