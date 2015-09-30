using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Ryanstaurant.UMS.DataAccess.EF;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllToken:BllBase
    {

        public void NewToken(string tokenKey, string clientToken)
        {


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
