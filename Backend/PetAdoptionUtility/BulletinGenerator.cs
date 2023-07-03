using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdopt.Utility
{
    public class BulletinGenerator
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        private static string connectionString = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";"; // 替换为实际的数据库连接字符串
        public static void InsertBulletin()
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

                        for (int i = 1; i <= 50; i++)
                        {
                            int seed = DateTime.Now.Millisecond;
                            Random random = new Random(seed);
                            string v_employee_id =  random.ToString().PadLeft(2, '0');
                            string v_heading = "Heading" + i.ToString();
                            string v_bulletin_contents = "Bulletin Content " + i.ToString();
                            int v_read_count = new Random().Next(0, 1001);
                            DateTime v_published_time = DateTime.Now.AddDays(-new Random().Next(0, 1096)).AddSeconds(new Random().Next(0, 86400));

                            command.CommandText = $"INSERT INTO bulletin(bulletin_id, employee_id, heading, bulletin_contents, read_count, published_time) " +
                                $"VALUES (bulletin_id_seq.NEXTVAL, '{v_employee_id}', '{v_heading}', '{v_bulletin_contents}', {v_read_count}, TO_TIMESTAMP('{v_published_time.ToString("yyyy-MM-dd HH:mm:ss")}', 'YYYY-MM-DD HH24:MI:SS'))";

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
