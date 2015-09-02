using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllAuthority
    {
        public List<ResultContent<Authority>> QueryAuthorities(RequestEntitiy<Authority> authorities)
        {
            if (authorities.RequestContents == null || authorities.RequestContents.Count == 0)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents为空"));
            }

            if (authorities.RequestContents.Count == 1 && authorities.RequestContents[0] == null)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含操作"));
            }


            var resultEntity = new List<ResultContent<Authority>>();

            if (authorities.QueryContentList != null)
            {
                resultEntity = Query(authorities.QueryContentList);
            }


            return resultEntity;
        }

        private static List<ResultContent<Authority>> Query(List<Authority> authorities)
        {
            var resultEntity = new List<ResultContent<Authority>>();

            using (var entities = new ryanstaurantEntities())
            {
                List<authority> authList;

                //没有指定，则返回所有查询结果
                if (!authorities.Any())
                {
                    authList = (from e in entities.authority select e).ToList();
                }
                else//有指定，则从传送的数据处进行查询
                {
                    var authIDList = (from e in authorities
                                     select e.ID).ToList();

                    var authNameList = (from e in authorities
                                       select e.Name).ToList();

                    authList =
                        (from e in entities.authority
                            where authIDList.Contains(e.id) || authNameList.Contains(e.Name)
                            select e).ToList();
                }

                resultEntity.AddRange(authList.Select(auth => new ResultContent<Authority>
                {
                    State = ResultState.Success,
                    Exception = string.Empty,
                    InnerErrorMessage = string.Empty,
                    Item = new Authority
                    {
                        ID = auth.id,
                        Description = auth.Description,
                        Name = auth.Name
                    }
                }));
            }

            return resultEntity;
        }


        public List<ResultContent<Authority>> ExcuteAuthorities(RequestEntitiy<Authority> authorities)
        {
            if (authorities.RequestContents == null || authorities.RequestContents.Count == 0)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents为空"));
            }

            if (authorities.RequestContents.Count == 1 && authorities.RequestContents[0] == null)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含操作"));
            }


            var resultEntity = Save(authorities);
            return resultEntity;
        }

        internal List<ResultContent<Authority>> Save(RequestEntitiy<Authority> requestEntitiies)
        {
            var resultEntity = new List<ResultContent<Authority>>();

            using (var entities = new ryanstaurantEntities())
            {

                foreach (var content in requestEntitiies.RequestContents)
                {
                    var resultcontent = new ResultContent<Authority>();


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
                                resultcontent = AddAuthorities(entities, content);
                            }
                            break;
                        case RequestOperation.Modify:
                            {
                                resultcontent = ModifyAuthorities(entities, content);
                            }
                            break;
                        case RequestOperation.Delete:
                            {
                                resultcontent = DeleteAuthorities(entities, content);
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

        private static ResultContent<Authority> DeleteAuthorities(ryanstaurantEntities entities, RequestContent<Authority> content)
        {
            var authority = content.RequestObject;

            var authorityInDb = (from e in entities.authority where e.id == content.RequestObject.ID select e).FirstOrDefault();

            if (authorityInDb == null)
            {
                return new ResultContent<Authority>
                {
                    Exception = "ID为[" + authority.ID + "]的权限不存在，没有进行删除操作",
                    InnerErrorMessage = string.Empty,
                    Item = authority,
                    State = ResultState.Success
                };
            }


            entities.authority.Remove(authorityInDb);

            entities.SaveChanges();


            return new ResultContent<Authority>
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                Item = authority,
                State = ResultState.Success

            };
        }

        private static ResultContent<Authority> ModifyAuthorities(ryanstaurantEntities entities, RequestContent<Authority> content)
        {
            var authority = content.RequestObject;

            var authorityInDb = (from e in entities.employee where e.ID == content.RequestObject.ID select e).FirstOrDefault();

            if (authorityInDb == null)
            {
                return new ResultContent<Authority>
                {
                    Exception = "ID为[" + authority.ID + "]的权限不存在，没有进行修改操作",
                    InnerErrorMessage = string.Empty,
                    Item = authority,
                    State = ResultState.Success
                };
            }


            authorityInDb.Description = authority.Description;
            authorityInDb.Name = authority.Name;


            entities.SaveChanges();


            return new ResultContent<Authority>
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                Item = authority,
                State = ResultState.Success

            };
        }

        private static ResultContent<Authority> AddAuthorities(ryanstaurantEntities entities, RequestContent<Authority> content)
        {
            var authority = content.RequestObject;


            var authToAdd = new employee
            {
                Description = authority.Description,
                Name = authority.Name
            };

            entities.employee.Add(authToAdd);


            entities.SaveChanges();

            authority.ID = authToAdd.ID;

            return new ResultContent<Authority>
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                Item = authority,
                State = ResultState.Success

            };
        }


    }
}
