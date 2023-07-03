using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionUtility
{
    public class RoomGenerator
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        private static string connectionString = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";"; // 替换为实际的数据库连接字符串
        public static void InsertRoom()
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
                            for (int j = 1; j <= 30; j++)
                            {
                                int v_compartment = j;
                                char v_room_status = (j % 2 == 1) ? 'Y' : 'N';
                                double v_room_size = Math.Round(new Random().NextDouble() * (100 - 10) + 10, 2);
                                int v_storey = i;

                                command.CommandText = $"SELECT COUNT(*) FROM room WHERE storey = {v_storey}";
                                int room_count_by_storey = Convert.ToInt32(command.ExecuteScalar());

                                if (room_count_by_storey < 30)
                                {
                                    command.CommandText = $"INSERT INTO room(compartment, room_status, room_size, storey) " +
                                        $"VALUES ({v_compartment}, '{v_room_status}', {v_room_size}, {v_storey})";

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
