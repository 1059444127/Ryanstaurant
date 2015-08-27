using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DAL;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllEmpAuth
    {
        public List<DataContract.EmpAuth> Get(List<DataContract.EmpAuth> empAuths)
        {
            return LoadDalMethod(empAuths, list => new DalEmpAuth().Get(list));
        }


        public List<DataContract.EmpAuth> Add(List<DataContract.EmpAuth> empAuths)
        {
            return LoadDalMethod(empAuths, list => new DalEmpAuth().Add(list));
        }

        public List<DataContract.EmpAuth> Modify(List<DataContract.EmpAuth> empAuths)
        {
            LoadDalMethod(empAuths, list => new DalEmpAuth().Delete(list));
            return LoadDalMethod(empAuths, list => new DalEmpAuth().Add(list));
        }

        public List<DataContract.EmpAuth> Delete(List<DataContract.EmpAuth> empAuths)
        {
            return LoadDalMethod(empAuths, list => new DalEmpAuth().Delete(list));
        }


        protected List<DataContract.EmpAuth> LoadDalMethod(List<DataContract.EmpAuth> empAuths,
            Func<List<Entity.EmpAuth>, List<Entity.EmpAuth>> methodHandler)
        {
            var entityList = (from e in empAuths select e.ConvertToDataContract<Entity.EmpAuth>()).ToList();
            var resultEntities = methodHandler(entityList);
            return (from e in resultEntities select e.ConvertToDataContract<DataContract.EmpAuth>()).ToList();
        }
    }
}
