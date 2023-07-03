using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdopt.Utility
{
    public class EmployeeGenerator
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        private static string connectionString = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";"; // 替换为实际的数据库连接字符串
        public static void InsertEmployees()
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

                        for (int i = 1; i <= 15; i++)
                        {
                            string v_employee_name = "Employee ";
                            int seed = (int)(DateTime.Now.Ticks);
                            Random random = new Random(seed);
                            double v_salary = Math.Round(random.NextDouble() * (99999 - 10000) + 10000, 2);
                            string v_phone_number = string.Empty;
                            string v_duty = string.Empty;
                            int v_working_start_hr = 0;
                            int v_working_start_min = 0;
                            int v_working_end_hr = 0;
                            int v_working_end_min = 0;

                            if (i % 3 == 0)
                            {
                                v_phone_number = Math.Round(new Random().NextDouble() * (999 - 100) + 100) + "-" +
                                    Math.Round(new Random().NextDouble() * (9999 - 1000) + 1000) + "-" +
                                    Math.Round(new Random().NextDouble() * (9999 - 1000) + 1000);
                            }
                            else if (i % 3 == 1)
                            {
                                v_phone_number = Math.Round(new Random().NextDouble() * (9 - 1) + 1) +
                                    Math.Round(new Random().NextDouble() * (9999999999 - 1000000000) + 1000000000).ToString();
                            }
                            else
                            {
                                v_phone_number = Math.Round(new Random().NextDouble() * (999 - 100) + 100) + " " +
                                    Math.Round(new Random().NextDouble() * (9999 - 1000) + 1000) + " " +
                                    Math.Round(new Random().NextDouble() * (9999 - 1000) + 1000);
                            }

                            switch (i % 9)
                            {
                                case 0:
                                    v_duty = "Animal Care Specialist";
                                    break;
                                case 1:
                                    v_duty = "Adoption Counselor";
                                    break;
                                case 2:
                                    v_duty = "Veterinary Technician";
                                    break;
                                case 3:
                                    v_duty = "Animal Behaviorist";
                                    break;
                                case 4:
                                    v_duty = "Volunteer Coordinator";
                                    break;
                                case 5:
                                    v_duty = "Foster Coordinator";
                                    break;
                                case 6:
                                    v_duty = "Facility Manager";
                                    break;
                                case 7:
                                    v_duty = "Fundraising and Outreach Coordinator";
                                    break;
                                default:
                                    v_duty = "Rescue Transporter";
                                    break;
                            }

                            v_working_start_hr = (int)Math.Round(new Random().NextDouble() * (12 - 0) + 0);
                            v_working_start_min = (int)Math.Round(new Random().NextDouble() * (59 - 0) + 0);
                            v_working_end_hr = (int)Math.Round(new Random().NextDouble() * (23 - 12) + 12);
                            v_working_end_min = (int)Math.Round(new Random().NextDouble() * (59 - 0) + 0);

                            command.CommandText = $"INSERT INTO employee(employee_id, employee_name, salary, phone_number, duty, working_start_hr, working_start_min, working_end_hr, working_end_min) " +
                                $"VALUES (employee_id_seq.NEXTVAL, '{v_employee_name}', {v_salary}, '{v_phone_number}', '{v_duty}', {v_working_start_hr}, {v_working_start_min}, {v_working_end_hr}, {v_working_end_min})";

                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (OracleException ex)
                            {
                                if (ex.Number == 1)
                                {
                                    // 当唯一性约束违反时，忽略该异常并继续
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
