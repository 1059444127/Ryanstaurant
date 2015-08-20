using System;
using System.Globalization;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.Entity;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllEmployee
    {
        public Employee Get(Employee employee)
        {
            var employeeFound = new Employee();
            using (var entities = new ryanstaurantEntities())
            {
                //获取人员基本属性
                var employeeEntity = (from e in entities.employee.ToList()
                    where string.Equals(e.Name, employee.Name, StringComparison.InvariantCultureIgnoreCase)
                    select e).FirstOrDefault();
                if (employeeEntity == null)
                    return employeeFound;


                employeeFound.ID = employeeEntity.ID.ToString(CultureInfo.InvariantCulture);
                employeeFound.Name = employeeEntity.Name;
                employeeFound.Password = employeeEntity.Password;

                //获取人员的角色列表
                var employeeRoles = from e in entities.emp_role.ToList()
                    where string.Equals(e.emp_id.ToString(CultureInfo.InvariantCulture) , employeeFound.ID,StringComparison.InvariantCultureIgnoreCase)
                    select e.role_id;

                //获取人员的权限
                var employeeRoleAuths = from e in entities.role_auth
                    where employeeRoles.ToList().Contains(e.role_id)
                    select e.auth_id;

                var employeeAuths = from e in entities.emp_auth
                    where
                        string.Equals(e.Emp_id.ToString(CultureInfo.InvariantCulture), employeeFound.ID, StringComparison.InvariantCultureIgnoreCase)
                    select e.Auth_id;

                //人员权限
                var auths = employeeAuths.Concat(employeeRoleAuths);

                foreach (var auth in auths)
                {
                    employeeFound.Authority |= auth;
                }


            }
            return employeeFound;

        }



    }
}
