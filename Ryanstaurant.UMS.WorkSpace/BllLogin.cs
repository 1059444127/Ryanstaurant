using System;
using System.Configuration;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllLogin
    {
        private UmsEntities _entities;

        public UmsEntities Entities
        {
            get
            {
                return _entities;
            }
            set
            {
                _entities = value;
            }
        }


        public BllLogin()
        {
            _entities = new UmsEntities();
        }


        ~BllLogin()
        {
            _entities.Dispose();
        }



        public string GetToken(string userName, string password)
        {
            var employee =
                (from e in _entities.employee where e.LoginName == userName && e.Password == password select e)
                    .FirstOrDefault();


            if (employee == null)
                return null;


            var token = Guid.NewGuid().ToString();

            _entities.UserToken.Add(new UserToken
            {
                TokenKey = token,
                UserID = employee.ID
            });

            var result = _entities.SaveChanges();
            if (result <= 0)
                throw new Exception("获取令牌失败", new Exception("GetToken Failed"));

            return token;
        }


        public bool ValidToken(string token, out string exception)
        {
            var tok = (from t in _entities.UserToken
                       where string.Equals(t.TokenKey, token, StringComparison.InvariantCultureIgnoreCase)
                       select t).FirstOrDefault();

            if (tok == null)
            {
                exception = "没有找到对应的登录令牌，操作不能进行";
                return false;
            }

            var tokenExpireDays =
                (from c in _entities.sysconfig where c.ShortCall == "tokenExpireDays" select c).FirstOrDefault();

            var expireDays = 0;
            if (tokenExpireDays != null)
            {
                int.TryParse(tokenExpireDays.ConfigValue, out expireDays);
            }

            if (tok.CreateTime.AddDays(expireDays) < DateTime.Now.Date)
            {
                exception = "令牌已经失效，请重新登录";
                return false;
            }

            exception = string.Empty;
            return true;
        }





    }
}
