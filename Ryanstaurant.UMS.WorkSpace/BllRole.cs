﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllRole:BllBase
    {
        public new UmsEntities Entities
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
                 where e.CommandInfo != null && e.CommandInfo.Operation == RequestOperation.Query
                 select e).ToList();

            return Query(!queries.Any() ? null : queries.ToList());
        }

        internal List<ItemContent> Query(List<ItemContent> itemContents)
        {
            var resultEntity = new List<ItemContent>();

                List<role> roleList;



                //没有指定，则返回所有查询结果
                if (itemContents == null)
                {
                    roleList = (from e in Entities.role select e).ToList();
                }
                else//有指定，则从传送的数据处进行查询
                {
                    var roles = itemContents.Cast<Role>().ToList();
                    var roleIDList = (from e in roles
                                     select e.ID).ToList();

                    var roleNameList = (from e in roles
                                       select e.Name).ToList();


                    roleList = (from e in Entities.role where roleIDList.Contains(e.id)||roleNameList.Contains(e.Name) select e).ToList();

                }



                //人员基本信息
                resultEntity.AddRange(roleList.Select(currentRole => new Role
                {
                    CommandInfo = new CommandInformation
                    {
                        State = ResultState.Success, Exception = string.Empty, InnerErrorMessage = string.Empty,
                    },
                    ID = currentRole.id, Description = currentRole.Description, Name = currentRole.Name, Authority = currentRole.Authority.GetValueOrDefault()
                }));


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
             where e.CommandInfo != null && e.CommandInfo.Operation != RequestOperation.Query
             select e).ToList();

            if (!queries.Any())
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含执行(add,modify或者delete)操作"));
            }

            var resultEntity = Save(roles);

            return resultEntity;
        }

        private List<ItemContent> Save(IEnumerable<ItemContent> requestEntitiies)
        {
            var resultEntity = new List<ItemContent>();



                foreach (var content in requestEntitiies)
                {
                    var resultcontent = new ItemContent();


                    if (content == null)
                    {
                        resultcontent.CommandInfo.Exception = "未设置操作类型";
                        resultcontent.CommandInfo.InnerErrorMessage = "ResultContent为NULL";
                        resultcontent.CommandInfo.State = ResultState.Fail;


                    }
                    else
                    {
                        switch (content.CommandInfo.Operation)
                        {
                            case RequestOperation.Add:
                            {
                                resultcontent = AddRole( content);
                            }
                                break;
                            case RequestOperation.Modify:
                            {
                                resultcontent = ModifyRole( content);
                            }
                                break;
                            case RequestOperation.Delete:
                            {
                                resultcontent = DeleteRole( content);
                            }
                                break;
                            default:
                            {
                                resultcontent.CommandInfo.Exception = "错误的操作类型";
                                resultcontent.CommandInfo.InnerErrorMessage = "RequestOperation=" +
                                                                             Enum.GetName(typeof (RequestOperation),
                                                                                 content.CommandInfo.Operation);
                                resultcontent.CommandInfo.State = ResultState.Fail;
                            }
                                break;
                        }
                    }
                        resultEntity.Add(resultcontent);
                }

            return resultEntity;
        }

        private ItemContent DeleteRole(ItemContent content)
        {
            var role = content as Role;

            if (role == null)
            {
                return new Role
                {
                    CommandInfo = new CommandInformation
                    {
                        Exception = "错误的数据类型，没有进行删除操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    }

                };
            }

            var roleInDb = (from e in Entities.role where e.id == role.ID select e).FirstOrDefault();

            if (roleInDb == null)
            {
                role.CommandInfo = new CommandInformation
                {
                    Exception = "ID为[" + role.ID + "]的角色不存在，没有进行删除操作",
                    InnerErrorMessage = string.Empty,
                    State = ResultState.Success
                };
                return role;
            }


            Entities.role.Remove(roleInDb);

            //删除人员角色关联
            var roleInEmpRole = from e in Entities.emp_role where e.role_id == role.ID select e;

            if (roleInEmpRole.Any())
            {
                Entities.emp_role.RemoveRange(roleInEmpRole.ToList());
            }


            Entities.SaveChanges();


            role.CommandInfo = new CommandInformation
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                State = ResultState.Success
            };

            return role;
        }

        private ItemContent ModifyRole(ItemContent content)
        {
            var role = content as Role;

            if (role == null)
            {
                return new Role
                {
                    CommandInfo = new CommandInformation
                    {
                        Exception = "错误的数据类型，没有进行修改操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    }

                };
            }



            var roleInDb = (from e in Entities.role where e.id == role.ID select e).FirstOrDefault();

            if (roleInDb == null)
            {
                role.CommandInfo = new CommandInformation
                {
                    Exception = "ID为[" + role.ID + "]的角色不存在，没有进行修改操作",
                    InnerErrorMessage = string.Empty,
                    State = ResultState.Success
                };
                return role;
            }


             

            roleInDb.Description = role.Description;
            roleInDb.Name = role.Name;
            roleInDb.Authority = role.Authority;


            Entities.SaveChanges();

            role.CommandInfo = new CommandInformation
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                State = ResultState.Success
            };

            return role;
        }

        private ItemContent AddRole(ItemContent content)
        {
            var role = content as Role;

            if (role == null)
            {
                return new Role
                {
                    CommandInfo = new CommandInformation
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
                Name = role.Name,
                Authority = role.Authority
            };

            Entities.role.Add(roleToAdd);



            Entities.SaveChanges();


            return new Role
            {
                CommandInfo = new CommandInformation
                {
                    Exception = string.Empty,
                    InnerErrorMessage = string.Empty,
                    State = ResultState.Success
                },
                ID=roleToAdd.id,
                Authority= role.Authority,
                Description = roleToAdd.Description,
                Name = roleToAdd.Name


               

            };
        }

    }
}
