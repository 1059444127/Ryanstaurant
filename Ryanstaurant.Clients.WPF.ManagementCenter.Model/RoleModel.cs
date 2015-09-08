using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.UMS.Client;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class RoleModel
    {
        protected readonly UMSClient ServiceClient = new UMSClient();

        private int _id = -1;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name { get; set; }

        public string Description { get; set; }
        public long Authority { get; set; }




        public void Add()
        {
            var arrRoles = new List<ItemContent>
            {
                new Role
                {
                    Description = Description,
                    ID = ID,
                    Name = Name,
                    Authority = Authority,
                    RequestInfo = new RequestContent
                    {
                        Operation = RequestOperation.Add
                    }
                }
            };

            var results = ServiceClient.Execute(arrRoles);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }


        public void Refresh()
        {
            var arrRoles = new List<ItemContent>
            {
                new Role
                {
                    ID = ID,
                    Name = Name,
                    Authority = Authority,
                    RequestInfo=new RequestContent
                    {
                        Operation = RequestOperation.Query
                    }
                }
            };

            var results = ServiceClient.Query(arrRoles);
            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            var authReturn = results.ResultObject.FirstOrDefault() as Role;


            if (authReturn == null)
            {
                throw new Exception(
                    ID == -1 ? "没有找到名称为[" + Name + "]对应的角色" : "没有找到ID为[" + ID + "]对应的角色");
            }

            ID = authReturn.ID;
            Description = authReturn.Description;
            Name = authReturn.Name;
            Authority = authReturn.Authority;
        }



        public void Modify()
        {
            var arrRole = new List<ItemContent>
            {
                new Role
                {
                    ID = ID,
                    Name = Name,
                    Description = Description,
                    Authority = Authority,
                    RequestInfo = new RequestContent
                    {
                        Operation = RequestOperation.Modify
                    }
                }
            };


            var results = ServiceClient.Execute(arrRole);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Delete()
        {
            var arrRole = new List<ItemContent>
            {
                new Role
                {
                    Description = Description,
                    ID = ID,
                    Name = Name,
                    Authority = Authority,
                    RequestInfo = new RequestContent
                    {
                        Operation = RequestOperation.Delete
                    }
                }
            };



            var results = ServiceClient.Execute(arrRole);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Reset();
        }


        public void Reset()
        {
            _id = -1;
            Name = string.Empty;
            Description = string.Empty;
            Authority = 0;
        }



        public List<RoleModel> GetAllRoles()
        {
            //获取EMP基本信息
            var result = ServiceClient.GetAllRoles();
            if (result.State != ResultState.Success)
            {
                throw new Exception(result.ErrorMessage);
            }

            var roleReturn = result.ResultObject.Cast<Role>().ToList();

            //写入基本信息
            var employeeList = roleReturn.Select(role => new RoleModel
            {
                Description = role.Description,
                ID = role.ID,
                Name = role.Name,
                Authority = role.Authority
            }).ToList();

            return employeeList;

        }



    }
}
