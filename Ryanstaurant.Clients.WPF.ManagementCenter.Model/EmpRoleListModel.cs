using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model.UMSProxy;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public  class EmpRoleListModel
    {

        protected readonly UMSServiceClient ServiceClient = new UMSServiceClient();

        public List<EmpRoleModel> Collection { get; set; }


        public EmpRoleListModel()
        {
            Collection = new List<EmpRoleModel>();
            Refresh();
        }


        public EmpRoleListModel(int empId,int roleId)
        {
            _empId = empId;
            _roleId = roleId;
            Collection = new List<EmpRoleModel>
            {
                new EmpRoleModel
                {
                    EmpId=empId,
                    RoleId = roleId
                }
            };
            Refresh();
        }


        private int _empId = -1;
        private int _roleId = -1;



        public void Add(int employeeId, int authorityId)
        {
            var arrAuthorities = new EmpRole[1];
            arrAuthorities[0] = new EmpRole
            {
                EmpID = employeeId,
                RoleID = authorityId
            };

            var results = ServiceClient.AddEmpRoles(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Add(int employeeId, List<int> authorityIds)
        {
            var arrAuthorities = new EmpRole[authorityIds.Count];
            for (int i = 0; i < arrAuthorities.Length; i++)
            {
                arrAuthorities[i] = new EmpRole
                {
                    EmpID = employeeId,
                    RoleID = authorityIds[i]
                };
            }


            var results = ServiceClient.AddEmpRoles(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Add(List<int> employeeIds, int authorityId)
        {
            var arrAuthorities = new EmpRole[employeeIds.Count];
            for (int i = 0; i < arrAuthorities.Length; i++)
            {
                arrAuthorities[i] = new EmpRole
                {
                    EmpID = employeeIds[i],
                    RoleID = authorityId
                };
            }


            var results = ServiceClient.AddEmpRoles(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }


        public void Modify(int employeeId, int authorityId)
        {
            var arrAuthority = new EmpRole[1];
            arrAuthority[0] = new EmpRole
            {
                EmpID = employeeId,
                RoleID = authorityId
            };


            var results = ServiceClient.ModifyEmpRoles(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Modify(int employeeId, List<int> authorityIds)
        {
            var arrAuthorities = new EmpRole[authorityIds.Count];
            for (var i = 0; i < arrAuthorities.Length; i++)
            {
                arrAuthorities[i] = new EmpRole
                {
                    EmpID = employeeId,
                    RoleID = authorityIds[i]
                };
            }

            var results = ServiceClient.ModifyEmpRoles(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Modify(List<int> employeeIds, int authorityId)
        {
            var arrAuthorities = new EmpRole[employeeIds.Count];
            for (var i = 0; i < arrAuthorities.Length; i++)
            {
                arrAuthorities[i] = new EmpRole
                {
                    EmpID = employeeIds[i],
                    RoleID = authorityId
                };
            }

            var results = ServiceClient.ModifyEmpRoles(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }


        public void Delete(int employeeId, int authorityId)
        {
            var arrAuthority = new EmpRole[1];
            arrAuthority[0] = new EmpRole
            {
                EmpID = employeeId,
                RoleID = authorityId
            };

            var results = ServiceClient.DeleteEmpRoles(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Delete(int employeeId, List<int> authorityIds)
        {
            var arrAuthority = new EmpRole[authorityIds.Count];

            for (var i = 0; i < arrAuthority.Length; i++)
            {
                arrAuthority[i] = new EmpRole
                {
                    EmpID = employeeId,
                    RoleID = authorityIds[i]
                };
            }

            var results = ServiceClient.DeleteEmpRoles(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Delete(List<int> employeeIds, int authorityId)
        {
            var arrAuthority = new EmpRole[employeeIds.Count];

            for (var i = 0; i < arrAuthority.Length; i++)
            {
                arrAuthority[i] = new EmpRole
                {
                    EmpID = employeeIds[i],
                    RoleID = authorityId
                };
            }

            var results = ServiceClient.DeleteEmpRoles(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }








        public void Refresh()
        {
            var array = new EmpRole[1]
            {
                new EmpRole {EmpID = _empId, RoleID = _roleId}
            };

            var results = ServiceClient.GetEmpRoles(array);
            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);



            var authReturn = results.ResultObject;

            Collection.Clear();

            if (authReturn.Length == 0)
                Collection = new List<EmpRoleModel>();


            Collection = authReturn.ToList().Select(er => new EmpRoleModel
            {
                EmpId = er.EmpID,
                RoleId = er.RoleID
            }).ToList();

        }


    }
}
