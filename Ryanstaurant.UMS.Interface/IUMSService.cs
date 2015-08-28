using System.Collections.Generic;
using System.ServiceModel;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.Interface
{
    [ServiceContract]
    public interface IUMSService
    {
        [OperationContract]
        ResultEntity<List<Employee>> GetEmployees(List<Employee> employees);

        [OperationContract]
        ResultEntity<List<Employee>> AddEmployees(List<Employee> employees);

        [OperationContract]
        ResultEntity<List<Employee>> ModifyEmployees(List<Employee> employees);

        [OperationContract]
        ResultEntity<List<Employee>> DeleteEmployees(List<Employee> employees);



        [OperationContract]
        ResultEntity<List<Role>> GetRoles(List<Role> roles);

        [OperationContract]
        ResultEntity<List<Role>> AddRoles(List<Role> roles);

        [OperationContract]
        ResultEntity<List<Role>> ModifyRoles(List<Role> roles);

        [OperationContract]
        ResultEntity<List<Role>> DeleteRoles(List<Role> roles);




        [OperationContract]
        ResultEntity<List<Authority>> GetAuthorities(List<Authority> authorities);

        [OperationContract]
        ResultEntity<List<Authority>> AddAuthorities(List<Authority> authorities);

        [OperationContract]
        ResultEntity<List<Authority>> ModifyAuthorities(List<Authority> authorities);

        [OperationContract]
        ResultEntity<List<Authority>> DeleteAuthorities(List<Authority> authorities);





        [OperationContract]
        ResultEntity<List<EmpRole>> GetEmpRoles(List<EmpRole> empRoles);

        [OperationContract]
        ResultEntity<List<EmpRole>> AddEmpRoles(List<EmpRole> empRoles);

        [OperationContract]
        ResultEntity<List<EmpRole>> ModifyEmpRoles(List<EmpRole> empRoles);

        [OperationContract]
        ResultEntity<List<EmpRole>> DeleteEmpRoles(List<EmpRole> empRoles);



        [OperationContract]
        ResultEntity<List<RoleAuth>> GetRoleAuths(List<RoleAuth> roleAuths);

        [OperationContract]
        ResultEntity<List<RoleAuth>> AddRoleAuths(List<RoleAuth> roleAuths);

        [OperationContract]
        ResultEntity<List<RoleAuth>> ModifyRoleAuths(List<RoleAuth> roleAuths);

        [OperationContract]
        ResultEntity<List<RoleAuth>> DeleteRoleAuths(List<RoleAuth> roleAuths);


        [OperationContract]
        ResultEntity<List<EmpAuth>> GetEmpAuths(List<EmpAuth> empAuths);

        [OperationContract]
        ResultEntity<List<EmpAuth>> AddEmpAuths(List<EmpAuth> empAuths);

        [OperationContract]
        ResultEntity<List<EmpAuth>> ModifyEmpAuths(List<EmpAuth> empAuths);

        [OperationContract]
        ResultEntity<List<EmpAuth>> DeleteEmpAuths(List<EmpAuth> empAuths);



    }
}
