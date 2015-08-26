using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DAL;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllAuthority
    {

        public List<DataContract.Authority> Get(List<DataContract.Authority> authorities)
        {
            return LoadDalMethod(authorities, list => new DalAuthority().Get(list));
        }


        public List<DataContract.Authority> AddEmployees(List<DataContract.Authority> authorities)
        {
            return LoadDalMethod(authorities, list => new DalAuthority().Add(list));
        }

        public List<DataContract.Authority> ModifyEmployees(List<DataContract.Authority> authorities)
        {
            return LoadDalMethod(authorities, list => new DalAuthority().Modify(list));
        }

        public List<DataContract.Authority> DeleteEmployees(List<DataContract.Authority> authorities)
        {
            return LoadDalMethod(authorities, list => new DalAuthority().Delete(list));
        }








        protected List<DataContract.Authority> LoadDalMethod(List<DataContract.Authority> authorities,
            Func<List<Entity.Authority>, List<Entity.Authority>> methodHandler)
        {
            var entityList = (from e in authorities select e.ConvertToDataContract<Entity.Authority>()).ToList();
            var resultEntities = methodHandler(entityList);
            return (from e in resultEntities select e.ConvertToDataContract<DataContract.Authority>()).ToList();
        }


    }
}
