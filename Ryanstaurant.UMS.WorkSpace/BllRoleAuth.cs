using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DAL;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllRoleAuth
    {
        public List<DataContract.RoleAuth> Get(List<DataContract.RoleAuth> roleAuths)
        {
            return LoadDalMethod(roleAuths ?? new List<DataContract.RoleAuth>(), list => new DalRoleAuth().Get(list));
        }


        public List<DataContract.RoleAuth> Add(List<DataContract.RoleAuth> roleAuths)
        {
            return LoadDalMethod(roleAuths, list => new DalRoleAuth().Add(list));
        }

        public List<DataContract.RoleAuth> Modify(List<DataContract.RoleAuth> roleAuths)
        {
            LoadDalMethod(roleAuths, list => new DalRoleAuth().Delete(list));
            return LoadDalMethod(roleAuths, list => new DalRoleAuth().Add(list));
        }

        public List<DataContract.RoleAuth> Delete(List<DataContract.RoleAuth> roleAuths)
        {
            return LoadDalMethod(roleAuths, list => new DalRoleAuth().Delete(list));
        }


        protected List<DataContract.RoleAuth> LoadDalMethod(List<DataContract.RoleAuth> roleAuths,
            Func<List<Entity.RoleAuth>, List<Entity.RoleAuth>> methodHandler)
        {
            var entityList = (from e in roleAuths select e.ConvertToDataContract<Entity.RoleAuth>()).ToList();
            var resultEntities = methodHandler(entityList);
            return (from e in resultEntities select e.ConvertToDataContract<DataContract.RoleAuth>()).ToList();
        }
    }
}
