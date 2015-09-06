using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllRole
    {


        public List<ItemContent> QueryRole(List<ItemContent> roles)
        {
            if (roles == null || roles.Count == 0)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents为空"));
            }

            if (roles.Count == 1 && roles[0] == null)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含操作"));
            }



            var queries =
                (from e in roles
                 where e.RequestInfo != null && e.RequestInfo.Operation == RequestOperation.Query
                 select e).ToList();

            return Query(!queries.Any() ? null : queries.ToList());
        }

        internal List<ItemContent> Query(List<ItemContent> itemContents)
        {
            var resultEntity = new List<ItemContent>();

            using (var entities = new ryanstaurantEntities())
            {
                List<role_auth> roleAuthList;
                List<role> roleList;
                List<authority> authList;



                //没有指定，则返回所有查询结果
                if (itemContents == null)
                {
                    roleAuthList = (from e in entities.role_auth select e).ToList();
                    roleList = (from e in entities.role select e).ToList();
                    authList = (from e in entities.authority select e).ToList();
                }
                else//有指定，则从传送的数据处进行查询
                {
                    var roles = itemContents.Cast<Role>().ToList();
                    var roleIDList = (from e in roles
                                     select e.ID).ToList();

                    var roleNameList = (from e in roles
                                       select e.Name).ToList();


                    roleAuthList = (from e in entities.role_auth where roleIDList.Contains(e.role_id) select e).ToList();
                    roleList = (from e in entities.role where roleIDList.Contains(e.id)||roleNameList.Contains(e.Name) select e).ToList();

                    authList = (from e in entities.authority select e).ToList();
                }



                //人员基本信息
                foreach (var role in roleList)
                {
                    var currentRole = role;

                    var resultRole = new Role
                    {
                        ResultInfo = new ResultContent
                        {
                            State = ResultState.Success,
                            Exception = string.Empty,
                            InnerErrorMessage = string.Empty,
                        },
                        ID = currentRole.id,
                        Description = currentRole.Description,
                        Name = currentRole.Name
                    };

                    var authIDList = (from er in roleAuthList
                                      where er.role_id == currentRole.id
                                      select er.auth_id).ToList();

                    resultRole.Authorities = (from r in authList
                        where authIDList.Contains(r.id)
                        select new Authority
                        {
                            Description = r.Description,
                            ID = r.id,
                            Name = r.Name
                        }).ToList();

                    resultEntity.Add(resultRole);
                }

            }

            return resultEntity;
        }


        public List<ItemContent> ExecuteRole(List<ItemContent> roles)
        {
            if (roles == null || roles.Count == 0)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents为空"));
            }

            if (roles.Count == 1 && roles[0] == null)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含操作"));
            }


            var queries =
            (from e in roles
             where e.RequestInfo != null && e.RequestInfo.Operation != RequestOperation.Query
             select e).ToList();

            if (!queries.Any())
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含执行(add,modify或者delete)操作"));
            }

            var resultEntity = Save(roles);

            return resultEntity;
        }

        private static List<ItemContent> Save(IEnumerable<ItemContent> requestEntitiies)
        {
            var resultEntity = new List<ItemContent>();

            using (var entities = new ryanstaurantEntities())
            {

                foreach (var content in requestEntitiies)
                {
                    var resultcontent = new ItemContent();


                    if (content == null)
                    {
                        resultcontent.ResultInfo.Exception = "未设置操作类型";
                        resultcontent.ResultInfo.InnerErrorMessage = "ResultContent为NULL";
                        resultcontent.ResultInfo.State = ResultState.Fail;


                    }
                    else
                    {
                        switch (content.RequestInfo.Operation)
                        {
                            case RequestOperation.Add:
                            {
                                resultcontent = AddRole(entities, content);
                            }
                                break;
                            case RequestOperation.Modify:
                            {
                                resultcontent = ModifyRole(entities, content);
                            }
                                break;
                            case RequestOperation.Delete:
                            {
                                resultcontent = DeleteRole(entities, content);
                            }
                                break;
                            default:
                            {
                                resultcontent.ResultInfo.Exception = "错误的操作类型";
                                resultcontent.ResultInfo.InnerErrorMessage = "RequestOperation=" +
                                                                             Enum.GetName(typeof (RequestOperation),
                                                                                 content.RequestInfo.Operation);
                                resultcontent.ResultInfo.State = ResultState.Fail;
                            }
                                break;
                        }
                    }
                        resultEntity.Add(resultcontent);
                }
            }
            return resultEntity;
        }

        private static ItemContent DeleteRole(ryanstaurantEntities entities, ItemContent content)
        {
            var role = content as Role;

            if (role == null)
            {
                return new Role
                {
                    ResultInfo = new ResultContent
                    {
                        Exception = "错误的数据类型，没有进行删除操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    }

                };
            }

            var roleInDb = (from e in entities.role where e.id == role.ID select e).FirstOrDefault();

            if (roleInDb == null)
            {
                role.ResultInfo = new ResultContent
                {
                    Exception = "ID为[" + role.ID + "]的角色不存在，没有进行删除操作",
                    InnerErrorMessage = string.Empty,
                    State = ResultState.Success
                };
                return role;
            }


            entities.role.Remove(roleInDb);



            //删除已存在的
            var roleListExist = (from e in entities.role_auth where e.role_id == role.ID select e).ToList();

            if (roleListExist.Any())
                entities.role_auth.RemoveRange(roleListExist);


            entities.SaveChanges();


            role.ResultInfo = new ResultContent
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                State = ResultState.Success
            };

            return role;
        }

        private static ItemContent ModifyRole(ryanstaurantEntities entities, ItemContent content)
        {
            var role = content as Role;

            if (role == null)
            {
                return new Role
                {
                    ResultInfo = new ResultContent
                    {
                        Exception = "错误的数据类型，没有进行修改操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    }

                };
            }



            var roleInDb = (from e in entities.role where e.id == role.ID select e).FirstOrDefault();

            if (roleInDb == null)
            {
                role.ResultInfo = new ResultContent
                {
                    Exception = "ID为[" + role.ID + "]的角色不存在，没有进行修改操作",
                    InnerErrorMessage = string.Empty,
                    State = ResultState.Success
                };
                return role;
            }


             

            roleInDb.Description = role.Description;
            roleInDb.Name = role.Name;



            //删除已存在的
            var roleListExist = (from e in entities.role_auth where e.role_id == role.ID select e).ToList();


            if (roleListExist.Any())
                entities.role_auth.RemoveRange(roleListExist);


            //权限关联关系采用先删除再添加的方式处理
            foreach (var auth in role.Authorities)
            {

                //添加新的
                entities.role_auth.Add(new role_auth
                {
                    role_id = role.ID,
                    auth_id = auth.ID
                });
            }


            entities.SaveChanges();

            role.ResultInfo = new ResultContent
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                State = ResultState.Success
            };

            return role;
        }

        private static ItemContent AddRole(ryanstaurantEntities entities, ItemContent content)
        {
            var role = content as Role;

            if (role == null)
            {
                return new Role
                {
                    ResultInfo = new ResultContent
                    {
                        Exception = "错误的数据类型，没有进行添加操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    }

                };
            }



            var roleToAdd = new role
            {
                Description = role.Description,
                Name = role.Name
            };

            entities.role.Add(roleToAdd);


            foreach (var auth in role.Authorities)
            {
                entities.role_auth.Add(new role_auth
                {
                    role_id = role.ID,
                    auth_id = auth.ID
                });

            }

      

            entities.SaveChanges();


            return new Role
            {
                ResultInfo = new ResultContent
                {
                    Exception = string.Empty,
                    InnerErrorMessage = string.Empty,
                    State = ResultState.Success
                },
                ID=roleToAdd.id,
                Authorities=role.Authorities,
                Description = roleToAdd.Description,
                Name = roleToAdd.Name


               

            };
        }

    }
}
