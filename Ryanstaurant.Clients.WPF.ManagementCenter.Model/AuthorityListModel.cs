using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.UMS.Client;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class AuthorityListModel
    {

        public List<AuthorityModel> Collection { get; set; }
        protected readonly UMSClient ServiceClient = new UMSClient();


        public AuthorityListModel()
        {
            Collection = Refresh();
        }




        public List<AuthorityModel> Refresh()
        {

            //获取EMP基本信息
            var result = ServiceClient.GetAllAuthorities();
            if (result.State != ResultState.Success)
            {
                throw new Exception(result.ErrorMessage);
            }

            var authReturn = result.ResultObject.Cast<Authority>().ToList();

            //写入基本信息
            var employeeList = authReturn.Select(auth => new AuthorityModel
            {
                Description = auth.Description,
                ID = auth.ID,
                Name = auth.Name
            }).ToList();

            return employeeList;

        }


        public List<AuthorityModel> GetAuthorities(List<int> authIds)
        {
            if (Collection == null) return null;

            return (from r in Collection
                    where authIds.Contains(r.ID)
                    select r).ToList();
        }

        public List<AuthorityModel> GetAllAuthorities()
        {
            //获取EMP基本信息
            var result = ServiceClient.GetAllAuthorities();
            if (result.State != ResultState.Success)
            {
                throw new Exception(result.ErrorMessage);
            }

            var authReturn = result.ResultObject.Cast<Authority>().ToList();

            //写入基本信息
            var employeeList = authReturn.Select(auth => new AuthorityModel
            {
                Description = auth.Description,
                ID = auth.ID,
                Name = auth.Name
            }).ToList();

            return employeeList;

        }

    }
}
