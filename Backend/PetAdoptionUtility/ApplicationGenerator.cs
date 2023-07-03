using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionUtility
{
    public class ApplicationGenerator
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        private static string connectionString = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";"; // 替换为实际的数据库连接字符串
        public static void InsertApplications()
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

                        // Insertion into donation table
                        for (int i = 1; i <= 10; i++)
                        {

                            int seed = DateTime.Now.Millisecond;
                            Random random = new Random(seed);
                            string v_donor_id = random.Next(1, 51).ToString();
                            seed = DateTime.Now.Millisecond;
                            random = new Random(seed);
                            int v_donation_amount = random.Next(1, 5000);

                            command.CommandText = $"INSERT INTO donation(donor_id, donation_amount) " +
                                $"VALUES ('{v_donor_id}', {v_donation_amount})";

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

                        // Insertion into application table
                        for (int i = 1; i <= 10; i++)
                        {
                            string v_pet_id = new Random().Next(1, 51).ToString();
                            string v_user_id = new Random().Next(1, 51).ToString();
                            string v_category = "";
                            string v_reason = "Reason " + v_pet_id;

                            switch (i % 7)
                            {
                                case 0:
                                    v_category = "Lost Pet";
                                    break;
                                case 1:
                                    v_category = "Treatment";
                                    break;
                                case 2:
                                    v_category = "Vaccination";
                                    break;
                                case 3:
                                    v_category = "Breeding";
                                    break;
                                case 4:
                                    v_category = "Adoption";
                                    break;
                                case 5:
                                    v_category = "Donation";
                                    break;
                                default:
                                    v_category = "Complaint";
                                    break;
                            }

                            command.CommandText = $"INSERT INTO application(pet_id, user_id, category, reason) " +
                                $"VALUES ('{v_pet_id}', '{v_user_id}', '{v_category}', '{v_reason}')";

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
