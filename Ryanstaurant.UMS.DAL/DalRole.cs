using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.Entity;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.DAL
{
    public class DalRole
    {

        public List<Role> GetAll()
        {
            using (var entities = new ryanstaurantEntities())
            {
                //从数据库获取相关人员的信息
                var roleList =
                    (from e in entities.role
                     select e).ToList();


                return roleList.Select(role => new Role
                {
                    Description = role.Description,
                    Exception = string.Empty,
                    ExceptionStackTrace = string.Empty,
                    ExpType = ExceptionType.None,
                    ID = role.id,
                    Name = role.Name
                }).ToList();
            }
        }





        public List<Role> Get(List<Role> roles)
        {
            using (var entities = new ryanstaurantEntities())
            {
                var listRoleId = (from r in roles select r.ID).ToList();

                var listRoles =
                    (from e in entities.role where listRoleId.Contains(e.id) select e).ToList();


                foreach (var role in roles)
                {
                    var matchedRole =
                        (from a in listRoles where a.id == role.ID select a).FirstOrDefault();


                    if (matchedRole == null)
                    {
                        role.Exception = "不存在ID为[" + role.ID + "]的角色";
                        role.ExpType = ExceptionType.NotExist;
                        continue;
                    }


                    role.Exception = string.Empty;
                    role.ExpType = ExceptionType.None;
                    role.Description = matchedRole.Description;
                    role.Name = matchedRole.Name;
                    role.ID = matchedRole.id;

                }
            }
            return roles;
        }



        public List<Role> Add(List<Role> roles)
        {
            var listRoles = Get(roles);

            using (var entities = new ryanstaurantEntities())
            {

                foreach (var role in listRoles)
                {
                    if (role.ExpType != ExceptionType.NotExist)
                    {
                        role.Exception = "ID为[" + role.ID + "]的角色已经存在";
                        role.ExpType = ExceptionType.AlreadyExist;
                        continue;
                    }

                    try
                    {

                        var result = entities.role.Add(new role
                        {
                            Description = role.Description,
                            id = role.ID,
                            Name = role.Name
                        });
                        role.ID = result.id;
                        role.Exception = string.Empty;
                        role.ExpType = ExceptionType.None;
                    }
                    catch (Exception ex)
                    {
                        role.Exception = ex.Message;
                        role.ExceptionStackTrace = ex.StackTrace;
                        role.ExpType = ExceptionType.Failed;
                    }
                }

                var state = entities.SaveChanges();
                if (state <= 0)
                {
                    throw new Exception("添加角色失败");
                }
            }
            return roles;
        }



        public List<Role> Modify(List<Role> roles)
        {
            var listRoles = Get(roles);

            using (var entities = new ryanstaurantEntities())
            {
                foreach (var role in listRoles)
                {
                    if (role.ExpType != ExceptionType.None)
                    {
                        role.Exception = "ID为[" + role.ID + "]的权限不存在";
                        role.ExpType = ExceptionType.NotExist;
                        continue;
                    }

                    var dbRole = entities.role.First(a => a.id == role.ID);
                    dbRole.Name = role.Name;
                    dbRole.Description = role.Description;

                    role.Exception = string.Empty;
                    role.ExceptionStackTrace = string.Empty;
                    role.ExpType = ExceptionType.None;
                }

                var state = entities.SaveChanges();
                if (state <= 0)
                {
                    throw new Exception("修改角色失败");
                }
            }
            return roles;
        }


        public List<Role> Delete(List<Role> roles)
        {
            var listRoles = Get(roles);

            using (var entities = new ryanstaurantEntities())
            {
                foreach (var role in listRoles)
                {
                    if (role.ExpType != ExceptionType.None)
                    {
                        role.Exception = "ID为[" + role.ID + "]的权限不存在";
                        role.ExpType = ExceptionType.NotExist;
                        continue;
                    }

                    var dbRole = entities.role.First(a => a.id == role.ID);
                    entities.role.Remove(dbRole);

                    role.Exception = string.Empty;
                    role.ExceptionStackTrace = string.Empty;
                    role.ExpType = ExceptionType.None;
                }

                var state = entities.SaveChanges();
                if (state <= 0)
                {
                    throw new Exception("删除角色失败");
                }
            }
            return roles;
        }
    }
}
