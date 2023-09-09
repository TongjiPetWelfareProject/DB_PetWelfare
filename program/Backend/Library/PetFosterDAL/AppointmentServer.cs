using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.DAL
{
    public class AppointmentServer
    {
        public static string conStr = AccommodateServer.conStr;
        /// <summary>
        /// 查看申请信息，由ShowProfiles(DataTable dt)调用
        /// </summary>
        /// <param name="Limitrows">最多显示的行数</param>
        /// <param name="Orderby">排序的依据（降序）</param>
        /// <returns>返回数据表</returns>
        public static DataTable ApplyInfo(string UID=null,string PID=null,string Categories=null)
        {
            DataTable dataTable = new DataTable();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();

                string query = "SELECT category,pet_name,breed,age,user_name,reason,apply_time,phone_number FROM application join user2 on user2.user_id=application.user_id join pet on application.pet_id=pet.pet_id";
                if (UID!=null&&PID==null)
                    query += $" where User_ID={UID} ";
                else if (PID != null&&UID==null)
                    query += $" where Pet_ID={PID}";
                if(UID!=null&&PID!=null)
                    query += $" where User_ID={UID} and Pet_ID={PID}";
                if (Categories != null)
                    query += $" where category='{Categories}'";
                OracleCommand command = new OracleCommand(query, connection);

                OracleDataAdapter adapter = new OracleDataAdapter(command);

                adapter.Fill(dataTable);

                connection.Close();


            }

            Console.ReadLine();
            return dataTable;
        }
        /// <summary>
        /// 获取某人对某宠物的申请种类
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public static List<string> GetCategories(string UID,string PID)
        {
            List<string> categories = new List<string>();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from application where User_ID=:user_id and Pet_ID:=Pet_ID";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                command.Parameters.Add("pet_id", OracleDbType.Varchar2, PID, ParameterDirection.Input);
                try
                {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        categories.Add(reader.GetString(2));    
                       
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                connection.Close();
            }

            return categories;
        }
    }
}
