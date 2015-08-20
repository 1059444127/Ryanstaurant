using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DataBase
{

    public enum DBType
    {
        MSSQL,
        MYSQL,
        ORACLE
    }



    public class DataBaseFactory
    {

        public static IDataBase CreateDataBase(DBType dbType)
        {
            return Assembly.GetExecutingAssembly().CreateInstance(  "DataBase." + dbType.ToString() + ".DataBase")	as IDataBase;
        }





    }
}
