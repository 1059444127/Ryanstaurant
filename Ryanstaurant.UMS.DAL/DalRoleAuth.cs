using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.Entity;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.DAL
{
    public class DalRoleAuth
    {
        public List<RoleAuth> Get(List<RoleAuth> roleAuths)
        {
            using (var entities = new ryanstaurantEntities())
            {
                var listRoleId = (from a in roleAuths select a.RoleID).ToList();
                var listAuthId = (from a in roleAuths select a.AuthID).ToList();

                var listRoleAuths =
                    (from e in entities.role_auth
                     where listRoleId.Contains(e.role_id) && listAuthId.Contains(e.auth_id)
                     select e).ToList();


                foreach (var rA in roleAuths)
                {
                    var matchedRoleAuth =
                        (from a in listRoleAuths where a.auth_id == rA.AuthID && a.role_id == rA.RoleID select a).FirstOrDefault();


                    if (matchedRoleAuth == null)
                    {
                        rA.Exception = "Auth_id为[" + rA.AuthID + "],Role_id为[" + rA.RoleID + "]的对应关系不存在";
                        rA.ExpType = ExceptionType.NotExist;
                        continue;
                    }


                    rA.Exception = string.Empty;
                    rA.ExpType = ExceptionType.None;
                    rA.AuthID = matchedRoleAuth.auth_id;
                    rA.RoleID = matchedRoleAuth.role_id;

                }
            }
            return roleAuths;
        }



        public List<RoleAuth> Add(List<RoleAuth> roleAuths)
        {
            var listRoleAuths = Get(roleAuths);

            using (var entities = new ryanstaurantEntities())
            {

                foreach (var roleAuth in listRoleAuths)
                {
                    if (roleAuth.ExpType != ExceptionType.NotExist)
                    {
                        roleAuth.Exception = "Auth_id为[" + roleAuth.AuthID + "],Role_id为[" + roleAuth.RoleID + "]的对应关系已经存在";
                        roleAuth.ExpType = ExceptionType.AlreadyExist;
                        continue;
                    }

                    try
                    {

                        entities.role_auth.Add(new role_auth
                        {
                            auth_id = roleAuth.AuthID,
                            role_id = roleAuth.RoleID
                        });
                        roleAuth.Exception = string.Empty;
                        roleAuth.ExpType = ExceptionType.None;
                    }
                    catch (Exception ex)
                    {
                        roleAuth.Exception = ex.Message;
                        roleAuth.ExceptionStackTrace = ex.StackTrace;
                        roleAuth.ExpType = ExceptionType.Failed;
                    }
                }

                var state = entities.SaveChanges();
                if (state <= 0)
                {
                    throw new Exception("添加角色权限失败");
                }
            }
            return roleAuths;
        }


        public List<RoleAuth> Delete(List<RoleAuth> RoleAuth)
        {
            var listRoleAuths = Get(RoleAuth);

            using (var entities = new ryanstaurantEntities())
            {
                foreach (var empRole in listRoleAuths)
                {
                    if (empRole.ExpType != ExceptionType.None)
                    {
                        empRole.Exception = "Auth_id为[" + empRole.AuthID + "],Role_id为[" + empRole.RoleID + "]的对应关系不存在";
                        empRole.ExpType = ExceptionType.NotExist;
                        continue;
                    }

                    var dbRoleAuth = entities.role_auth.First(a => a.auth_id == empRole.AuthID && a.role_id == empRole.RoleID);
                    entities.role_auth.Remove(dbRoleAuth);

                    empRole.Exception = string.Empty;
                    empRole.ExceptionStackTrace = string.Empty;
                    empRole.ExpType = ExceptionType.None;
                }

                var state = entities.SaveChanges();
                if (state <= 0)
                {
                    throw new Exception("删除角色权限失败");
                }
            }
            return RoleAuth;
        }
    }
}
