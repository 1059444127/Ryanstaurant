using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.Client;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class AuthorityModel
    {

        protected IUMSClient ServiceClient;

        public IUMSClient UmsClient
        {
            get
            {
                return ServiceClient;
            }
            set
            {
                ServiceClient = value;
            }
        }



         public AuthorityModel(IUMSClient client)
        {
            ServiceClient = client;
        }

        [Obsolete("单元测试使用，正常情况下请勿使用")]
         public AuthorityModel()
        { }







        #region 属性

        private long _id = -1;

        public long ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Name { get; set; }

        public string Description { get; set; }
        #endregion


        public void Add()
        {
            var arrAuthorities = new List<ItemContent>
            {
                new Authority
                {
                    Description = Description,
                    ID = ID,
                    Name = Name,
                    CommandInfo = new CommandInformation()
                    {
                        Operation = RequestOperation.Add
                    }
                }
            };

            var results = ServiceClient.Execute(arrAuthorities);

            if (ServiceClient.State == ResultState.Fail)
                throw new Exception(ServiceClient.ErrorMessage);

            Refresh();
        }


        public void Refresh()
        {
            var arrAuthorities = new List<ItemContent>
            {
                new Authority
                {
                    ID = ID,
                    Name = Name,
                    CommandInfo =new CommandInformation
                    {
                        Operation = RequestOperation.Query
                    }
                }
            };

            var results = ServiceClient.Query(arrAuthorities);
            if (ServiceClient.State == ResultState.Fail)
                throw new Exception(ServiceClient.ErrorMessage);

            var authReturn = results.FirstOrDefault() as Authority;

            if (authReturn == null)
            {
                throw new Exception(
                    ID == -1 ? "没有找到名称为[" + Name + "]对应的权限" : "没有找到ID为[" + ID + "]对应的权限");
            }

            ID = authReturn.ID;
            Description = authReturn.Description;
            Name = authReturn.Name;

        }



        public void Modify()
        {
            var arrAuthority = new List<ItemContent>
            {
                new Authority
                {
                    ID = ID,
                    Name = Name,
                    Description = Description,
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Modify
                    }
                }
            };


            var results = ServiceClient.Execute(arrAuthority);

            if (ServiceClient.State == ResultState.Fail)
                throw new Exception(ServiceClient.ErrorMessage);

            Refresh();
        }

        public void Delete()
        {
            var arrAuthority = new List<ItemContent>
            {
                new Authority
                {
                    Description = Description,
                    ID = ID,
                    Name = Name,
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Delete
                    }
                }
            };



            var results = ServiceClient.Execute(arrAuthority);

            if (ServiceClient.State == ResultState.Fail)
                throw new Exception(ServiceClient.ErrorMessage);

            Reset();
        }


        public void Reset()
        {
            _id = -1;
            Name = string.Empty;
            Description = string.Empty;
        }





        public List<AuthorityModel> GetAllAuthorities()
        {
            //获取EMP基本信息
            var result = ServiceClient.GetAllAuthorities();
            if (ServiceClient.State == ResultState.Fail)
            {
                throw new Exception(ServiceClient.ErrorMessage);
            }

            var authReturn = result.Cast<Authority>().ToList();

            //写入基本信息
            var authorityList = authReturn.Select(auth => new AuthorityModel
            {
                Description = auth.Description,
                ID = auth.ID,
                Name = auth.Name
            }).ToList();

            return authorityList;

        }
    }
}
