using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.DataContract;

namespace Ryanstaurant.UMS.DAL
{
    public class DalEmployee
    {

        public List<Employee> Get(List<Employee> employees)
        {
            using (var entities = new ryanstaurantEntities())
            {
                //从传入的参数整理出需要查询的所有相关人员ID和名字
                var probEmployeeIdList = (from e in employees
                                          select e.ID).ToList();
                var probEmployeeNameList = (from e in employees
                                            select e.Name).ToList();

                //从数据库获取相关人员的信息
                var employeeList =
                    (from e in entities.employee
                     where probEmployeeIdList.Contains(e.ID) || probEmployeeNameList.Contains(e.Name)
                     select e).ToList();



                foreach (var currentEmployee in employees)
                {
                    var avaliablList = employeeList;
                    var tempEmployee = currentEmployee;
                    if (currentEmployee.ID >= 0)
                    {
                        avaliablList = (from e in employeeList where e.ID == tempEmployee.ID select e).ToList();
                        if (avaliablList.Count == 0)
                        {
                            currentEmployee.Exception = "错误的ID,如果希望通过姓名进行查询，请将ID处设置为-1";
                            continue;
                        }
                    }
                    else
                    {
                        avaliablList = (from a in avaliablList where a.Name == tempEmployee.Name select a).ToList();

                        if (avaliablList.Count == 0)
                        {
                            currentEmployee.Exception = "错误的姓名";
                            continue;
                        }
                    }
                    var employeeFound = avaliablList.First();

                    currentEmployee.ID = employeeFound.ID;

                    currentEmployee.ID = employeeFound.ID;
                    currentEmployee.Name = employeeFound.Name;
                    currentEmployee.Password = employeeFound.Password;
                    currentEmployee.Description = employeeFound.Description;
                    currentEmployee.LoginName = employeeFound.LoginName;

                    //加载权限
                    //获取人员的角色列表
                    var employeeRoles = from e in entities.emp_role.ToList()
                                        where e.emp_id == employeeFound.ID
                                        select e.role_id;

                    //获取人员的权限
                    var employeeRoleAuths = from e in entities.role_auth.ToList()
                                            where employeeRoles.ToList().Contains(e.role_id)
                                            select e.auth_id;

                    var employeeAuths = from e in entities.emp_auth.ToList()
                                        where e.Emp_id == employeeFound.ID
                                        select e.Auth_id;

                    //人员权限
                    var auths = employeeAuths.Union(employeeRoleAuths).ToList();

                    foreach (var auth in auths)
                    {
                        currentEmployee.Authority |= auth;
                    }
                }

                return employees;

            }
        }

        public List<Employee> AddEmployees(List<Employee> employees)
        {
            using (var entities = new ryanstaurantEntities())
            {
                foreach (var currentEmployee in employees)
                {
                    currentEmployee.Exception = string.Empty;

                    if ((from e in entities.employee where e.Name == currentEmployee.Name select e).FirstOrDefault() != null)
                    {
                        currentEmployee.Exception = "用户名为" + currentEmployee.Name + "的用户已经存在";
                        continue;
                    }

                    try
                    {
                        entities.employee.Add(new employee
                        {
                            Description = currentEmployee.Description,
                            Name = currentEmployee.Name,
                            Password = currentEmployee.Password,
                            LoginName = currentEmployee.LoginName
                        });
                        currentEmployee.Exception = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        currentEmployee.Exception = ex.Message;
                    }
                }
                var result = entities.SaveChanges();
                if (result <= 0)
                {
                    throw new Exception("添加用户失败");
                }

            }
            return employees;
        }

        public List<Employee> ModifyEmployees(List<Employee> employees)
        {
            using (var entities = new ryanstaurantEntities())
            {
                foreach (var employee in employees)
                {
                    employee.Exception = string.Empty;

                    try
                    {
                        var currentemployee =
                            (from e in entities.employee where e.ID == employee.ID select e).FirstOrDefault();

                        if (currentemployee == null)
                        {
                            employee.Exception = "用户ID为" + employee.ID + "的用户不存在";
                            continue;
                        }

                        currentemployee.Name = employee.Name;
                        currentemployee.LoginName = employee.LoginName;
                        currentemployee.Password = employee.Password;
                        currentemployee.Description = employee.Description;
                        employee.Exception = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        employee.Exception = ex.Message;
                    }
                }
                var result = entities.SaveChanges();
                if (result <= 0)
                {
                    throw new Exception("修改用户基本信息失败");
                }
            }
            return employees;
        }

        public List<Employee> DeleteEmployees(List<Employee> employees)
        {
            using (var entities = new ryanstaurantEntities())
            {
                foreach (var employee in employees)
                {
                    employee.Exception = string.Empty;

                    try
                    {
                        var currentemployee =
                            (from e in entities.employee where e.ID == employee.ID select e).FirstOrDefault();

                        if (currentemployee == null)
                        {
                            employee.Exception = "用户ID为" + employee.ID + "的用户不存在";
                            continue;
                        }

                        entities.employee.Remove(currentemployee);

                    }
                    catch (Exception ex)
                    {
                        employee.Exception = ex.Message;
                    }
                }
                var result = entities.SaveChanges();
                if (result <= 0)
                {
                    throw new Exception("删除用户失败");
                }
            }
            return employees;
        }
    }
}
