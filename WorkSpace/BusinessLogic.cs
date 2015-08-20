using DataBase;
using BusinessModels;
using System.Data;

namespace WorkSpace
{
    public class BusinessLogic
    {
        readonly IDataBase _db = DataBaseFactory.CreateDataBase(DBType.MYSQL);

        public Employee GetEmployee(string employeeName)
        {
            var emp = new Employee();

            var strSQL = "select id,name,password from employee where name = '" + employeeName.Replace("'", "") + "'";
            var dt = _db.ReadDataTableBySql(strSQL);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            emp.ID = dt.Rows[0]["id"].ToString();
            emp.Name = dt.Rows[0]["Name"].ToString();
            emp.Password = dt.Rows[0]["Password"].ToString();

            return emp;


        }





        public DataTable GetFloorLayout()
        {
            const string strSQL = "Select * from v_TableSeats";
            DataTable dt = _db.ReadDataTableBySql(strSQL);
            return dt;

        }





        public DataTable GetNavigations()
        {
            const string strSQL = "select * from Navigator";
            DataTable dt = _db.ReadDataTableBySql(strSQL);
            return dt;
        }

    }
}
