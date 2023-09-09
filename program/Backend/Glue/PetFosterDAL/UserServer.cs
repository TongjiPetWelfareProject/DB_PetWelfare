using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using PetFoster.Model;
using static PetFoster.Model.PetData;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace PetFoster.DAL
{
    public class UserServer
    {
        public static string conStr = AccommodateServer.conStr;
        /// <summary>
        /// 查看用户信息，由ShowProfiles(DataTable dt)调用
        /// 注意用户的密码不能用明文存储，最起码的要求密码不能在客户端显示！！！
        /// </summary>
        /// <param name="Limitrows">最多显示的行数</param>
        /// <param name="Orderby">排序的依据（降序）</param>
        /// <returns>返回数据表</returns>
        public static DataTable UserInfo(decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT user_id,user_name,phone_number,account_status,address FROM user2 where role='User'";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }

        /// <summary>
        /// 用户登录时匹配用户信息，如果为0，说明密码错误,否则密码正确
        /// </summary>
        /// <param name="user">用户行</param>
        /// <returns></returns>
        public static User GetUser(string UID, string pwd, bool IsAdmin = false)
        {
            bool con = false;
            User user1 = new User();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                command.CommandText = "select *from user2 where User_ID=:user_id";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                try
                {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // 访问每一行的数据
                        // 其他列..
                        user1.User_ID = reader["User_ID"].ToString();
                        user1.User_Name = reader["User_Name"].ToString();
                        user1.Account_Status = reader["Account_Status"].ToString();
                        user1.Address = reader["Address"].ToString();
                        user1.Password = reader["Password"].ToString();
                        user1.Phone_Number = reader["Phone_Number"].ToString();
                        user1.Role = reader["Role"].ToString();
                        user1.Avatar = GetAvatar(reader["User_ID"].ToString());
                    }
                    if (user1.User_ID == "-1")
                        throw new Exception("不存在的用户，请注册新用户！");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                connection.Close();
            }

            return user1;
        }
        public static int CheckUserExistence(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                command.CommandText = "SELECT COUNT(*) FROM user2 WHERE User_ID = :user_id";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);

                try
                {
                    int userCount = Convert.ToInt32(command.ExecuteScalar());

                    if (userCount == 0)
                    {
                        // 用户不存在
                        return -1;
                    }
                    else
                    {
                        // 用户存在
                        return 1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static int CheckUserPhoneExistence(string phone)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                command.CommandText = "SELECT COUNT(*) FROM user2 WHERE Phone_Number = :user_phone";
                command.Parameters.Clear();
                command.Parameters.Add("user_phone", OracleDbType.Varchar2, phone, ParameterDirection.Input);

                try
                {
                    int userCount = Convert.ToInt32(command.ExecuteScalar());

                    if (userCount == 0)
                    {
                        // 用户不存在
                        return -1;
                    }
                    else
                    {
                        // 用户存在
                        return 1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public static Profile GetStatistics(int UID, out int err)
        {
            bool con = false;
            err = 0;
            Profile profile = new Profile(-1, -1);
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                profile.getlikes = -1;
                command.CommandText = $"select total_like,clicks from user_profile where User_ID={UID}";
                try
                {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // 访问每一行的数据
                        // 其他列..
                        profile.getlikes = Convert.ToInt32(reader["Total_Like"]);
                        profile.getclicks = Convert.ToInt32(reader["Clicks"]);
                        // 执行你的逻辑操作，例如将数据存储到自定义对象中或进行其他处理

                    }
                    if (profile.getlikes == -1)
                    {
                        err = -1;
                        throw new Exception("你要查找的用户不存在！");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    err = -2;//查询过程异常
                }
                connection.Close();
            }

            return profile;
        }
        public static User GetUserByTel(string Tel)
        {
            User user1 = new User();

            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM user2 WHERE Phone_Number = :tel";
                command.Parameters.Add(new OracleParameter(":tel", Tel));

                try
                {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        user1.User_ID = reader["User_ID"].ToString();
                        user1.User_Name = reader["User_Name"].ToString();
                        user1.Account_Status = reader["Account_Status"].ToString();
                        user1.Address = reader["Address"].ToString();
                        user1.Password = reader["Password"].ToString();
                        user1.Phone_Number = reader["Phone_Number"].ToString();
                        user1.Role = reader["Role"].ToString();
                        user1.Avatar = GetAvatar(reader["User_ID"].ToString());
                    }
                    if (user1.User_ID == "-1")
                        throw new Exception("不存在的用户，请注册新用户！");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return user1;
        }

        public static string GetRole(string UID)
        {
            bool con = false;
            User user1 = new User();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select role from user2 where User_ID=:user_id";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                try
                {
                    string role = command.ExecuteScalar() as string;
                    if (role == null)
                        return "Unknown";
                    else
                        return role;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "Error";
                }
            }
        }
        public static string GetStatus(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select account_status from user2 where User_ID=:user_id";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                try
                {
                    string status = command.ExecuteScalar() as string;
                    return status;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "Banned";
                }
            }
        }
        public static string GetAvatar(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select avatar from user2 where User_ID=:user_id";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                try
                {
                    string path = command.ExecuteScalar() as string;
                    return path;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public static string GetPhone(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select phone_number from user2 where User_ID=:user_id";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                try
                {
                    string phone = command.ExecuteScalar() as string;
                    return phone;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "No Phone";
                }
            }
        }
        public static string GetAddress(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select address from user2 where User_ID=:user_id";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                try
                {
                    string address = command.ExecuteScalar() as string;
                    return address;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "No Address";
                }
            }
        }
        public static string GetName(string UID)
        {
            bool con = false;
            User user1 = new User();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select user_name from user2 where User_ID=:user_id";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                try
                {
                    string username = command.ExecuteScalar() as string;
                    if (username == null)
                        return "User"+UID;
                    else
                        return username;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "Error";
                }
            }
        }
        public static int GetTotalLikes(string UID)
        {
            int totalLikes = 0;
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select sum(like_num) from forum_posts where user_id = :user_id";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        totalLikes = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return totalLikes;
        }
        public static int GetTotalReadCount(string UID)
        {
            int totalLikes = 0;
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select sum(read_count) from forum_posts where user_id = :user_id";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                try
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        totalLikes = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return totalLikes;
        }


        static bool IsValidAddress(string address)
        {
            // 读取配置文件并加载地址
            List<string> addresses = new List<string>();
            string configFile = "config/addresses.txt";
            string[] lines = File.ReadAllLines(configFile);
            addresses.AddRange(lines);
            if (addresses.Contains(address))
                return true;
            else
            {
                throw new Exception(address + "地址不合法！");
            }
        }
        /// <summary>
        /// 校验信息并注册
        /// </summary>
        /// <param name="Username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="Address">地址</param>
        public static string InsertUser(string Username, string pwd, string phoneNumber, string Address = "Beijing",string Role="User")
        {
            string UID = "-1";
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    string account_status = "In Good Standing";
                    command.CommandText = "INSERT INTO user2 (user_id, user_name, password, phone_number, account_status, address,role) " +
                        "VALUES (user_id_seq.NEXTVAL, :user_name, :password, :phone_number, :account_status, :address,:Role)";
                    command.Parameters.Clear();
                    command.Parameters.Add("user_name", OracleDbType.Varchar2, Username, ParameterDirection.Input);
                    command.Parameters.Add("password", OracleDbType.Varchar2, pwd, ParameterDirection.Input);
                    command.Parameters.Add("phone_number", OracleDbType.Varchar2, phoneNumber, ParameterDirection.Input);
                    command.Parameters.Add("account_status", OracleDbType.Varchar2, account_status, ParameterDirection.Input);
                    command.Parameters.Add("address", OracleDbType.Varchar2, Address, ParameterDirection.Input);
                    command.Parameters.Add("Role", OracleDbType.Varchar2, Role, ParameterDirection.Input);
                    try
                    {
                        command.ExecuteNonQuery();
                        command.CommandText = "select user_id_seq.CURRVAL from dual";
                        UID = command.ExecuteScalar().ToString();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("错误码" + ex.ErrorCode.ToString());
                        throw;
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.ToString());
            }
            return UID;
        }

        public static bool DeleteUser(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"delete user2 where User_ID= {UID}";
                command.Parameters.Clear();
                try
                {
                    command.ExecuteNonQuery();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public static void UpdateUser2(string UID, string Username, string phoneNumber, string Address)
        {
            // 更改信息
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();
                    command.CommandText = $"UPDATE user2 SET user_name='{Username}', phone_number='{phoneNumber}', " +
                        $"address='{Address}' where user_id={UID}";
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("错误码" + ex.ErrorCode.ToString());
                        throw;
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.ToString());
            }
        }
        public static void UpdateUserAvatar(string UID, string url)
        {
            // 添加新行
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"UPDATE user2 SET " +
                        $"avatar=:avatar where user_id={UID}";
                    command.Parameters.Clear();
                    command.Parameters.Add("avatar", OracleDbType.Varchar2, url, ParameterDirection.Input);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("错误码" + ex.ErrorCode.ToString());
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.ToString());
            }
        }
        public static void UpdateUser(string UID, string Username, string pwd, string phoneNumber, string Address = "Beijing", string account_status = "In Good Standing")
        {
            // 更改信息
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE user2 SET user_name=:user_name, phone_number=:phone_number, " +
                        "account_status=:account_status, address=:address where user_id=:user_id";
                    command.Parameters.Clear();
                    command.Parameters.Add("user_name", OracleDbType.Varchar2, Username, ParameterDirection.Input);
                    command.Parameters.Add("phone_number", OracleDbType.Varchar2, phoneNumber, ParameterDirection.Input);
                    command.Parameters.Add("account_status", OracleDbType.Varchar2, account_status, ParameterDirection.Input);
                    command.Parameters.Add("address", OracleDbType.Varchar2, Address, ParameterDirection.Input);
                    command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("错误码" + ex.ErrorCode.ToString());

                        throw;
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.ToString());
            }
        }
        public static void UpdatePassword(string UID, string pwd)
        {
            // 更改信息
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE user2 SET password=:password " +
                        " where user_id=:user_id";
                    command.Parameters.Clear();
                    command.Parameters.Add("password", OracleDbType.Varchar2, pwd, ParameterDirection.Input);
                    command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("错误码" + ex.ErrorCode.ToString());

                        throw;
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
