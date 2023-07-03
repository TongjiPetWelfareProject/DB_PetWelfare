using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
namespace PetAdopt.Utility
{
    public class UserModel
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        public static void InsertUsers()
        {
            string conStringUser = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";";

            try
            {
                using (OracleConnection connection = new OracleConnection(conStringUser))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;

                    for (int i = 1; i <= 50; i++)
                    {
                        string user_name = GenerateRandomString(20);
                        string password = "Password1!";
                        string phone_number = string.Empty;
                        string account_status = string.Empty;
                        string address = "Shanghai";

                        switch (i % 3)
                        {
                            case 0:
                                phone_number = $"{GenerateRandomNumber(100, 999)}-{GenerateRandomNumber(1000, 9999)}-{GenerateRandomNumber(1000, 9999)}";
                                break;
                            case 1:
                                phone_number = $"{GenerateRandomNumber(1, 9)}{GenerateRandomNumber(1000000000, 2000000000)}";
                                break;
                            default:
                                phone_number = $"{GenerateRandomNumber(100, 999)} {GenerateRandomNumber(1000, 9999)} {GenerateRandomNumber(1000, 9999)}";
                                break;
                        }

                        switch (i % 7)
                        {
                            case 0:
                                account_status = "Compliant";
                                break;
                            case 1:
                                account_status = "In Good Standing";
                                break;
                            case 2:
                                account_status = "Under Review";
                                break;
                            case 3:
                                account_status = "Warning Issued";
                                break;
                            case 4:
                                account_status = "Suspended";
                                break;
                            case 5:
                                account_status = "Probation";
                                break;
                            default:
                                account_status = "Banned";
                                break;
                        }

                        command.CommandText = "INSERT INTO user2 (user_id, user_name, password, phone_number, account_status, address) " +
                            "VALUES (user_id_seq.NEXTVAL, :user_name, :password, :phone_number, :account_status, :address)";
                        command.Parameters.Clear();
                        command.Parameters.Add("user_name", OracleDbType.Varchar2, user_name, ParameterDirection.Input);
                        command.Parameters.Add("password", OracleDbType.Varchar2, password, ParameterDirection.Input);
                        command.Parameters.Add("phone_number", OracleDbType.Varchar2, phone_number, ParameterDirection.Input);
                        command.Parameters.Add("account_status", OracleDbType.Varchar2, account_status, ParameterDirection.Input);
                        command.Parameters.Add("address", OracleDbType.Varchar2, address, ParameterDirection.Input);

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
            }
            catch (Exception ex)
            {
                // 处理异常
                throw;
            }
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }
    }

}
