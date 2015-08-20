using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace DataBase
{
    public class DBFactory
    {


        public enum DataBaseType
        {
            SQL,
            ORACLE,
            MYSQL
        }



        public static IDataBase CreateDataBase(DataBaseType dbtype,string strServer,string strDataBase,string strUserID,string strPassword)
        {
            switch (dbtype)
            {
                case DataBaseType.MYSQL:
                    {
                        IDataBase db = new MYSQL.DataBase();

                    } break;
            }
        }



        public static IDataBase CreateDataBase(DataBaseType dbtype,string strConnectionString)
        {

        }


    }
}
