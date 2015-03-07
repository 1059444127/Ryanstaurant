using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase;
using BusinessModels;
using System.Data;

namespace WorkSpace
{
    public class BusinessLogic
    {

        IDataBase db = DataBaseFactory.CreateDataBase(DBType.MYSQL);

        public Employee GetEmployee(string EmployeeName)
        {
            Employee emp = new Employee();

            string strSQL = "select id,name,password from employee where name = '" + EmployeeName.Replace("'", "") + "'";
            DataTable dt = db.ReadDataTableBySQL(strSQL);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            emp.ID = dt.Rows[0]["id"].ToString();
            emp.Name = dt.Rows[0]["Name"].ToString();
            emp.Password = dt.Rows[0]["Password"].ToString();

            return emp;


        }





        public DataTable GetFloorLayout()
        {
            string strSQL = "Select * from v_TableSeats";
            DataTable dt = db.ReadDataTableBySQL(strSQL);
            return dt;

        }





        public DataTable GetNavigations()
        {
            string strSQL = "select * from Navigator";
            DataTable dt = db.ReadDataTableBySQL(strSQL);
            return dt;
        }

    }
}
