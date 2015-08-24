using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DAL;
using Ryanstaurant.UMS.DataContract;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllEmployee
    {
        public Employee Get(Employee employee)
        {
            #region ORG
            //var employeeFound = new Employee();
            //using (var entities = new ryanstaurantEntities())
            //{

                //    //获取人员基本属性
                //    var employeeEntity = (from e in entities.employee.ToList()
                //        where string.Equals(e.Name, employee.Name, StringComparison.InvariantCultureIgnoreCase)
                //        select e).FirstOrDefault();
                //    if (employeeEntity == null)
                //        return employeeFound;


                //    employeeFound.ID = employeeEntity.ID;
                //    employeeFound.Name = employeeEntity.Name;
                //    employeeFound.Password = employeeEntity.Password;
                //    employeeFound.Description = employeeEntity.Description;

                //    //获取人员的角色列表
                //    var employeeRoles = from e in entities.emp_role.ToList()
                //        where e.emp_id == employeeFound.ID
                //        select e.role_id;

                //    //获取人员的权限
                //    var employeeRoleAuths = from e in entities.role_auth.ToList()
                //        where employeeRoles.ToList().Contains(e.role_id)
                //        select e.auth_id;

                //    var employeeAuths = from e in entities.emp_auth.ToList()
                //        where e.Emp_id == employeeFound.ID
                //        select e.Auth_id;

                //    //人员权限
                //    var auths = employeeAuths.Union(employeeRoleAuths).ToList();

                //    foreach (var auth in auths)
                //    {
                //        employeeFound.Authority |= auth;
                //    }


                //}
                //return employeeFound;
            #endregion
            return Get(new List<Employee> {employee}).FirstOrDefault();

        }


        public List<Employee> Get(List<Employee> employees)
        {
            return new DalEmployee().Get(employees);
        }

        public List<Employee> AddEmployees(List<Employee> employees)
        {
            return new DalEmployee().AddEmployees(employees);
        }

        public List<Employee> ModifyEmployees(List<Employee> employees)
        {
            return new DalEmployee().ModifyEmployees(employees);
        }

        public List<Employee> DeleteEmployees(List<Employee> employees)
        {
            return new DalEmployee().DeleteEmployees(employees);
        }




    }
}
