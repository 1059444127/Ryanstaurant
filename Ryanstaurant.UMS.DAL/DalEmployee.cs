using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.Entity;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.DAL
{
    public class DalEmployee
    {

        public List<Employee> GetAll()
        {
            using (var entities = new ryanstaurantEntities())
            {
                //从数据库获取相关人员的信息
                var employeeList =
                    (from e in entities.employee
                     select e).ToList();


                return employeeList.Select(employee => new Employee
                {
                    Description = employee.Description,
                    Exception = string.Empty,
                    ExceptionStackTrace = string.Empty,
                    ExpType = ExceptionType.None,
                    ID = employee.ID,
                    LoginName = employee.LoginName,
                    Name = employee.Name,
                    Password = employee.Password
                }).ToList();
            }
        }


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

                }

                return employees;

            }
        }

        public List<Employee> Add(List<Employee> employees)
        {
            var employeeList = Get(employees);

            using (var entities = new ryanstaurantEntities())
            {
                foreach (var currentEmployee in employeeList)
                {
                    currentEmployee.Exception = string.Empty;

                    if (currentEmployee.ExpType != ExceptionType.NotExist)
                    {
                        currentEmployee.Exception = "用户名为" + currentEmployee.Name + "的用户已经存在";
                        continue;
                    }

                    try
                    {
                        var resultEmp = entities.employee.Add(new employee
                        {
                            Description = currentEmployee.Description,
                            Name = currentEmployee.Name,
                            Password = currentEmployee.Password,
                            LoginName = currentEmployee.LoginName
                        });
                        currentEmployee.Exception = string.Empty;
                        currentEmployee.ID = resultEmp.ID;
                        currentEmployee.ExpType = ExceptionType.None;
                    }
                    catch (Exception ex)
                    {
                        currentEmployee.Exception = ex.Message;
                        currentEmployee.ExceptionStackTrace = ex.StackTrace;
                        currentEmployee.ExpType= ExceptionType.Failed;
                    }
                }
                var result = entities.SaveChanges();
                if (result <= 0)
                {
                    throw new Exception("添加用户信息失败");
                }

            }
            return employees;
        }

        public List<Employee> Modify(List<Employee> employees)
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

        public List<Employee> Delete(List<Employee> employees)
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
