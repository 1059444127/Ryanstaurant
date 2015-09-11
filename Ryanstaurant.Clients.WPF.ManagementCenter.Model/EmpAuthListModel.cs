using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.UMS.Client;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class EmpAuthListModel
    {

        protected readonly UMSClient ServiceClient = new UMSClient();

        public EmpAuthListModel()
        {
            Collection = new List<EmpAuthModel>();
            Refresh();
        }

        private int _empId = -1;
        private int _authId = -1;

        public EmpAuthListModel(int empId, int authId)
        {
            _empId = empId;
            _authId = authId;

            Collection = new List<EmpAuthModel>
            {
                new EmpAuthModel
                {
                    EmpId=empId,
                    AuthId = authId
                }
            };
            Refresh();
        }





        public List<EmpAuthModel> Collection { get; set; }




        public void Add(int employeeId, int authorityId)
        {
            var arrAuthorities = new EmpAuth[1];
            arrAuthorities[0] = new EmpAuth
            {
                EmpID = employeeId,
                AuthID = authorityId
            };

            var results = ServiceClient.AddEmpAuths(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Add(int employeeId, List<int> authorityIds)
        {
            var arrAuthorities = new EmpAuth[authorityIds.Count];
            for (int i = 0; i < arrAuthorities.Length; i++)
            {
                arrAuthorities[i] = new EmpAuth
                {
                    EmpID = employeeId,
                    AuthID = authorityIds[i]
                };
            }
            
            
            var results = ServiceClient.AddEmpAuths(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Add(List<int> employeeIds, int authorityId)
        {
            var arrAuthorities = new EmpAuth[employeeIds.Count];
            for (int i = 0; i < arrAuthorities.Length; i++)
            {
                arrAuthorities[i] = new EmpAuth
                {
                    EmpID = employeeIds[i],
                    AuthID = authorityId
                };
            }


            var results = ServiceClient.AddEmpAuths(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }


        public void Modify(int employeeId, int authorityId)
        {
            var arrAuthority = new EmpAuth[1];
            arrAuthority[0] = new EmpAuth
            {
                EmpID = employeeId,
                AuthID = authorityId
            };


            var results = ServiceClient.ModifyEmpAuths(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Modify(int employeeId, List<int> authorityIds)
        {
            var arrAuthorities = new EmpAuth[authorityIds.Count];
            for (var i = 0; i < arrAuthorities.Length; i++)
            {
                arrAuthorities[i] = new EmpAuth
                {
                    EmpID = employeeId,
                    AuthID = authorityIds[i]
                };
            }

            var results = ServiceClient.ModifyEmpAuths(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Modify(List<int> employeeIds, int authorityId)
        {
            var arrAuthorities = new EmpAuth[employeeIds.Count];
            for (var i = 0; i < arrAuthorities.Length; i++)
            {
                arrAuthorities[i] = new EmpAuth
                {
                    EmpID = employeeIds[i],
                    AuthID = authorityId
                };
            }

            var results = ServiceClient.ModifyEmpAuths(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }


        public void Delete(int employeeId, int authorityId)
        {
            var arrAuthority = new EmpAuth[1];
            arrAuthority[0] = new EmpAuth
            {
                EmpID = employeeId,
                AuthID = authorityId
            };

            var results = ServiceClient.DeleteEmpAuths(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Delete(int employeeId, List<int> authorityIds)
        {
            var arrAuthority = new EmpAuth[authorityIds.Count];

            for (var i = 0; i < arrAuthority.Length; i++)
            {
                arrAuthority[i] = new EmpAuth
                {
                    EmpID = employeeId,
                    AuthID = authorityIds[i]
                };
            }

            var results = ServiceClient.DeleteEmpAuths(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Delete(List<int> employeeIds, int authorityId)
        {
            var arrAuthority = new EmpAuth[employeeIds.Count];

            for (var i = 0; i < arrAuthority.Length; i++)
            {
                arrAuthority[i] = new EmpAuth
                {
                    EmpID = employeeIds[i],
                    AuthID = authorityId
                };
            }

            var results = ServiceClient.DeleteEmpAuths(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }




        public void Refresh()
        {
            var arrAuthorities = new EmpAuth[1]
            {
                new EmpAuth
                {
                    AuthID = _authId,
                    EmpID = _empId
                }
            };

            var results = ServiceClient.GetEmpAuths(arrAuthorities);
            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            var authReturn = results.ResultObject;

            Collection.Clear();
            Collection = authReturn.ToList().Select(ea=>new EmpAuthModel
            {
                EmpId=ea.EmpID,
                AuthId = ea.AuthID
            }).ToList();

        }















    }
}
