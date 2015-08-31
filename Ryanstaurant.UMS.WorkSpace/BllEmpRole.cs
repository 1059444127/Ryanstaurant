using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.UMS.DAL;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllEmpRole
    {

        public List<DataContract.EmpRole> Get(List<DataContract.EmpRole> empRoles)
        {
            return LoadDalMethod(empRoles ?? new List<DataContract.EmpRole> {new EmpRole {EmpID = -1, RoleID = -1}},
                list => new DalEmpRole().Get(list));
        }


        public List<DataContract.EmpRole> Add(List<DataContract.EmpRole> empRoles)
        {
            return LoadDalMethod(empRoles, list => new DalEmpRole().Add(list));
        }

        public List<DataContract.EmpRole> Modify(List<DataContract.EmpRole> empRoles)
        {
            LoadDalMethod(empRoles, list => new DalEmpRole().Delete(list));
            return LoadDalMethod(empRoles, list => new DalEmpRole().Add(list));
        }

        public List<DataContract.EmpRole> Delete(List<DataContract.EmpRole> empRoles)
        {
            return LoadDalMethod(empRoles, list => new DalEmpRole().Delete(list));
        }


        protected List<DataContract.EmpRole> LoadDalMethod(List<DataContract.EmpRole> empRoles,
            Func<List<Entity.EmpRole>, List<Entity.EmpRole>> methodHandler)
        {
            var entityList = (from e in empRoles select e.ConvertToDataContract<Entity.EmpRole>()).ToList();
            var resultEntities = methodHandler(entityList);
            return (from e in resultEntities select e.ConvertToDataContract<DataContract.EmpRole>()).ToList();
        }
    }
}
