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
    public class DonationServer
    {
        public static string conStr = AccommodateServer.conStr;
        public static bool DeleteDonations(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"delete donation where Donor_ID= {UID}";
                command.Parameters.Clear();
                try
                {
                    command.ExecuteNonQuery();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 注意SQL中EXTRACT的用法，这是给管理员看的，个人点赞是GetLikePetEntry...
        /// </summary>
        /// <param name="Limitrows"></param>
        /// <param name="Orderby"></param>
        /// <returns></returns>
        public static DataTable DonationInfo(decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT user_name,donation_amount,TO_CHAR(donation_time,'YYYY-MM-DD') as donated_date, TO_CHAR(donation_time,'HH24:MI:SS') as donated_time FROM donation" +
                " left join user2 on donor_id=user_id";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        public static DataTable DonateIDInfo(string UID,decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            string query = $"SELECT * FROM donation where donor_id={UID}";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        public static DataTable TotalAmount(string UID)
        {
            DataTable dataTable = new DataTable();
            string query = $"SELECT sum(donation_amount) FROM donation where donor_id={UID}";
            return DBHelper.ShowInfo(query);
        }
        public static DataTable DonationAmount(decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();

                string query = "SELECT donor_id, SUM(donation_amount) AS total_donation, MAX(donation_amount) AS max_donation, COUNT(*) AS donation_count FROM donation";
                if (Limitrows > 0)
                    query += $" WHERE rownum <= {Limitrows} ";
                query += " GROUP BY donor_id";
                if (!string.IsNullOrEmpty(Orderby))
                    query += $" ORDER BY {Orderby} DESC";

                OracleCommand command = new OracleCommand(query, connection);

                OracleDataAdapter adapter = new OracleDataAdapter(command);

                adapter.Fill(dataTable);

                connection.Close();
            }

            //Console.ReadLine();
            return dataTable;
        }


        /// <summary>
        /// 获取点赞宠物条目
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        /// <returns>true表示有条目，则可以删除，false表示可以点赞</returns>
        public static bool GetLikePetEntry(string UID, string PID)
        {
            bool con = false;
            User user1 = new User();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"select *from like_pet where Pet_ID={PID} and User_ID={UID}";
                command.Parameters.Clear();
                try
                {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        connection.Close();
                        return true;
                        // 执行你的逻辑操作，例如将数据存储到自定义对象中或进行其他处理
                    }
                    connection.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        /// <summary>
        /// 用户想要捐赠,捐赠ID空出来，没必要写
        /// </summary>
        /// <param name="DID"></param>
        /// <param name="Amount"></param>
        /// <param name="censor_state"></param>
        public static void TryDonote(string DID, decimal Amount)
        {
            // 添加新行
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"INSERT INTO donation (donor_id, donation_amount) " +
                        $"VALUES ({DID},{Amount})";
                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"{DID}想捐{Amount}元");

                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("不存在的用户或数额非法");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 只有存在条目才能删除
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public static void DeleteLikePet(string UID, string PID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from like_pet where Pet_ID= :Pet_ID and User_ID=:User_ID";
                command.Parameters.Clear();
                command.Parameters.Add("Pet_ID", OracleDbType.Varchar2, PID, ParameterDirection.Input);
                command.Parameters.Add("User_ID", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"{UID}给{PID}的点赞已取消");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"不存在{UID}给{PID}的点赞");
                }
            }
        }
    }
}
