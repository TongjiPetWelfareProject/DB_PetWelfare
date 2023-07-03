using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionUtility
{
    public  class VetGenerator
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        private static string connectionString = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";"; // 替换为实际的数据库连接字符串
        public static void InsertVets()
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
                            string v_vet_id = "V" + i.ToString();
                            string v_vet_name = "Vet " + v_vet_id;
                            double v_salary = Math.Round(new Random().NextDouble() * (100000 - 50000) + 50000, 2);
                            string v_phone_number = "";

                            if (i % 3 == 0)
                            {
                                v_phone_number = $"{new Random().Next(100, 999)}-{new Random().Next(1000, 9999)}-{new Random().Next(1000, 9999)}";
                            }
                            else if (i % 3 == 1)
                            {
                                v_phone_number = $"{new Random().Next(1, 99)}{new Random().Next(100000000, 999999999)}";
                            }
                            else
                            {
                                v_phone_number = $"{new Random().Next(100, 999)} {new Random().Next(1000, 9999)} {new Random().Next(1000, 9999)}";
                            }

                            int v_working_start_hr = new Random().Next(0, 12);
                            int v_working_start_min = new Random().Next(0, 59);
                            int v_working_end_hr = new Random().Next(12, 23);
                            int v_working_end_min = new Random().Next(0, 59);

                            command.CommandText = $"INSERT INTO vet(vet_id, vet_name, salary, phone_number, working_start_hr, working_start_min, working_end_hr, working_end_min) " +
                                $"VALUES (vet_id_seq.NEXTVAL, '{v_vet_name}', {v_salary}, '{v_phone_number}', {v_working_start_hr}, {v_working_start_min}, {v_working_end_hr}, {v_working_end_min})";

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
