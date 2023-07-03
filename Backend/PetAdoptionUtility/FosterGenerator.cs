using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionUtility
{
    public class FosterGenerator
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        private static string connectionString = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";"; // 替换为实际的数据库连接字符串
        public static void InsertFoster()
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

                        for (int i = 1; i <= 10; i++)
                        {
                            int v_duration = (int)Math.Truncate(new Random().NextDouble() * (100 - 1) + 1);
                            string v_fosterer = Math.Truncate(new Random().NextDouble() * (51 - 1) + 1).ToString();
                            string v_pet_id = Math.Truncate(new Random().NextDouble() * (51 - 1) + 1).ToString();

                            command.CommandText = $"INSERT INTO foster(duration, fosterer, pet_id) VALUES ({v_duration}, '{v_fosterer}', '{v_pet_id}')";

                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (OracleException ex)
                            {
                                if (ex.Number == 1)
                                {
                                    // 当唯一性约束违反时，忽略并继续
                                    continue;
                                }
                                else
                                {
                                    // 处理其他异常
                                    throw;
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                throw;
            }

        }
    }
}
