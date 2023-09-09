using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using PetFoster.Model;
using static PetFoster.Model.PetData;
using System.IO;

namespace PetFoster.DAL
{
    public class DBHelper
    {
        public static string conStr = AccommodateServer.conStr;
        public static void ExecuteNonScalar(string text)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = text;
                try
                {
                    command.ExecuteNonQuery();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public static DataTable ShowInfo(string query, decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            if (Limitrows > 0)
                query += $" where rownum<={Limitrows} ";
            if ((Orderby) != null)
                query += $" order by {Orderby} desc";
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();
                OracleCommand command = new OracleCommand(query, connection);

                OracleDataAdapter adapter = new OracleDataAdapter(command);

                adapter.Fill(dataTable);

                connection.Close();
            }
            return dataTable;
        }
        public static int GetScalarInt(string query)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                try
                {
                    Object? oval = command.ExecuteScalar();
                    string val;
                    if (oval == null)
                        val = "-1";
                    else
                        val = oval.ToString();
                    int petname = Convert.ToInt32(val);
                    connection.Close();
                    return petname;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                    return -1;
                }
            }
        }
        public static string GetScalar(string query)
        {
            bool con = false;
            User user1 = new User();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.Parameters.Clear();
                try
                {
                    string petname = command.ExecuteScalar() as string;
                    return petname;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "Error";
                }
            }
        }
    }
}