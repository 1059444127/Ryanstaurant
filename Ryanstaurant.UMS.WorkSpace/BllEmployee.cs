using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllEmployee
    {

        public List<ResultContent<Employee>> QueryEmployees(RequestEntitiy<Employee> employees)
        {
            if (employees.RequestContents == null || employees.RequestContents.Count == 0)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents为空"));
            }

            if (employees.RequestContents.Count == 1 && employees.RequestContents[0] == null)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含操作"));
            }


            var resultEntity = new List<ResultContent<Employee>>();

            if (employees.QueryContentList != null)
            {
                resultEntity = Query(employees.QueryContentList);
            }


            return resultEntity;
        }


        public List<ResultContent<Employee>> ExcuteEmployees(RequestEntitiy<Employee> employees)
        {
            if (employees.RequestContents == null || employees.RequestContents.Count == 0)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents为空"));
            }

            if (employees.RequestContents.Count == 1 && employees.RequestContents[0] == null)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含操作"));
            }


            var resultEntity = new List<ResultContent<Employee>>();

            resultEntity = Save(employees);
            return resultEntity;
        }


        internal List<ResultContent<Employee>> Query(List<Employee> employees)
        {
            var resultEntity = new List<ResultContent<Employee>>();

            using (var entities = new ryanstaurantEntities())
            {
                List<employee> empList;
                List<emp_role> empRoleList;
                List<emp_auth> empAuthList;
                List<role_auth> roleAuthList;
                List<role> roleList;
                List<authority> authList;

                //没有指定，则返回所有查询结果
                if (!employees.Any())
                {
                    empList = (from e in entities.employee select e).ToList();
                    empRoleList = (from e in entities.emp_role select e).ToList();
                    empAuthList = (from e in entities.emp_auth select e).ToList();
                    roleAuthList = (from e in entities.role_auth select e).ToList();
                    roleList = (from e in entities.role select e).ToList();
                    authList = (from e in entities.authority select e).ToList();
                }
                else//有指定，则从传送的数据处进行查询
                {
                    var empIDList = (from e in employees
                        select e.ID).ToList();

                    var empNameList = (from e in employees
                                     select e.Name).ToList();


                    empList = (from e in entities.employee where empIDList.Contains(e.ID) || empNameList.Contains(e.Name) select e).ToList();
                    empRoleList = (from e in entities.emp_role where empIDList.Contains(e.emp_id) select e).ToList();
                    empAuthList = (from e in entities.emp_auth where empIDList.Contains(e.Emp_id) select e).ToList();
                    roleAuthList = (from e in entities.role_auth select e).ToList();
                    roleList = (from e in entities.role select e).ToList();
                    authList = (from e in entities.authority select e).ToList();
                }



                //人员基本信息
                foreach (var employee in empList)
                {
                    var currentEmp = employee;

                    var resultEmployee = new ResultContent<Employee>
                    {
                        State = ResultState.Success,
                        Exception = string.Empty,
                        InnerErrorMessage = string.Empty,
                        Item = new Employee
                        {
                            ID = currentEmp.ID,
                            Description = currentEmp.Description,
                            LoginName = currentEmp.LoginName,
                            Name = currentEmp.Name,
                            Password = currentEmp.Password
                        }
                    };

                    var roleIDList = (from er in empRoleList
                        where er.emp_id == currentEmp.ID
                        select er.role_id).ToList();

                    resultEmployee.Item.Roles = (from r in roleList
                        where roleIDList.Contains(r.id)
                        select new Role
                        {
                            Description = r.Description,
                            ID = r.id,
                            Name = r.Name
                        }).ToList();


                    foreach (var role in resultEmployee.Item.Roles)
                    {
                        var rAuthIDList = (from a in roleAuthList
                            where role.ID == a.role_id
                            select a.auth_id).ToList();

                        role.Authorities = (from a in authList
                            where rAuthIDList.Contains(a.id)
                            select new Authority
                            {
                                Description = a.Description,
                                ID = a.id,
                                KeyCode = a.KeyCode.GetValueOrDefault(0),
                                Name = a.Name
                            }).ToList();
                    }

                    var authIDList = (from a in empAuthList
                        where a.Emp_id == currentEmp.ID
                        select a.Auth_id).ToList();

                    resultEmployee.Item.Authorities = (from a in authList
                        where authIDList.Contains(a.id)
                        select new Authority
                        {
                            Description = a.Description,
                            ID = a.id,
                            KeyCode = a.KeyCode.GetValueOrDefault(0),
                            Name = a.Name
                        }).ToList();

                    resultEntity.Add(resultEmployee);
                }

            }

            return resultEntity;

        }


        internal List<ResultContent<Employee>> Save(RequestEntitiy<Employee> requestEntitiies)
        {
            var resultEntity = new List<ResultContent<Employee>>();

            using (var entities = new ryanstaurantEntities())
            {

                foreach (var content in requestEntitiies.RequestContents)
                {
                    var resultcontent = new ResultContent<Employee>();


                    if (content == null)
                    {
                        resultcontent.Exception = "未设置操作类型";
                        resultcontent.InnerErrorMessage = "ResultContent为NULL";
                        resultcontent.Item = null;
                        resultcontent.State = ResultState.Fail;
                    }

                    switch (content.Operation)
                    {
                        case RequestOperation.Add:
                        {
                            resultcontent = AddEmployee(entities, content);
                        }
                            break;
                        case RequestOperation.Modify:
                        {
                            resultcontent = ModifyEmployee(entities, content);
                        }
                            break;
                        case RequestOperation.Delete:
                        {
                            resultcontent = DeleteEmployee(entities, content);
                        }
                            break;
                        default:
                        {
                            resultcontent.Exception = "错误的操作类型";
                            resultcontent.InnerErrorMessage = "RequestOperation=" +
                                                              Enum.GetName(typeof (RequestOperation), content.Operation);
                            resultcontent.Item = null;
                            resultcontent.State = ResultState.Fail;
                        }
                            break;
                    }
                    resultEntity.Add(resultcontent);

                }
            }
            return resultEntity;
        }


        private static ResultContent<Employee> DeleteEmployee(ryanstaurantEntities entities, RequestContent<Employee> employeeContent)
        {
            var employee = employeeContent.RequestObject;

            var employeeInDb = (from e in entities.employee where e.ID == employeeContent.RequestObject.ID select e).FirstOrDefault();

            if (employeeInDb == null)
            {
                return new ResultContent<Employee>
                {
                    Exception = "ID为[" + employee.ID + "]的人员不存在，没有进行删除操作",
                    InnerErrorMessage = string.Empty,
                    Item = employee,
                    State = ResultState.Success
                };
            }


            entities.employee.Remove(employeeInDb);



            //删除已存在的
            var roleListExist = (from e in entities.emp_role where e.emp_id == employee.ID select e).ToList();

            if (roleListExist.Any())
                entities.emp_role.RemoveRange(roleListExist);


            //删除已存在的
            var authListExist = (from e in entities.emp_auth where e.Emp_id == employee.ID select e).ToList();

            if (authListExist.Any())
                entities.emp_auth.RemoveRange(authListExist);

            entities.SaveChanges();


            return new ResultContent<Employee>
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                Item = employee,
                State = ResultState.Success

            };

        }

        private static ResultContent<Employee> ModifyEmployee(ryanstaurantEntities entities, RequestContent<Employee> employeeContent)
        {

            var employee = employeeContent.RequestObject;

            var employeeInDb = (from e in entities.employee where e.ID == employeeContent.RequestObject.ID select e).FirstOrDefault();

            if (employeeInDb == null)
            {
                return new ResultContent<Employee>
                {
                    Exception = "ID为["+employee.ID+"]的人员不存在，没有进行修改操作",
                    InnerErrorMessage = string.Empty,
                    Item = employee,
                    State = ResultState.Success
                };
            }


            employeeInDb.Description = employee.Description;
            employeeInDb.LoginName = employee.LoginName;
            employeeInDb.Name = employee.Name;
            employeeInDb.Password = employee.Password;



            //删除已存在的
            var roleListExist = (from e in entities.emp_role where e.emp_id == employee.ID select e).ToList();


            if (roleListExist.Any())
            entities.emp_role.RemoveRange(roleListExist);


            //权限关联关系采用先删除再添加的方式处理
            foreach (var role in employee.Roles)
            {

                //添加新的
                entities.emp_role.Add(new emp_role
                {
                    emp_id = employee.ID,
                    role_id = role.ID
                });
            }



            //删除已存在的
            var authListExist = (from e in entities.emp_auth where e.Emp_id == employee.ID select e).ToList();

            if (authListExist.Any())
            entities.emp_auth.RemoveRange(authListExist);

            foreach (var auth in employee.Authorities)
            {

                entities.emp_auth.Add(new emp_auth
                {
                    Emp_id = employee.ID,
                    Auth_id = auth.ID
                });
            }

            entities.SaveChanges();


            return new ResultContent<Employee>
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                Item = employee,
                State = ResultState.Success

            };


        }

        private static ResultContent<Employee> AddEmployee(ryanstaurantEntities entities, RequestContent<Employee> employeeContent)
        {
            var employee = employeeContent.RequestObject;


            var empToAdd = new employee
            {
                Description = employee.Description,
                LoginName = employee.LoginName,
                Name = employee.Name,
                Password = employee.Password
            };

            entities.employee.Add(empToAdd);


            foreach (var role in employee.Roles)
            {
                entities.emp_role.Add(new emp_role
                {
                    emp_id=employee.ID,
                    role_id = role.ID
                });

            }

            foreach (var auth in employee.Authorities)
            {
                entities.emp_auth.Add(new emp_auth
                {
                    Emp_id = employee.ID,
                    Auth_id = auth.ID
                });
            }

            entities.SaveChanges();

            employee.ID = empToAdd.ID;

            return new ResultContent<Employee>
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                Item = employee,
                State = ResultState.Success
                
            };
        }
    }
}
