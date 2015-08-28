using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.Entity;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.DAL
{
    public class DalAuthority
    {

        public List<Authority> GetAll()
        {
            using (var entities = new ryanstaurantEntities())
            {
                //从数据库获取相关人员的信息
                var authorityList =
                    (from e in entities.authority
                     select e).ToList();


                return authorityList.Select(authority => new Authority
                {
                    Description = authority.Description,
                    Exception = string.Empty,
                    ExceptionStackTrace = string.Empty,
                    ExpType = ExceptionType.None,
                    ID = authority.id,
                    KeyCode =authority.KeyCode.GetValueOrDefault(),
                    Name = authority.Name
                }).ToList();
            }
        }




        public List<Authority> Get(List<Authority> authorities)
        {
            using (var entities = new ryanstaurantEntities())
            {
                var listAuthorityId = (from a in authorities select a.ID).ToList();

                var listAuthorities =
                    (from e in entities.authority where listAuthorityId.Contains(e.id) select e).ToList();


                foreach (var authority in authorities)
                {
                    var matchedAuthority =
                        (from a in listAuthorities where a.id == authority.ID select a).FirstOrDefault();


                    if (matchedAuthority == null)
                    {
                        authority.Exception = "不存在ID为[" + authority.ID + "]的权限";
                        authority.ExpType = ExceptionType.NotExist;
                        continue;
                    }


                    authority.Exception = string.Empty;
                    authority.ExpType = ExceptionType.None;
                    authority.Description = matchedAuthority.Description;
                    authority.KeyCode = matchedAuthority.KeyCode.GetValueOrDefault(0);
                    authority.Name = matchedAuthority.Name;
                    authority.ID = matchedAuthority.id;

                }
            }
            return authorities;
        }



        public List<Authority> Add(List<Authority> authorities)
        {
            var listAuthorities = Get(authorities);

            using (var entities = new ryanstaurantEntities())
            {

                foreach (var authority in listAuthorities)
                {
                    if (authority.ExpType != ExceptionType.NotExist)
                    {
                        authority.Exception = "ID为["+authority.ID+"]的权限已经存在";
                        authority.ExpType= ExceptionType.AlreadyExist;
                        continue;
                    }

                    try
                    {

                        var result = entities.authority.Add(new authority
                        {
                            Description = authority.Description,
                            id = authority.ID,
                            KeyCode = authority.KeyCode,
                            Name = authority.Name
                        });
                        authority.ID = result.id;
                        authority.Exception = string.Empty;
                        authority.ExpType= ExceptionType.None;
                    }
                    catch (Exception ex)
                    {
                        authority.Exception = ex.Message;
                        authority.ExceptionStackTrace = ex.StackTrace;
                        authority.ExpType= ExceptionType.Failed;
                    }
                }

                var state = entities.SaveChanges();
                if (state <= 0)
                {
                    throw new Exception("添加用户信息失败");
                }
            }
            return authorities;
        }



        public List<Authority> Modify(List<Authority> authorities)
        {
            var listAuthorities = Get(authorities);

            using (var entities = new ryanstaurantEntities())
            {
                foreach (var authority in listAuthorities)
                {
                    if (authority.ExpType != ExceptionType.None)
                    {
                        authority.Exception = "ID为[" + authority.ID + "]的权限不存在";
                        authority.ExpType = ExceptionType.NotExist;
                        continue;
                    }

                    var dbAuthority = entities.authority.First(a => a.id == authority.ID);
                    dbAuthority.Name = authority.Name;
                    dbAuthority.Description = authority.Description;
                    dbAuthority.KeyCode = authority.KeyCode;

                    authority.Exception = string.Empty;
                    authority.ExceptionStackTrace = string.Empty;
                    authority.ExpType = ExceptionType.None;
                }

                var state = entities.SaveChanges();
                if (state <= 0)
                {
                    throw new Exception("修改用户基本信息失败");
                }
            }
            return authorities;
        }


        public List<Authority> Delete(List<Authority> authorities)
        {
            var listAuthorities = Get(authorities);

            using (var entities = new ryanstaurantEntities())
            {
                foreach (var authority in listAuthorities)
                {
                    if (authority.ExpType != ExceptionType.None)
                    {
                        authority.Exception = "ID为[" + authority.ID + "]的权限不存在";
                        authority.ExpType = ExceptionType.NotExist;
                        continue;
                    }

                    var dbAuthority = entities.authority.First(a => a.id == authority.ID);
                    entities.authority.Remove(dbAuthority);

                    authority.Exception = string.Empty;
                    authority.ExceptionStackTrace = string.Empty;
                    authority.ExpType = ExceptionType.None;
                }

                var state = entities.SaveChanges();
                if (state <= 0)
                {
                    throw new Exception("删除用户基本信息失败");
                }
            }
            return authorities;
        }
    }
}
