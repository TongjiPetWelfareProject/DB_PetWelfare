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
    public class CommentPostServer
    {
        public static string conStr = AccommodateServer.conStr;
        public static void DeleteCommentPosts(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from comment_post where User_ID=:User_ID";
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
        public static DataTable CommentPostInfo(decimal Limitrows = -1, string Orderby = null, string UID = "-1", string PID = "-1")
        {
            DataTable dataTable = new DataTable();
            string query = "";
            if (UID == "-1" && PID == "-1")
            {
                query = "SELECT user_id,post_id,TO_CHAR(comment_time,'YYYY-MM-DD') as comment_date, TO_CHAR(comment_time,'HH24:MI:SS')as commented_time,comment_contents  FROM comment_post";
            }
            else if (PID != "-1" && UID == "-1")
            {
                query = $"SELECT user_id,post_id,TO_CHAR(comment_time,'YYYY-MM-DD') as comment_date, TO_CHAR(comment_time,'HH24:MI:SS')as commented_time,comment_contents  FROM comment_post where Post_ID={PID}";
            }
            else if (PID == "-1" && UID != "-1")
            {
                query = $"SELECT user_id,post_id,TO_CHAR(comment_time,'YYYY-MM-DD') as comment_date, TO_CHAR(comment_time,'HH24:MI:SS')as commented_time,comment_contents  FROM comment_post where User_ID={UID}";
            }
            else
            {
                query = $"SELECT user_id,post_id,TO_CHAR(comment_time,'YYYY-MM-DD') as comment_date, TO_CHAR(comment_time,'HH24:MI:SS')as commented_time,comment_contents  FROM comment_post where Post_ID={PID} and User_ID={UID}";
            }
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        public static int InsertCommentPost(string UID, string PID, string content)
        {
            // 添加新行
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO comment_post (user_id, post_id,comment_contents) " +
                        "VALUES (:user_id,:post_id,:comment_contents)";
                    command.Parameters.Clear();
                    command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                    command.Parameters.Add("post_id", OracleDbType.Varchar2, PID, ParameterDirection.Input);
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
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// 只有存在条目才能删除
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public static void DeleteCommentPost(string UID, string PID, DateTime dateTime)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from comment_post where Post_ID= :Post_ID and User_ID=:User_ID" +
                    " and comment_time=:dt";
                command.Parameters.Clear();
                command.Parameters.Add("Post_ID", OracleDbType.Varchar2, PID, ParameterDirection.Input);
                command.Parameters.Add("User_ID", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                command.Parameters.Add("dt", OracleDbType.TimeStamp, dateTime, ParameterDirection.Input);
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
        public static int GetCommentPostNums(string PID)
        {
            string query = $"select count(*) from comment_post where post_id={PID}";
            return DBHelper.GetScalarInt(query);
        }
        public static void DeleteAllCommentsForPost(string FID)
        {
            string query = $"delete comment_post where post_id={FID}";
            DBHelper.ExecuteNonScalar(query);
        }
        public static List<PostComment> GetAllComment(string inPID)
        {
            List<PostComment> postcomments = new List<PostComment>();

            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();

                string query = $"select user_id, comment_time, comment_contents from comment_post where post_id={inPID}";

                OracleCommand command = new OracleCommand(query, connection);

                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PostComment comment = new PostComment
                        {
                            PID = inPID,
                            UID = reader["user_id"].ToString(),
                            Avatar = UserServer.GetAvatar(reader["user_id"].ToString()),
                            Comment_Time = Convert.ToDateTime(reader["comment_time"]),
                            Content = reader["comment_contents"].ToString(),
                            User_Name = UserServer.GetName(reader["user_id"].ToString()),
                            Post_Title = ForumPostServer.PIDtoPostTitle(inPID)
                        };
                        postcomments.Add(comment);
                    }
                }

                connection.Close();
            }

            return postcomments;
        }
        public static List<PostComment> GetUserComment(string UID)
        {
            List<PostComment> postcomments = new List<PostComment>();

            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();

                string query = $"select user_id, post_id, comment_time, comment_contents from comment_post where user_id={UID}";

                OracleCommand command = new OracleCommand(query, connection);

                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PostComment comment = new PostComment
                        {
                            PID = reader["post_id"].ToString(),
                            UID = reader["user_id"].ToString(),
                            Avatar = UserServer.GetAvatar(reader["user_id"].ToString()),
                            Comment_Time = Convert.ToDateTime(reader["comment_time"]),
                            Content = reader["comment_contents"].ToString(),
                            Post_Title = ForumPostServer.PIDtoPostTitle(reader["post_id"].ToString())
                        };
                        postcomments.Add(comment);
                    }
                }

                connection.Close();
            }

            return postcomments;
        }
    }
}
