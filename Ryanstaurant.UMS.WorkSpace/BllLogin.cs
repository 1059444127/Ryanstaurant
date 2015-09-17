using System;
using System.Configuration;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllLogin:BllBase
    {
        public UmsEntities Entities
        {
            get
            {
                return base.Entities;
            }
            set
            {
                base.Entities = value;
            }
        }


        public string GetToken(string userName, string password)
        {
            var employee =
                (from e in Entities.employee where e.LoginName == userName && e.Password == password select e)
                    .FirstOrDefault();


            if (employee == null)
                return null;


            var token = Guid.NewGuid().ToString();

            Entities.UserToken.Add(new UserToken
            {
                TokenKey = token,
                UserID = employee.ID,
                CreateTime = DateTime.Now
            });

            var result = Entities.SaveChanges();
            if (result <= 0)
                throw new Exception("获取令牌失败", new Exception("GetToken Failed"));

            return token;
        }


        public bool ValidToken(string token, out string exception)
        {
            var tok = (from t in Entities.UserToken
                where t.TokenKey == token
                select t).FirstOrDefault();

            if (tok == null)
            {
                exception = "没有找到对应的登录令牌，操作不能进行";
                return false;
            }

            var tokenExpireDays =
                (from c in Entities.sysconfig where c.ShortCall == "tokenExpireDays" select c).FirstOrDefault();

            var expireHours = 0;
            if (tokenExpireDays != null)
            {
                int.TryParse(tokenExpireDays.ConfigValue, out expireHours);
            }

            if (tok.CreateTime.AddHours(expireHours) < DateTime.Now.Date)
            {
                exception = "令牌已经失效，请重新登录";
                return false;
            }

            exception = string.Empty;
            return true;
        }





    }
}
