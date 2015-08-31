using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.Interface;
using Ryanstaurant.UMS.WorkSpace;


namespace Ryanstaurant.UMS.Service
{
    public class UMSService:IUMSService
    {

        public ResultEntity<List<Employee>> AddEmployees(List<Employee> employees)
        {

            return LoadBusinessMethod(() => new BllEmployee().Add(employees));
        }

        public ResultEntity<List<Employee>> ModifyEmployees(List<Employee> employees)
        {
            return LoadBusinessMethod(() => new BllEmployee().Modify(employees));
        }

        public ResultEntity<List<Employee>> DeleteEmployees(List<Employee> employees)
        {
            return LoadBusinessMethod(() => new BllEmployee().Delete(employees));
        }



        protected ResultEntity<T> LoadBusinessMethod<T>(Func<T> methodHandler)
        {
            try
            {
                return new ResultEntity<T>
                {
                    ErrorMessage = string.Empty,
                    ResultObject = methodHandler(),
                    State = ResultState.Success
                };
            }
            catch (Exception ex)
            {
                return new ResultEntity<T>
                {
                    InnerErrorMessage = ex.InnerException == null ? string.Empty : ex.InnerException.Message,
                    ErrorMessage = ex.Message,
                    ResultObject = default(T),
                    State = ResultState.Fail
                };
            }
        }


        public ResultEntity<List<Employee>> GetEmployees(List<Employee> employees)
        {
            return LoadBusinessMethod(() => new BllEmployee().Get(employees));
        }




        public ResultEntity<List<Role>> GetRoles(List<Role> roles)
        {
            return LoadBusinessMethod(() => new BllRole().Get(roles));
        }

        public ResultEntity<List<Role>> AddRoles(List<Role> roles)
        {
            return LoadBusinessMethod(() => new BllRole().Add(roles));
        }

        public ResultEntity<List<Role>> ModifyRoles(List<Role> roles)
        {
            return LoadBusinessMethod(() => new BllRole().Modify(roles));
        }

        public ResultEntity<List<Role>> DeleteRoles(List<Role> roles)
        {
            return LoadBusinessMethod(() => new BllRole().Delete(roles));
        }

        public ResultEntity<List<Authority>> GetAuthorities(List<Authority> authorities)
        {
            return LoadBusinessMethod(() => new BllAuthority().Get(authorities));
        }

        public ResultEntity<List<Authority>> AddAuthorities(List<Authority> authorities)
        {
            return LoadBusinessMethod(() => new BllAuthority().Add(authorities));
        }

        public ResultEntity<List<Authority>> ModifyAuthorities(List<Authority> authorities)
        {
            return LoadBusinessMethod(() => new BllAuthority().Modify(authorities));
        }

        public ResultEntity<List<Authority>> DeleteAuthorities(List<Authority> authorities)
        {
            return LoadBusinessMethod(() => new BllAuthority().Delete(authorities));
        }

        public ResultEntity<List<EmpRole>> GetEmpRoles(List<EmpRole> empRoles)
        {
            return LoadBusinessMethod(() => new BllEmpRole().Get(empRoles));
        }

        public ResultEntity<List<EmpRole>> AddEmpRoles(List<EmpRole> empRoles)
        {
            return LoadBusinessMethod(() => new BllEmpRole().Add(empRoles));
        }

        public ResultEntity<List<EmpRole>> ModifyEmpRoles(List<EmpRole> empRoles)
        {
            return LoadBusinessMethod(() => new BllEmpRole().Modify(empRoles));
        }

        public ResultEntity<List<EmpRole>> DeleteEmpRoles(List<EmpRole> empRoles)
        {
            return LoadBusinessMethod(() => new BllEmpRole().Delete(empRoles));
        }

        public ResultEntity<List<RoleAuth>> GetRoleAuths(List<RoleAuth> roleAuths)
        {
            return LoadBusinessMethod(() => new BllRoleAuth().Get(roleAuths));
        }

        public ResultEntity<List<RoleAuth>> AddRoleAuths(List<RoleAuth> roleAuths)
        {
            return LoadBusinessMethod(() => new BllRoleAuth().Add(roleAuths));
        }

        public ResultEntity<List<RoleAuth>> ModifyRoleAuths(List<RoleAuth> roleAuths)
        {
            return LoadBusinessMethod(() => new BllRoleAuth().Modify(roleAuths));
        }

        public ResultEntity<List<RoleAuth>> DeleteRoleAuths(List<RoleAuth> roleAuths)
        {
            return LoadBusinessMethod(() => new BllRoleAuth().Delete(roleAuths));
        }

        public ResultEntity<List<EmpAuth>> GetEmpAuths(List<EmpAuth> empAuths)
        {
            return LoadBusinessMethod(() => new BllEmpAuth().Get(empAuths));
        }

        public ResultEntity<List<EmpAuth>> AddEmpAuths(List<EmpAuth> empAuths)
        {
            return LoadBusinessMethod(() => new BllEmpAuth().Add(empAuths));
        }

        public ResultEntity<List<EmpAuth>> ModifyEmpAuths(List<EmpAuth> empAuths)
        {
            return LoadBusinessMethod(() => new BllEmpAuth().Modify(empAuths));
        }
        public ResultEntity<List<EmpAuth>> DeleteEmpAuths(List<EmpAuth> empAuths)
        {
            return LoadBusinessMethod(() => new BllEmpAuth().Delete(empAuths));
        }
    }
}
