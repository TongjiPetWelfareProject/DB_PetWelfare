using System;
using System.Data;
using System.IO;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
namespace PetAdopt.Utility
{
    public class PetGenerator
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        private static string connectionString = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";"; // 替换为实际的数据库连接字符串
        private static OracleConnection connection;

        public static void InsertPets()
        {
            try
            {
                connection = new OracleConnection(connectionString);
                connection.Open();

                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                for (int i = 1; i <= 20; i++)
                {
                    string pet_name = GenerateRandomString(10);
                    string breed = GetBreed(i);
                    int age = Convert.ToInt32(GenerateRandomNumber(0, 20));
                    byte[] avatar = GetRandomAvatar();
                    string health_state = GetHealthState(i);
                    string vaccine = GetVaccine(i);
                    int read_num = Convert.ToInt32(GenerateRandomNumber(0, 100));

                    command.CommandText = "INSERT INTO pet(pet_id, pet_name, breed, age, avatar, health_state, vaccine, read_num) " +
                        "VALUES (pet_id_seq.NEXTVAL, :pet_name, :breed, :age, :avatar, :health_state, :vaccine, :read_num)";
                    command.Parameters.Clear();
                    command.Parameters.Add("pet_name", OracleDbType.Varchar2, pet_name, ParameterDirection.Input);
                    command.Parameters.Add("breed", OracleDbType.Varchar2, breed, ParameterDirection.Input);
                    command.Parameters.Add("age", OracleDbType.Int32, age, ParameterDirection.Input);
                    command.Parameters.Add("avatar", OracleDbType.Blob, avatar, ParameterDirection.Input);
                    command.Parameters.Add("health_state", OracleDbType.Varchar2, health_state, ParameterDirection.Input);
                    command.Parameters.Add("vaccine", OracleDbType.Char, vaccine, ParameterDirection.Input);
                    command.Parameters.Add("read_num", OracleDbType.Int32, read_num, ParameterDirection.Input);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        if (ex.Number == 1) // 唯一性约束违反的错误码
                        {
                            // 忽略唯一性约束违反并继续
                            continue;
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        private static int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }
        public DataTable GetPets()
        {
            try
            {
                connection = new OracleConnection(connectionString);
                connection.Open();

                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM pet";

                OracleDataAdapter adapter = new OracleDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception ex)
            {
                // 处理异常
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void UpdatePet(int petId, string petName, string breed)
        {
            try
            {
                connection = new OracleConnection(connectionString);
                connection.Open();

                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE pet SET pet_name = :pet_name, breed = :breed WHERE pet_id = :pet_id";
                command.Parameters.Add("pet_name", OracleDbType.Varchar2, petName, ParameterDirection.Input);
                command.Parameters.Add("breed", OracleDbType.Varchar2, breed, ParameterDirection.Input);
                command.Parameters.Add("pet_id", OracleDbType.Int32, petId, ParameterDirection.Input);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // 处理异常
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void DeletePet(int petId)
        {
            try
            {
                connection = new OracleConnection(connectionString);
                connection.Open();

                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM pet WHERE pet_id = :pet_id";
                command.Parameters.Add("pet_id", OracleDbType.Int32, petId, ParameterDirection.Input);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // 处理异常
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string GetBreed(int index)
        {
            string[] breeds = {
            "German Shepherd", "Labrador Retriever", "Golden Retriever", "Bulldog", "Beagle",
            "Poodle", "Rottweiler", "Yorkshire Terrier", "Boxer", "Dachshund"
        };

            return breeds[(index - 1) % 10];
        }

        private static byte[] GetRandomAvatar()
        {
            // 获取随机的 avatar
            // ...
            string imagePath = "D:\\Picture\\picture" + GenerateRandomNumber(1, 4).ToString() + ".jfif";
            byte[] imageBytes = ConvertImageToByteArray(imagePath);
            return imageBytes;
        }


        public static byte[] ConvertImageToByteArray(string imagePath)
        {
            byte[] imageData;

            using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    imageData = binaryReader.ReadBytes((int)fileStream.Length);
                }
            }

            return imageData;
        }
        private static string GetHealthState(int index)
        {
            string[] healthStates = {
            "Vibrant", "Well", "Decent", "Unhealthy", "Sicky", "Critical"
        };

            return healthStates[(index - 1) % 6];
        }

        private static string GetVaccine(int index)
        {
            return (index % 2 == 0) ? "Y" : "N";
        }
    }

}
