using System;
using System.Collections.Generic;
using System.Linq;
using Ryanstaurant.UMS.DAL;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.Interface;

namespace Ryanstaurant.UMS.WorkSpace
{
    public class BllEmployee
    {
        public List<DataContract.Employee> Get(List<DataContract.Employee> employees)
        {
            return LoadDalMethod(employees ?? new List<Employee>() ,
                list => (list == null || list.Count == 0) ? new DalEmployee().GetAll() : new DalEmployee().Get(list));
        }


        public List<DataContract.Employee> Add(List<DataContract.Employee> employees)
        {
            return LoadDalMethod(employees, list => new DalEmployee().Add(list));
        }

        public List<DataContract.Employee> Modify(List<DataContract.Employee> employees)
        {
            return LoadDalMethod(employees, list => new DalEmployee().Modify(list));
        }

        public List<DataContract.Employee> Delete(List<DataContract.Employee> employees)
        {
            return LoadDalMethod(employees, list => new DalEmployee().Delete(list));
        }



        protected List<DataContract.Employee> LoadDalMethod(List<DataContract.Employee> employees,
            Func<List<Entity.Employee>, List<Entity.Employee>> methodHandler)
        {
            var entityList = (from e in employees select e.ConvertToDataContract<Entity.Employee>()).ToList();
            var resultEntities = methodHandler(entityList);
            return (from e in resultEntities select e.ConvertToDataContract<DataContract.Employee>()).ToList();
        }





    }
}
