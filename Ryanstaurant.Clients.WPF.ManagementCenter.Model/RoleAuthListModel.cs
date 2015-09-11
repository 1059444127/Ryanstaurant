using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model.UMSProxy;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class RoleAuthListModel
    {

        protected readonly UMSServiceClient ServiceClient = new UMSServiceClient();

        public List<RoleAuthModel> Collection { get; set; }


        public RoleAuthListModel()
        {
            Collection= new List<RoleAuthModel>();
            Refresh();
        }



        private int _authId = -1;
        private int _roleId = -1;



        public void Add(int employeeId, int authorityId)
        {
            var arrAuthorities = new RoleAuth[1];
            arrAuthorities[0] = new RoleAuth
            {
                RoleID = employeeId,
                AuthID = authorityId
            };

            var results = ServiceClient.AddRoleAuths(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Add(int employeeId, List<int> authorityIds)
        {
            var arrAuthorities = new RoleAuth[authorityIds.Count];
            for (int i = 0; i < arrAuthorities.Length; i++)
            {
                arrAuthorities[i] = new RoleAuth
                {
                    RoleID = employeeId,
                    AuthID = authorityIds[i]
                };
            }


            var results = ServiceClient.AddRoleAuths(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Add(List<int> employeeIds, int authorityId)
        {
            var arrAuthorities = new RoleAuth[employeeIds.Count];
            for (int i = 0; i < arrAuthorities.Length; i++)
            {
                arrAuthorities[i] = new RoleAuth
                {
                    RoleID = employeeIds[i],
                    AuthID = authorityId
                };
            }


            var results = ServiceClient.AddRoleAuths(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }


        public void Modify(int employeeId, int authorityId)
        {
            var arrAuthority = new RoleAuth[1];
            arrAuthority[0] = new RoleAuth
            {
                RoleID = employeeId,
                AuthID = authorityId
            };


            var results = ServiceClient.ModifyRoleAuths(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Modify(int employeeId, List<int> authorityIds)
        {
            var arrAuthorities = new RoleAuth[authorityIds.Count];
            for (var i = 0; i < arrAuthorities.Length; i++)
            {
                arrAuthorities[i] = new RoleAuth
                {
                    RoleID = employeeId,
                    AuthID = authorityIds[i]
                };
            }

            var results = ServiceClient.ModifyRoleAuths(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Modify(List<int> employeeIds, int authorityId)
        {
            var arrAuthorities = new RoleAuth[employeeIds.Count];
            for (var i = 0; i < arrAuthorities.Length; i++)
            {
                arrAuthorities[i] = new RoleAuth
                {
                    RoleID = employeeIds[i],
                    AuthID = authorityId
                };
            }

            var results = ServiceClient.ModifyRoleAuths(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }


        public void Delete(int employeeId, int authorityId)
        {
            var arrAuthority = new RoleAuth[1];
            arrAuthority[0] = new RoleAuth
            {
                RoleID = employeeId,
                AuthID = authorityId
            };

            var results = ServiceClient.DeleteRoleAuths(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Delete(int employeeId, List<int> authorityIds)
        {
            var arrAuthority = new RoleAuth[authorityIds.Count];

            for (var i = 0; i < arrAuthority.Length; i++)
            {
                arrAuthority[i] = new RoleAuth
                {
                    RoleID = employeeId,
                    AuthID = authorityIds[i]
                };
            }

            var results = ServiceClient.DeleteRoleAuths(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Delete(List<int> employeeIds, int authorityId)
        {
            var arrAuthority = new RoleAuth[employeeIds.Count];

            for (var i = 0; i < arrAuthority.Length; i++)
            {
                arrAuthority[i] = new RoleAuth
                {
                    RoleID = employeeIds[i],
                    AuthID = authorityId
                };
            }

            var results = ServiceClient.DeleteRoleAuths(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }





        public void Refresh()
        {
            var arrAuthorities = new RoleAuth[1]
            {
                new RoleAuth {AuthID = _authId, RoleID = _roleId}
            };

            var results = ServiceClient.GetRoleAuths(arrAuthorities);
            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            var authReturn = results.ResultObject;

            Collection.Clear();
            Collection = authReturn.ToList().Select(ra=>new RoleAuthModel
            {
                AuthId = ra.AuthID,
                RoleId = ra.RoleID
            }).ToList();

        }



    }
}
