using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace PetAdopt.DAL
{
    public class DBHelper
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        static string conStr = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";";
        public static object GetScalar(string sql, OracleParameter[] pa)
        {
            using (OracleConnection conn = new OracleConnection(conStr))
            {
                object ob = null;
                try
                {
                    conn.Open();
                    OracleCommand com = new OracleCommand(sql, conn);
                    com.Parameters.AddRange(pa);
                    ob = com.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return ob;
            }
        }
        public static OracleDataReader GetDataReader(string sql, OracleParameter[] pa)
        {
            OracleConnection conn = new OracleConnection(conStr);
            conn.Open();
            OracleCommand com = new OracleCommand(sql, conn);
            if (pa != null) com.Parameters.AddRange(pa);
            OracleDataReader reader = com.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        public static int RunExecuteNonQuery(string sql)
        {
            using (OracleConnection conn = new OracleConnection(conStr))
            {
                int row = 0;
                try
                {
                    conn.Open();
                    OracleCommand com = new OracleCommand(sql, conn);
                    row = com.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return row;
            }
        }
        public static int GetProcedure(string sql, OracleParameter[] para)
        {
            using (OracleConnection conn = new OracleConnection(conStr))
            {
                int row;
                try
                {
                    conn.Open();
                    OracleCommand com = new OracleCommand(sql, conn);
                    com.CommandText = sql;
                    com.Connection = conn;
                    com.CommandType = CommandType.StoredProcedure;
                    if (para != null)
                        com.Parameters.AddRange(para);
                    row = com.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return row;
            }
        }

    }
}
