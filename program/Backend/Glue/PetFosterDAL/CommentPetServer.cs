using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using PetFoster.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.DAL
{
    public class CommentPetServer
    {
        public static string conStr = AccommodateServer.conStr;
        /// <summary>
        /// 注意SQL中EXTRACT的用法，这是给管理员看的，个人点赞是GetLikePetEntry...
        /// </summary>
        /// <param name="Limitrows"></param>
        /// <param name="Orderby"></param>
        /// <returns></returns>
        public static DataTable CommentPetInfo(decimal Limitrows = -1, string Orderby = null, string UID = "-1", string PID = "-1")
        {
            DataTable dataTable = new DataTable();
            string query = "";
            if (UID == "-1" && PID == "-1")
            {
                query = "SELECT user_id,pet_id,TO_CHAR(comment_time,'YYYY-MM-DD') as comment_date, TO_CHAR(comment_time,'HH24:MI:SS')as commented_time,comment_contents  FROM comment_pet";
            }
            else if (PID != "-1" && UID == "-1")
            {
                query = $"SELECT user_id,pet_id,TO_CHAR(comment_time,'YYYY-MM-DD') as comment_date, TO_CHAR(comment_time,'HH24:MI:SS')as commented_time,comment_contents  FROM comment_pet where Pet_ID={PID}";
            }
            else if (PID == "-1" && UID != "-1")
            {
                query = $"SELECT user_id,pet_id,TO_CHAR(comment_time,'YYYY-MM-DD') as comment_date, TO_CHAR(comment_time,'HH24:MI:SS')as commented_time,comment_contents  FROM comment_pet where User_ID={UID}";
            }
            else
            {
                query = $"SELECT user_id,pet_id,TO_CHAR(comment_time,'YYYY-MM-DD') as comment_date, TO_CHAR(comment_time,'HH24:MI:SS')as commented_time,comment_contents  FROM comment_pet where Pet_ID={PID} and User_ID={UID}";
            }
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        public static void InsertCommentPet(string UID, string PID,string content)
        {
            // 添加新行
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO comment_pet (user_id, pet_id,comment_contents) " +
                        "VALUES (:user_id,:pet_id,:comment_contents)";
                    command.Parameters.Clear();
                    command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                    command.Parameters.Add("pet_id", OracleDbType.Varchar2, PID, ParameterDirection.Input);
                    command.Parameters.Add("comment_contents", OracleDbType.Varchar2, content, ParameterDirection.Input);

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"{UID}给{PID}在{DateTime.Now}评论");

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
        public static void DeleteCommentPets(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from comment_pet where User_ID=:User_ID";
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
        /// <summary>
        /// 只有存在条目才能删除
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public static void DeleteCommentPet(string UID, string PID, DateTime datetime)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
             {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"delete from comment_pet where Pet_ID= :Pet_ID and User_ID=:User_ID  AND comment_time=:DateTime";
                command.Parameters.Clear();
                command.Parameters.Add("Pet_ID", OracleDbType.Varchar2, PID, ParameterDirection.Input);
                command.Parameters.Add("User_ID", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                command.Parameters.Add("DateTime", OracleDbType.TimeStamp, datetime, ParameterDirection.Input);
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"{UID}给{PID}的评论已取消");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"不存在{UID}给{PID}的评论");
                }
            }
        }

        internal static DataTable GetCommentPets(string user_id)
        {
            string query = "select user_name,pet_name,comment_time,comment_contents from comment_pet left join " +
                $"pet on pet.pet_id=comment_pet.pet_id left join" +
                $" user2 on user2.user_id=comment_pet.user_id where comment_pet.user_id={user_id}";
            return DBHelper.ShowInfo(query);
        }
    }
}
