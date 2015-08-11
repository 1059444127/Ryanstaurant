using System;
using System.Data;
using System.Data.SqlClient;

namespace DataBase.MSSQL
{
    public class DataBase:IDataBase
    {
        private SqlConnection conn;
        private SqlCommand comm;
        private SqlDataAdapter _sda;


        private string _errorMessage = string.Empty;


        private int _errorCode;

        public int ErrorCode
        {
            get { return _errorCode; }
            internal set
            {
                _errorCode = value;
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            internal set { _errorMessage = value; }
        }


        public DataBase()
        {
            var connBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "Ryanstaurant",
                UserID = "sa",
                Password = "sa"
            };

            conn = new SqlConnection(connBuilder.ConnectionString);


        }




        public DataTable ReadDataTableBySql(string strSql)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();


                var dsReturn = new DataSet();
                _sda = new SqlDataAdapter(strSql, conn);
                _sda.Fill(dsReturn);
                return dsReturn.Tables.Count == 0 ? null : dsReturn.Tables[0];
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                _errorCode = -1;
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
