using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Ryanstaurant.UMS.DataAccess;
using Ryanstaurant.UMS.DataAccess.EF;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllToken:BllBase
    {
        public new UmsEntity Entity
        {
            get
            {
                return base.Entity;
            }
            set
            {
                base.Entity = value;
            }
        }




        private const string PrivateKey = @"<RSAKeyValue><Modulus>kE0wvy74FfVhfX6/g/e9nI+9uMinpNtgEfvaYEopC2FBnZ41IRjFZkryC1v51gPrsLR3HhI5pUN55jmvSt5eb0Jo0DhgEkKBTmPr3p6tIW8zT+94n4Cd7xwM/ebMjelIV7FaoFCqKlIcC+EOl1GjBmkNMYZlSdl6DBk4UPDH1rM=</Modulus><Exponent>AQAB</Exponent><P>wT4r9/eFiHUXsi46NI6wg8nSMa4ENoCNOrTcU1j4h4lA7WLBhJRnhomefkawoqCASJ/mMTSLEw3I45BDLslg7w==</P><Q>vyoiGhsaWmO69Kl1KtgoBI5D5ADWqNV3cmhUhltvnzfkkOeqDDIlQebxIDKs2R5MM3os4HueTiJesa/u22CefQ==</Q><DP>KKiJHcIxkVV5p968FmTTVNc454UCk+kWTfzRwqOcnv/M7mVx7BBBe/gmpdD/xyfX9e/WwhGZFWg4FVE8IXLzdw==</DP><DQ>g3boSOfGsc7QLZ20NCi/LJLh4ZUqCqXzJTzWlCehle+MQpLvAMAjmzTFMo1nDVGmuegVoLOi5L0CPlRtgXTqyQ==</DQ><InverseQ>vmUWP5omXlysoPz1bFMBDCg7qnCytT2DgdqjeyEEeNVjHWtXx0IKYG5r3DLpuS1yEMZR49ekC/5D4Wfyb4hTGw==</InverseQ><D>Jb3asxZg0rV0QzOEecqxMCK0V4E7v7WVAf8iVa7v30cXt1bqxHZLIv8VIX1z8dLisTvyL85Kf3wPhPSvcWCJyCFGqsPD29dDJOIGRnlBM84VpqLcz+3y9lD/rVzwZOflHm8WnQVXThv02zI6/35/C9t6lwulWBG+GeOeYM6ml+k=</D></RSAKeyValue>";
        private const string PrivateSystemToken = @"a08cc634-1cd3-4e97-802f-5a0e0beb30a5";


        public bool NewToken(string tokenKey,byte[] clientTokenBytes)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PrivateKey);
            var clientToken =rsa.Decrypt(clientTokenBytes, false);

            if (PrivateSystemToken != Encoding.UTF8.GetString(clientToken)) return false;

            Entity.UMS_Tokens.Add(new UMS_Token
            {
                ExpireTime = DateTime.MaxValue,
                TokenKey = Guid.Parse(tokenKey)
            });
            Entity.SaveChanges();
            return true;
        }



        public void SendClientToken(Guid clientToken)
        {

            var gmsContext = new DbContext("Gms");
            try
            {
                gmsContext.Database.Connection.Open();
                gmsContext.Database.ExecuteSqlCommand(@"UPDATE  dbo.GMS_TokenLocation
SET     ConnectionString = @ConnectionString ,
        ClientToken = @ClientToken ,
        IsLoginSystem = 1
WHERE   SystemName = 'UMS'", new object[]
                {
                    new SqlParameter("@ConnectionString", Entity.Database.Connection.ConnectionString),
                    new SqlParameter("@ClientToken", clientToken)
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                gmsContext.Dispose();
            }

        }




    }
}
