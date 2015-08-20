using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataBase.MYSQL
{
    public class DataBase :IDataBase
    {
        MySqlConnection conn = null;
        MySqlCommand comm = null;

        private string _ErrorMessage = "";

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            internal set { _ErrorMessage = value; }
        }


        private int _ErrorCode = 0;

        public int ErrorCode
        {
            get { return _ErrorCode; }
            internal set
            {
                _ErrorCode = value; 
            }
        }





        public DataBase()
        {
            MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder();
            connBuilder.Server = "127.0.0.1";
            connBuilder.Database = "Ryanstaurant";
            connBuilder.UserID = "root";
            connBuilder.Password = "";

            conn = new MySqlConnection(connBuilder.ConnectionString);
            

        }

        


        public DataTable ReadDataTableBySQL(string strSql)
        {

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();


                DataSet dsReturn = new DataSet();
                dsReturn = MySqlHelper.ExecuteDataset(conn, strSql);
                if (dsReturn.Tables.Count == 0)
                    return null;
                return dsReturn.Tables[0];

            }
            catch(Exception ex)
            {
                this._ErrorMessage = ex.Message;
                this._ErrorCode = -1;
                return null;
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }



        }










    }
}
