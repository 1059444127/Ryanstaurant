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

        protected readonly UMSClient ServiceClient = new UMSClient();

        #region 属性

        private int _id = -1;

        public int ID
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
                    RequestInfo = new RequestContent
                    {
                        Operation = RequestOperation.Add
                    }
                }
            };

            var results = ServiceClient.Execute(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

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
                    RequestInfo =new RequestContent
                    {
                        Operation = RequestOperation.Query
                    }
                }
            };

            var results = ServiceClient.Query(arrAuthorities);
            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            var authReturn = results.ResultObject.FirstOrDefault() as Authority;

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
                    RequestInfo = new RequestContent
                    {
                        Operation = RequestOperation.Modify
                    }
                }
            };


            var results = ServiceClient.Execute(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

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
                    RequestInfo = new RequestContent
                    {
                        Operation = RequestOperation.Delete
                    }
                }
            };



            var results = ServiceClient.Execute(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Reset();
        }


        public void Reset()
        {
            _id = -1;
            Name = string.Empty;
            Description = string.Empty;
        }





    }
}
