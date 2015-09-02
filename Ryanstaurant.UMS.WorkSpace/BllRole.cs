using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllRole
    {


        public List<ResultContent<Role>> QueryRoles(RequestEntitiy<Role> roles)
        {
            if (roles.RequestContents == null || roles.RequestContents.Count == 0)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents为空"));
            }

            if (roles.RequestContents.Count == 1 && roles.RequestContents[0] == null)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含操作"));
            }


            var resultEntity = new List<ResultContent<Role>>();

            if (roles.QueryContentList != null)
            {
                resultEntity = Query(roles.QueryContentList);
            }


            return resultEntity;
        }

        internal List<ResultContent<Role>> Query(List<Role> roles)
        {
            var resultEntity = new List<ResultContent<Role>>();

            using (var entities = new ryanstaurantEntities())
            {
                List<role_auth> roleAuthList;
                List<role> roleList;
                List<authority> authList;

                //没有指定，则返回所有查询结果
                if (!roles.Any())
                {
                    roleAuthList = (from e in entities.role_auth select e).ToList();
                    roleList = (from e in entities.role select e).ToList();
                    authList = (from e in entities.authority select e).ToList();
                }
                else//有指定，则从传送的数据处进行查询
                {
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

                    var resultRole = new ResultContent<Role>
                    {
                        State = ResultState.Success,
                        Exception = string.Empty,
                        InnerErrorMessage = string.Empty,
                        Item = new Role
                        {
                            ID = currentRole.id,
                            Description = currentRole.Description,
                            Name = currentRole.Name
                        }
                    };

                    var authIDList = (from er in roleAuthList
                                      where er.role_id == currentRole.id
                                      select er.auth_id).ToList();

                    resultRole.Item.Authorities = (from r in authList
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


        public List<ResultContent<Role>> ExcuteRoles(RequestEntitiy<Role> roles)
        {
            if (roles.RequestContents == null || roles.RequestContents.Count == 0)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents为空"));
            }

            if (roles.RequestContents.Count == 1 && roles.RequestContents[0] == null)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含操作"));
            }


            var resultEntity = new List<ResultContent<Role>>();

            resultEntity = Save(roles);
            return resultEntity;
        }

        private List<ResultContent<Role>> Save(RequestEntitiy<Role> requestEntitiies)
        {
            var resultEntity = new List<ResultContent<Role>>();

            using (var entities = new ryanstaurantEntities())
            {

                foreach (var content in requestEntitiies.RequestContents)
                {
                    var resultcontent = new ResultContent<Role>();


                    if (content == null)
                    {
                        resultcontent.Exception = "未设置操作类型";
                        resultcontent.InnerErrorMessage = "ResultContent为NULL";
                        resultcontent.Item = null;
                        resultcontent.State = ResultState.Fail;
                    }

                    switch (content.Operation)
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
                                resultcontent.Exception = "错误的操作类型";
                                resultcontent.InnerErrorMessage = "RequestOperation=" +
                                                                  Enum.GetName(typeof(RequestOperation), content.Operation);
                                resultcontent.Item = null;
                                resultcontent.State = ResultState.Fail;
                            }
                            break;
                    }
                    resultEntity.Add(resultcontent);

                }
            }
            return resultEntity;
        }

        private static ResultContent<Role> DeleteRole(ryanstaurantEntities entities, RequestContent<Role> content)
        {
            var role = content.RequestObject;

            var roleInDb = (from e in entities.role where e.id == content.RequestObject.ID select e).FirstOrDefault();

            if (roleInDb == null)
            {
                return new ResultContent<Role>
                {
                    Exception = "ID为[" + role.ID + "]的角色不存在，没有进行删除操作",
                    InnerErrorMessage = string.Empty,
                    Item = role,
                    State = ResultState.Success
                };
            }


            entities.role.Remove(roleInDb);



            //删除已存在的
            var roleListExist = (from e in entities.role_auth where e.role_id == role.ID select e).ToList();

            if (roleListExist.Any())
                entities.role_auth.RemoveRange(roleListExist);


            entities.SaveChanges();


            return new ResultContent<Role>
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                Item = role,
                State = ResultState.Success

            };
        }

        private static ResultContent<Role> ModifyRole(ryanstaurantEntities entities, RequestContent<Role> content)
        {
            var role = content.RequestObject;

            var roleInDb = (from e in entities.role where e.id == content.RequestObject.ID select e).FirstOrDefault();

            if (roleInDb == null)
            {
                return new ResultContent<Role>
                {
                    Exception = "ID为[" + role.ID + "]的角色不存在，没有进行修改操作",
                    InnerErrorMessage = string.Empty,
                    Item = role,
                    State = ResultState.Success
                };
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


            return new ResultContent<Role>
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                Item = role,
                State = ResultState.Success

            };
        }

        private static ResultContent<Role> AddRole(ryanstaurantEntities entities, RequestContent<Role> content)
        {
            var role = content.RequestObject;


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

            role.ID = roleToAdd.id;

            return new ResultContent<Role>
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                Item = role,
                State = ResultState.Success

            };
        }

    }
}
