using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionUtility
{
    public class CollectPetInfoGenerator
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        private static string connectionString = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";"; // 替换为实际的数据库连接字符串
        public static void InsertCollectPetInfo()
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

                        for (int i = 1; i <= 30; i++)
                        {
                            
                            string v_user_id = new Random().Next(1, 51).ToString();
                            string v_pet_id = new Random().Next(1, 51).ToString();

                            command.CommandText = $"INSERT INTO collect_pet_info(user_id, pet_id) " +
                                $"VALUES ('{v_user_id}', '{v_pet_id}')";

                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (OracleException ex)
                            {
                                // Handle exception
                                Console.WriteLine($"An error occurred - {ex.Number} - ERROR - {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine($"An error occurred - {ex.Message}");
            }

        }
    }
}
