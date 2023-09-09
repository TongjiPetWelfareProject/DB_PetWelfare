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
    public class LikePostServer
    {
        public static string conStr = AccommodateServer.conStr;
        public static void DeleteLikePosts(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from like_post where User_ID=:User_ID";
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
        /// 注意SQL中EXTRACT的用法，这是给管理员看的，个人点赞是GetLikePetEntry...
        /// </summary>
        /// <param name="Limitrows"></param>
        /// <param name="Orderby"></param>
        /// <returns></returns>
        public static DataTable LikePostInfo(decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT user_id,post_id,TO_CHAR(like_time,'YYYY-MM-DD') as liked_date, TO_CHAR(like_time,'HH24:MI:SS') as liked_time FROM like_post";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        /// <summary>
        /// 获取点赞帖子条目
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        /// <returns>true表示有条目，则可以删除，false表示可以点赞</returns>
        public static bool GetLikePostEntry(string UID, string PID)
        {
            bool con = false;
            User user1 = new User();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"select *from like_post where Post_ID={PID} and User_ID={UID}";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                command.Parameters.Add("post_id", OracleDbType.Varchar2, PID, ParameterDirection.Input);
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
        public static void InsertLikePost(string UID, string PID)
        {
            // 添加新行
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO like_post (user_id, post_id) " +
                        "VALUES (:user_id,:post_id)";
                    command.Parameters.Clear();
                    command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                    command.Parameters.Add("post_id", OracleDbType.Varchar2, PID, ParameterDirection.Input);

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
        public static void DeleteAllLikesForPost(string FID)
        {
            string query = $"delete like_post where post_id={FID}";
            DBHelper.ExecuteNonScalar(query);
        }
        /// <summary>
        /// 只有存在条目才能删除
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public static void DeleteLikePost(string UID, string PID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from like_post where Post_ID= :Post_ID and User_ID=:User_ID";
                command.Parameters.Clear();
                command.Parameters.Add("Post_ID", OracleDbType.Varchar2, PID, ParameterDirection.Input);
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
        public static int GetLikePostNums(string PID)
        {
            string query = $"select count(*) from like_post where post_id={PID}";
            return DBHelper.GetScalarInt(query);
        }

        public static DataTable GetLikePosts(string user_id)
        {
            string query = $"select heading,post_time,like_numpost_func(like_post.post_id) as like_num,like_post.post_id," +
                $"comment_numpost_func(like_post.post_id) as comment_num," +
                $"read_count from like_post left join forum_posts " +
                $"on forum_posts.post_id=like_post.post_id where like_post.user_id={user_id}";
            return DBHelper.ShowInfo(query);
        }
    }
}
