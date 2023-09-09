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
    public class LikePetServer
    {
        public static string conStr = AccommodateServer.conStr;
        /// <summary>
        /// 注意SQL中EXTRACT的用法，这是给管理员看的，个人点赞是GetLikePetEntry...
        /// </summary>
        /// <param name="Limitrows"></param>
        /// <param name="Orderby"></param>
        /// <returns></returns>
        public static DataTable LikePetInfo(decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT user_id,pet_id,EXTRACT date from like_time as liked_date, EXTRACT time from like_time as liked_time FROM like_pet";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
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
        /// 点赞
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        public static void InsertLikePet(string UID,string PID)
        {
            // 添加新行
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO like_pet (user_id, pet_id) " +
                        "VALUES (:user_id,:pet_id)";
                    command.Parameters.Clear();
                    command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                    command.Parameters.Add("pet_id", OracleDbType.Varchar2, PID, ParameterDirection.Input);
                   
                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"{UID}给{PID}在{DateTime.Now}点赞");
                        
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("不存在的用户或宠物");
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
        public static int _GetLikeNums(string PID)
        {
            string query = $"select count(*) from like_pet where pet_id={PID}";
            string popul=DBHelper.GetScalar(query);
            return Convert.ToInt32(popul);
        }
        /// <summary>
        /// 只有存在条目才能删除
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public static void DeleteLikePet(string UID,string PID)
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
        public static void DeleteLikePets(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from like_pet where User_ID=:User_ID";
                command.Parameters.Clear();
                command.Parameters.Add("User_ID", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static DataTable GetLikePet(string user_id)
        {
            string query = "select like_pet.pet_id,pet_name,sex,avatar," +
                "TRUNC(MONTHS_BETWEEN(SYSDATE, birthdate) / 12) AS age from like_pet" +
                $" left join pet on pet.pet_id=like_pet.pet_id where user_id={user_id}";
            return DBHelper.ShowInfo(query);
        }
    }
}
