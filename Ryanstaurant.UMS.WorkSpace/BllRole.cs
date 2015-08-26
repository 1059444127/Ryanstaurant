using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DAL;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.WorkSpace
{
    class BllRole
    {
        public List<DataContract.Role> Get(List<DataContract.Role> roles)
        {
            return LoadDalMethod(roles, list => new DalRole().Get(list));
        }


        public List<DataContract.Role> AddEmployees(List<DataContract.Role> roles)
        {
            return LoadDalMethod(roles, list => new DalRole().Add(list));
        }

        public List<DataContract.Role> ModifyEmployees(List<DataContract.Role> roles)
        {
            return LoadDalMethod(roles, list => new DalRole().Modify(list));
        }

        public List<DataContract.Role> DeleteEmployees(List<DataContract.Role> roles)
        {
            return LoadDalMethod(roles, list => new DalRole().Delete(list));
        }


        protected List<DataContract.Role> LoadDalMethod(List<DataContract.Role> roles,
            Func<List<Entity.Role>, List<Entity.Role>> methodHandler)
        {
            var entityList = (from e in roles select e.ConvertToDataContract<Entity.Role>()).ToList();
            var resultEntities = methodHandler(entityList);
            return (from e in resultEntities select e.ConvertToDataContract<DataContract.Role>()).ToList();
        }
    }
}
