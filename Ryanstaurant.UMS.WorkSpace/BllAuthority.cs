﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataAccess;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllAuthority:BllBase
    {

        public UmsEntity Entities
        {
            get
            {
                return Entity;
            }
            set
            {
                Entity = value;
            }
        }


        public List<ItemContent> QueryAuthority(List<ItemContent> authorities)
        {
            if (authorities == null || authorities.Count == 0)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents为空"));
            }

            if (authorities.Count == 1 && authorities[0] == null)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含操作"));
            }


            var queries =
               (from e in authorities
                where e.CommandInfo != null && e.CommandInfo.Operation == RequestOperation.Query
                select e).ToList();

            return Query(!queries.Any() ? null : queries.ToList());
        }

        private List<ItemContent> Query(IEnumerable<ItemContent> itemContents)
        {
            var resultEntity = new List<ItemContent>();


                List<UMS_Authorities> authList;


                //没有指定，则返回所有查询结果
                if (itemContents == null)
                {
                    authList = (from e in Entities.UMS_Authorities select e).ToList();
                }
                else//有指定，则从传送的数据处进行查询
                {
                    var authorities = itemContents.Cast<Authority>().ToList();
                    var authIDList = (from e in authorities
                                     select e.ID).ToList();

                    var authNameList = (from e in authorities
                                        select e.Name).ToList();

                    authList =
                        (from e in Entities.UMS_Authorities
                            where authIDList.Contains(e.id) || authNameList.Contains(e.Name)
                            select e).ToList();
                }

                resultEntity.AddRange(authList.Select(auth => new Authority
                {
                    CommandInfo = new CommandInformation
                    {
                        State = ResultState.Success,
                        Exception = string.Empty,
                        InnerErrorMessage = string.Empty
                    },
                    ID = auth.id,
                    Description = auth.Description,
                    Name = auth.Name
                }));


            return resultEntity;
        }


        public List<ItemContent> ExecuteAuthority(List<ItemContent> authorities)
        {
            if (authorities == null || authorities.Count == 0)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents为空"));
            }

            if (authorities.Count == 1 && authorities[0] == null)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含操作"));
            }


            var queries =
               (from e in authorities
                where e.CommandInfo != null && e.CommandInfo.Operation != RequestOperation.Query
                select e).ToList();

            if (!queries.Any())
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含执行(add,modify或者delete)操作"));
            }

            var resultEntity = Save(authorities);

            return resultEntity;
        }

        internal List<ItemContent> Save(List<ItemContent> requestEntitiies)
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
                                resultcontent = AddAuthorities( content);
                            }
                                break;
                            case RequestOperation.Modify:
                            {
                                resultcontent = ModifyAuthorities( content);
                            }
                                break;
                            case RequestOperation.Delete:
                            {
                                resultcontent = DeleteAuthorities( content);
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

        private ItemContent DeleteAuthorities(ItemContent content)
        {
            var authority = content as Authority;


            if (authority == null)
            {
                return new Authority
                {
                    CommandInfo = new CommandInformation
                    {
                        Exception = "错误的数据类型，没有进行删除操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    }
                    
                };
            }



            var authorityInDb = (from e in Entities.UMS_Authorities where e.id == authority.ID select e).FirstOrDefault();

            if (authorityInDb == null)
            {
                return new Authority
                {
                    CommandInfo = new CommandInformation
                    {

                        Exception = "ID为[" + authority.ID + "]的权限不存在，没有进行删除操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    },
                    ID = authority.ID,
                    Description = authority.Description,
                    Name = authority.Name
                };
            }


            //去除所有角色中的当前权限
            var roles = from e in Entities.UMS_Roles select e;

            foreach (var role in roles)
            {
                role.Authority &= ~authority.ID;
            }

            //去除所有人员中的当前权限
            var employees = from e in Entities.UMS_Employees select e;
            foreach (var employee in employees)
            {
                employee.Authority &= ~authority.ID;
            }

            Entities.UMS_Authorities.Remove(authorityInDb);

            Entities.SaveChanges();


            return new Authority
            {
                CommandInfo = new CommandInformation
                {

                    Exception = string.Empty,
                    InnerErrorMessage = string.Empty,
                    State = ResultState.Success
                },

                ID = authority.ID,
                Description = authority.Description,
                Name = authority.Name
            };
        }

        private ItemContent ModifyAuthorities(ItemContent content)
        {
            var authority = content as Authority;


            if (authority == null)
            {
                return new Authority
                {
                    CommandInfo = new CommandInformation
                    {
                        Exception = "错误的数据类型，没有进行修改操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    }

                };
            }


            var authorityInDb = (from e in Entities.UMS_Authorities where e.id == authority.ID select e).FirstOrDefault();

            if (authorityInDb == null)
            {
                return new Authority
                {
                    CommandInfo = new CommandInformation
                    {
                        Exception = "ID为[" + authority.ID + "]的权限不存在，没有进行修改操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    },
                    ID = authority.ID,
                    Description = authority.Description,
                    Name = authority.Name

                };
            }


            authorityInDb.Description = authority.Description;
            authorityInDb.Name = authority.Name;


            Entities.SaveChanges();


            return new Authority
            {
                CommandInfo = new CommandInformation
                {

                    Exception = string.Empty,
                    InnerErrorMessage = string.Empty,
                    State = ResultState.Success
                },
                ID = authority.ID,
                Description = authority.Description,
                Name = authority.Name
            };
        }



        private long GetAvailableAuthId()
        {
            var idList = (from i in Entities.UMS_Authorities orderby i.id select i.id).ToList();
            for (long i = 1; i < long.MaxValue; i = i << 1)
            {
                if (idList.Contains(i)) continue;
                return i;
            }
            return 1;
        }





        private  ItemContent AddAuthorities(ItemContent content)
        {
            var authority = content as Authority;

            if (authority == null)
            {
                return new Authority
                {
                    CommandInfo = new CommandInformation
                    {
                        Exception = "错误的数据类型，没有进行添加操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    }

                };
            }


            var authToAdd = new UMS_Authorities
            {
                id = GetAvailableAuthId(),
                Description = authority.Description,
                Name = authority.Name
            };

            Entities.UMS_Authorities.Add(authToAdd);

            Entities.SaveChanges();

            authority.ID = authToAdd.id;

            return new Authority
            {
                CommandInfo = new CommandInformation
                {

                    Exception = string.Empty,
                    InnerErrorMessage = string.Empty,
                    State = ResultState.Success
                },
                ID = authority.ID,
                Description = authority.Description,
                Name = authority.Name
            };
        }


    }
}
