using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllEmployee:BllBase
    {
        public new UmsEntities Entities
        {
            get
            {
                return base.Entities;
            }
            set
            {
                base.Entities = value;
            }
        }








        public List<ItemContent> QueryEmployee(List<ItemContent> employees)
        {
            if (employees == null || employees.Count == 0)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents为空"));
            }

            if (employees.Count == 1 && employees[0] == null)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含操作"));
            }


            var queries =
                (from e in employees
                    where e.CommandInfo != null && e.CommandInfo.Operation == RequestOperation.Query
                    select e).ToList();

            return Query(!queries.Any() ? null : queries.ToList());
        }


        public List<ItemContent> ExecuteEmployee(List<ItemContent> employees)
        {
            if (employees == null || employees.Count == 0)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents为空"));
            }

            if (employees.Count == 1 && employees[0] == null)
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含操作"));
            }

            var queries =
                (from e in employees
                where e.CommandInfo != null && e.CommandInfo.Operation != RequestOperation.Query
                select e).ToList();

            if (!queries.Any())
            {
                throw new Exception("没有找到需要的操作", new Exception("RequestContents没有包含执行(add,modify或者delete)操作"));
            }

            var resultEntity = Save(employees);

            return resultEntity;
        }


        internal List<ItemContent> Query(List<ItemContent> itemContents)
        {
            var resultEntity = new List<ItemContent>();


                List<employee> empList;
                List<emp_role> empRoleList;
                List<role> roleList;



                //没有指定，则返回所有查询结果
                if (itemContents == null)
                {
                    empList = (from e in Entities.employee select e).ToList();
                    empRoleList = (from e in Entities.emp_role select e).ToList();
                    roleList = (from e in Entities.role select e).ToList();
                }
                else //有指定，则从传送的数据处进行查询
                {
                    var employees = itemContents.Cast<Employee>().ToList();

                    var empIDList = (from e in employees
                        select e.ID).ToList();

                    var empNameList = (from e in employees
                        select e.Name).ToList();


                    empList =
                        (from e in Entities.employee
                            where empIDList.Contains(e.ID) || empNameList.Contains(e.Name)
                            select e).ToList();
                    empRoleList = (from e in Entities.emp_role where empIDList.Contains(e.emp_id) select e).ToList();
                    roleList = (from e in Entities.role select e).ToList();
                }



                //人员基本信息
                foreach (var employee in empList)
                {
                    var currentEmp = employee;

                    var resultEmployee = new Employee
                    {
                        CommandInfo = new CommandInformation
                        {
                            State = ResultState.Success,
                            Exception = string.Empty,
                            InnerErrorMessage = string.Empty
                        },
                        ID = currentEmp.ID,
                        Description = currentEmp.Description,
                        LoginName = currentEmp.LoginName,
                        Name = currentEmp.Name,
                        Password = currentEmp.Password,
                        EmpAuthority = currentEmp.Authority.GetValueOrDefault()
                    };

                    var roleIDList = (from er in empRoleList
                        where er.emp_id == currentEmp.ID
                        select er.role_id).ToList();

                    resultEmployee.Roles = (from r in roleList
                        where roleIDList.Contains(r.id)
                        select new Role
                        {
                            Description = r.Description,
                            ID = r.id,
                            Name = r.Name,
                            Authority = r.Authority.GetValueOrDefault()
                        }).ToList();


                    resultEntity.Add(resultEmployee);
                }


            return resultEntity;

        }


        internal List<ItemContent> Save(List<ItemContent> requestEntitiies)
        {
            var resultEntity = new List<ItemContent>();


                foreach (var content in requestEntitiies)
                {
                    var resultcontent = new ItemContent();


                    if (content == null)
                    {
                        resultcontent.CommandInfo.Exception = "未设置操作类型";
                        resultcontent.CommandInfo.InnerErrorMessage = "ResultContent为NULL";
                        resultcontent.CommandInfo.State = ResultState.Fail;

                    }
                    else
                    {

                        switch (content.CommandInfo.Operation)
                        {
                            case RequestOperation.Add:
                            {
                                resultcontent = AddEmployee(Entities, content);
                            }
                                break;
                            case RequestOperation.Modify:
                            {
                                resultcontent = ModifyEmployee(Entities, content);
                            }
                                break;
                            case RequestOperation.Delete:
                            {
                                resultcontent = DeleteEmployee(Entities, content);
                            }
                                break;
                            default:
                            {
                                resultcontent.CommandInfo.Exception = "错误的操作类型";
                                resultcontent.CommandInfo.InnerErrorMessage = "RequestOperation=" +
                                                                             Enum.GetName(typeof (RequestOperation),
                                                                                 content.CommandInfo.Operation);
                                resultcontent.CommandInfo.State = ResultState.Fail;
                            }
                                break;
                        }
                    }
                    resultEntity.Add(resultcontent);


            }
            return resultEntity;
        }


        private static ItemContent DeleteEmployee(UmsEntities Entities, ItemContent employeeContent)
        {
            var employee = employeeContent as Employee;

            if (employee == null)
            {
                return new Employee
                {
                    CommandInfo = new CommandInformation
                    {
                        Exception = "错误的数据类型，没有进行添加操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    }

                };
            }



            var employeeInDb = (from e in Entities.employee where e.ID == employee.ID select e).FirstOrDefault();

            if (employeeInDb == null)
            {
                employee.CommandInfo = new CommandInformation
                {
                    Exception = "ID为[" + employee.ID + "]的人员不存在，没有进行删除操作",
                    InnerErrorMessage = string.Empty,
                    State = ResultState.Success
                };
            }


            Entities.employee.Remove(employeeInDb);



            //删除已存在的
            var roleListExist = (from e in Entities.emp_role where e.emp_id == employee.ID select e).ToList();

            if (roleListExist.Any())
                Entities.emp_role.RemoveRange(roleListExist);



            Entities.SaveChanges();


            employee.CommandInfo = new CommandInformation
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                State = ResultState.Success
            };

            return employee;
        }

        private static ItemContent ModifyEmployee(UmsEntities Entities, ItemContent employeeContent)
        {

            var employee = employeeContent as Employee;


            if (employee == null)
            {
                return new Employee
                {
                    CommandInfo = new CommandInformation
                    {
                        Exception = "错误的数据类型，没有进行添加操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    }

                };
            }





            var employeeInDb = (from e in Entities.employee where e.ID == employee.ID select e).FirstOrDefault();

            if (employeeInDb == null)
            {
                employee.CommandInfo = new CommandInformation
                {
                    Exception = "ID为[" + employee.ID + "]的人员不存在，没有进行修改操作",
                    InnerErrorMessage = string.Empty,
                    State = ResultState.Success
                };

                return employee;
            }


            employeeInDb.Description = employee.Description;
            employeeInDb.LoginName = employee.LoginName;
            employeeInDb.Name = employee.Name;
            employeeInDb.Password = employee.Password;
            employeeInDb.Authority = employee.EmpAuthority;



            //删除已存在的
            var roleListExist = (from e in Entities.emp_role where e.emp_id == employee.ID select e).ToList();


            if (roleListExist.Any())
            Entities.emp_role.RemoveRange(roleListExist);


            //权限关联关系采用先删除再添加的方式处理
            foreach (var role in employee.Roles)
            {

                //添加新的
                Entities.emp_role.Add(new emp_role
                {
                    emp_id = employee.ID,
                    role_id = role.ID
                });
            }


            Entities.SaveChanges();


            employee.CommandInfo = new CommandInformation
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                State = ResultState.Success
            };

            return employee;

        }

        private static ItemContent AddEmployee(UmsEntities Entities, ItemContent employeeContent)
        {
            var employee = employeeContent as Employee;



            if (employee == null)
            {
                return new Employee
                {
                    CommandInfo = new CommandInformation
                    {
                        Exception = "错误的数据类型，没有进行添加操作",
                        InnerErrorMessage = string.Empty,
                        State = ResultState.Success
                    }

                };
            }




            var empToAdd = new employee
            {
                Description = employee.Description,
                LoginName = employee.LoginName,
                Name = employee.Name,
                Password = employee.Password,
                Authority = employee.EmpAuthority
            };

            Entities.employee.Add(empToAdd);

            if (employee.Roles!=null && employee.Roles.Count > 0)
            {
                foreach (var role in employee.Roles)
                {
                    Entities.emp_role.Add(new emp_role
                    {
                        emp_id = employee.ID,
                        role_id = role.ID,
                    });
                }
            }


            Entities.SaveChanges();

            employee.ID = empToAdd.ID;

            employee.CommandInfo = new CommandInformation
            {
                Exception = string.Empty,
                InnerErrorMessage = string.Empty,
                State = ResultState.Success
            };

            return employee;
        }
    }
}
