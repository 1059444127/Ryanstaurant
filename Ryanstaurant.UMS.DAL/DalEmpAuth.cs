using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DataAccess.EF;
using Ryanstaurant.UMS.Entity;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.DAL
{
    public class DalEmpAuth
    {
        public List<EmpAuth> Get(List<EmpAuth> empAuth)
        {
            using (var entities = new ryanstaurantEntities())
            {
                var listEmpId = (from a in empAuth select a.EmpID).ToList();
                var listAuthId = (from a in empAuth select a.AuthID).ToList();



                List<emp_auth> listEmpAuths;
                if (empAuth.Count == 0)
                {
                    listEmpAuths =
                    (from e in entities.emp_auth
                     select e).ToList();
                }
                else
                {
                    listEmpAuths =
                    (from e in entities.emp_auth
                        where  listEmpId.Contains(e.Emp_id) && listAuthId.Contains(e.Auth_id)
                        select e).ToList();
                }


                foreach (var eA in empAuth)
                {
                    var matchedEmpAuth =
                        (from a in listEmpAuths where a.Emp_id == eA.EmpID && a.Auth_id==eA.AuthID select a).FirstOrDefault();


                    if (matchedEmpAuth == null)
                    {
                        eA.Exception = "不存在Emp_id为[" + eA.EmpID + "],Auth_id为["+eA.AuthID+"]的对应关系";
                        eA.ExpType = ExceptionType.NotExist;
                        continue;
                    }


                    eA.Exception = string.Empty;
                    eA.ExpType = ExceptionType.None;
                    eA.EmpID = matchedEmpAuth.Emp_id;
                    eA.AuthID = matchedEmpAuth.Auth_id;

                }
            }
            return empAuth;
        }



        public List<EmpAuth> Add(List<EmpAuth> empAuths)
        {
            var listEmpAuths = Get(empAuths);

            using (var entities = new ryanstaurantEntities())
            {

                foreach (var empAuth in listEmpAuths)
                {
                    if (empAuth.ExpType != ExceptionType.NotExist)
                    {
                        empAuth.Exception = "Emp_id为[" + empAuth.EmpID + "],Auth_id为[" + empAuth.AuthID + "]的对应关系已经存在";
                        empAuth.ExpType = ExceptionType.AlreadyExist;
                        continue;
                    }

                    try
                    {

                        entities.emp_auth.Add(new emp_auth
                        {
                            Emp_id = empAuth.EmpID,
                            Auth_id = empAuth.AuthID
                        });
                        empAuth.Exception = string.Empty;
                        empAuth.ExpType = ExceptionType.None;
                    }
                    catch (Exception ex)
                    {
                        empAuth.Exception = ex.Message;
                        empAuth.ExceptionStackTrace = ex.StackTrace;
                        empAuth.ExpType = ExceptionType.Failed;
                    }
                }

                var state = entities.SaveChanges();
                if (state <= 0)
                {
                    throw new Exception("添加用户权限失败");
                }
            }
            return empAuths;
        }


        public List<EmpAuth> Delete(List<EmpAuth> empAuths)
        {
            var listEmpAuths = Get(empAuths);

            using (var entities = new ryanstaurantEntities())
            {
                foreach (var empAuth in listEmpAuths)
                {
                    if (empAuth.ExpType != ExceptionType.None)
                    {
                        empAuth.Exception = "Emp_id为[" + empAuth.EmpID + "],Auth_id为[" + empAuth.AuthID + "]的对应关系不存在";
                        empAuth.ExpType = ExceptionType.NotExist;
                        continue;
                    }

                    var dbEmpAuth = entities.emp_auth.First(a => a.Emp_id == empAuth.EmpID && a.Auth_id == empAuth.AuthID);
                    entities.emp_auth.Remove(dbEmpAuth);

                    empAuth.Exception = string.Empty;
                    empAuth.ExceptionStackTrace = string.Empty;
                    empAuth.ExpType = ExceptionType.None;
                }

                var state = entities.SaveChanges();
                if (state <= 0)
                {
                    throw new Exception("删除用户权限失败");
                }
            }
            return empAuths;
        }




    }
}
