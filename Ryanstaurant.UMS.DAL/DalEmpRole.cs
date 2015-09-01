using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.Entity;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.DAL
{
    public class DalEmpRole
    {
        public List<EmpRole> Get(List<EmpRole> empRole)
        {
            var returnEmpRoles = new List<EmpRole>();
            using (var entities = new ryanstaurantEntities())
            {
                var listEmpId = (from a in empRole where a.EmpID != -1 select a.EmpID).ToList();
                var listRoleId = (from a in empRole where a.RoleID != -1 select a.RoleID).ToList();


                List<emp_role> listEmpRoles;

                if (empRole.Count == 0 || (listRoleId.Count == 0 && listEmpId.Count == 0))
                {
                    listEmpRoles =
                        (from e in entities.emp_role
                            select e).ToList();
                }
                else
                {
                    listEmpRoles =
                        (from e in entities.emp_role
                            where listEmpId.Contains(e.emp_id) || listRoleId.Contains(e.role_id)
                            select e).ToList();
                }


                foreach (var eA in empRole)
                {
                    List<emp_role> matchedEmpRoles;
                    var exception = string.Empty;
                    if (eA.RoleID == -1 && eA.EmpID == -1)
                    {
                        matchedEmpRoles = listEmpRoles;
                    }
                    else if (eA.EmpID == -1)
                    {
                        matchedEmpRoles =
                            (from a in listEmpRoles
                                where a.role_id == eA.RoleID
                                select a).ToList();

                        if (matchedEmpRoles.Count == 0)
                            exception = "Role_id为[" + eA.RoleID + "]的对应关系不存在";

                    }
                    else if (eA.RoleID == -1)
                    {
                        matchedEmpRoles =
                            (from a in listEmpRoles
                                where a.emp_id == eA.EmpID
                                select a).ToList();

                        if (matchedEmpRoles.Count == 0)
                            exception = "Emp_id为[" + eA.EmpID + "]的对应关系不存在";

                    }
                    else
                    {
                        matchedEmpRoles =
                            (from a in listEmpRoles
                                where a.emp_id == eA.EmpID && a.role_id == eA.RoleID
                                select a).ToList();

                        if (matchedEmpRoles.Count == 0)
                            exception = "Emp_id为[" + eA.EmpID + "],Role_id为[" + eA.RoleID + "]的对应关系不存在";

                    }

                    if (!string.IsNullOrEmpty(exception))
                    {
                        returnEmpRoles.Add(new EmpRole
                        {
                            EmpID = eA.EmpID,
                            Exception = exception,
                            ExceptionStackTrace = string.Empty,
                            ExpType = ExceptionType.NotExist,
                            RoleID = eA.RoleID
                        });
                        continue;
                    }

                    returnEmpRoles.AddRange(matchedEmpRoles.Select(matchedEmpRole => new EmpRole
                    {
                        EmpID = matchedEmpRole.emp_id,
                        Exception = string.Empty,
                        ExceptionStackTrace = string.Empty,
                        ExpType = ExceptionType.None,
                        RoleID = matchedEmpRole.role_id
                    }));
                }
            }
            return returnEmpRoles;
        }


        public List<EmpRole> Add(List<EmpRole> empRoles)
        {
            var listEmpRoles = Get(empRoles);

            using (var entities = new ryanstaurantEntities())
            {

                foreach (var empRole in listEmpRoles)
                {
                    if (empRole.ExpType != ExceptionType.NotExist)
                    {
                        empRole.Exception = "Emp_id为[" + empRole.EmpID + "],Role_id为[" + empRole.RoleID + "]的对应关系已经存在";
                        empRole.ExpType = ExceptionType.AlreadyExist;
                        continue;
                    }

                    try
                    {

                        entities.emp_role.Add(new emp_role
                        {
                            emp_id = empRole.EmpID,
                            role_id = empRole.RoleID
                        });
                        empRole.Exception = string.Empty;
                        empRole.ExpType = ExceptionType.None;
                    }
                    catch (Exception ex)
                    {
                        empRole.Exception = ex.Message;
                        empRole.ExceptionStackTrace = ex.StackTrace;
                        empRole.ExpType = ExceptionType.Failed;
                    }
                }

                var state = entities.SaveChanges();
                if (state <= 0)
                {
                    throw new Exception("添加用户角色失败");
                }
            }
            return empRoles;
        }


        public List<EmpRole> Delete(List<EmpRole> empRoles)
        {
            var listEmpRoles = Get(empRoles);

            using (var entities = new ryanstaurantEntities())
            {
                foreach (var empRole in listEmpRoles)
                {
                    if (empRole.ExpType != ExceptionType.None)
                    {
                        empRole.Exception = "Emp_id为[" + empRole.EmpID + "],Role_id为[" + empRole.RoleID + "]的对应关系不存在";
                        empRole.ExpType = ExceptionType.NotExist;
                        continue;
                    }

                    var dbEmpRole = entities.emp_role.First(a => a.emp_id == empRole.EmpID && a.role_id == empRole.RoleID);
                    entities.emp_role.Remove(dbEmpRole);

                    empRole.Exception = string.Empty;
                    empRole.ExceptionStackTrace = string.Empty;
                    empRole.ExpType = ExceptionType.None;
                }

                var state = entities.SaveChanges();
                if (state <= 0)
                {
                    throw new Exception("删除用户权限失败");
                }
            }
            return empRoles;
        }


    }
}
