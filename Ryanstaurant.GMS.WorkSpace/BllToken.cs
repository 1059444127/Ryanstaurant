using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using Ryanstaurant.GMS.DataAccess.EF;
using Ryanstaurant.GMS.IWorkSpace;

namespace Ryanstaurant.GMS.WorkSpace
{
    public class BllToken:BllBase, IBllToken
    {

        public BllToken() { }


        public BllToken(GmsEntity entity) : base(entity)
        {

        }




        public string Login(string userName, string password)
        {
            //验证登录信息
            var umsDbString =
                (from tl in Entity.GMS_TokenLocation where tl.IsLoginSystem == 1 select tl.ConnectionString).ToList()
                    .FirstOrDefault();


            if (string.IsNullOrEmpty(umsDbString))
                return "登录服务器丢失，请查看GMS系统的令牌配置是否正确";

            var umsDb = new DbContext(umsDbString).Database;
            string userID;
            try
            {
                umsDb.Connection.Open();
                userID =
                    umsDb.SqlQuery<string>(
                        "SELECT LoginName FROM dbo.UMS_Employees WHERE LoginName=@LoginName AND Password=@Password",
                        new object[]
                        {
                            new SqlParameter("@LoginName", userName),
                            new SqlParameter("@Password", password)
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                umsDb.Connection.Close();
            }

            if (string.IsNullOrEmpty(userID))
            {
                return "登录失败，错误的用户名或者密码";
            }
            
            var token = Guid.NewGuid();
            TransactionScope transaction = null;
            using (transaction =new TransactionScope())
            {
                var span = 1;
                int.TryParse(Entity.GMS_Sysconfig.Find("tokenExpireDays").ConfigValue, out span);

                var currentDateTime = DateTime.Now;

                //同步到所有配置的Token表
                foreach (var tl in Entity.GMS_TokenLocation)
                {
                    DbContext otherEntity=null;
                    try
                    {
                        otherEntity = new DbContext(tl.ConnectionString);
                        otherEntity.Database.Connection.Open();
                        var sql =
                            string.Format(
                                "INSERT INTO dbo.{0}_Token ( TokenKey, ExpireTime ) VALUES ( @TokenKey, @ExpireTime)",
                                tl.SystemName.ToUpper());

                        otherEntity.Database.ExecuteSqlCommand(sql, new[]
                        {
                            new SqlParameter("@TokenKey",token),
                            new SqlParameter("@ExpireTime", currentDateTime.AddHours(span))
                        });
                    }
                    catch (Exception ex)
                    {
                        return "同步令牌出现错误:" + ex.Message;
                    }
                    finally
                    {
                        if (otherEntity != null)
                            otherEntity.Database.Connection.Close();
                    }
                }

                Entity.GMS_Token.Add(new GMS_Token
                {
                    CreateTime = currentDateTime,
                    ExpireTime = currentDateTime.AddHours(span),
                    TokenKey = token
                });

                Entity.SaveChanges();

                transaction.Complete();
            }

            return token.ToString();

        }
    }


}
