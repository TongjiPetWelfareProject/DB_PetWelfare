using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionUtility
{
    public class ForumPostGenerator
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        private static string connectionString = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";"; // 替换为实际的数据库连接字符串
        public static void InsertForumPosts()
        {

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.Text;

                        for (int i = 1; i <= 20; i++)
                        {
                            string v_post_id = i.ToString();
                            string v_user_id = new Random().Next(0, 50).ToString();
                            string v_post_contents = "This is post " + v_post_id;
                            int v_read_count = new Random().Next(0, 1001);

                            command.CommandText = $"INSERT INTO forum_posts(post_id, user_id, post_contents, read_count) " +
                                $"VALUES (post_id_seq.NEXTVAL, '{v_user_id}', '{v_post_contents}', {v_read_count})";

                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (OracleException ex)
                            {
                                // 处理异常
                                Console.WriteLine($"An error occurred - {ex.Number} - ERROR - {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine($"An error occurred - {ex.Message}");
            }

        }
    }
}
