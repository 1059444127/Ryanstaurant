using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.UMS.Client;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class RoleListModel
    {
        public List<RoleModel> Collection { get; set; }


        protected readonly UMSClient ServiceClient = new UMSClient();


        public RoleListModel()
        {
            Collection = GetAllRoles();
        }




        public List<RoleModel> GetRoles(List<int> roleIds)
        {
            if (Collection == null) return null;

            return (from r in Collection
                where roleIds.Contains(r.ID)
                select r).ToList();
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
                Authority=role.Authority
            }).ToList();

            return employeeList;

        }
    }
}
