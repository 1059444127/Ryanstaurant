using System;
using System.Collections.Generic;
using System.Text;
using Ryanstaurant.UMS.Entity;
using Ryanstaurant.UMS.Interface;
using Ryanstaurant.UMS.WorkSpace;

namespace Ryanstaurant.UMS.Service
{
    public class UMSService:IUMSService
    {

        public Employee GetEmployee(string employeeName)
        {
            var employee = new Employee { Name = employeeName };
            return new BllEmployee().Get(employee);
        }
    }
}
