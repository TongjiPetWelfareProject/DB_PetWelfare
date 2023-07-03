using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using PetAdopt.Model;
using static PetAdopt.Model.DataSet1;
using System.IO;
namespace PetAdopt.DAL
{
    public class UserServer
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        private static string conStr = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";"; // 替换为实际的数据库连接字符串
        /// <summary>
        /// 用户登录时匹配用户信息，如果为0，说明密码错误,否则密码正确
        /// </summary>
        /// <param name="user">用户行</param>
        /// <returns></returns>
        public static bool GetUser(USER2Row user)
        {
            bool con = false;
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                // 在此块中执行数据操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select *from user2 where User_ID=:user_id and password=:pwd";
                command.Parameters.Clear();
                string UID = user.USER_ID;
                string pwd = user.PASSWORD;
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                command.Parameters.Add("pwd", OracleDbType.Varchar2, pwd, ParameterDirection.Input);
                    try
                    {
                        OracleDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            // 访问每一行的数据
                            string userId = reader["User_ID"].ToString();
                            string userName = reader["User_Name"].ToString();
                        // 其他列..
                            Console.WriteLine($"你好，{userName}，你已经登陆成功！");
                        con =true;
                            // 执行你的逻辑操作，例如将数据存储到自定义对象中或进行其他处理
                        }
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("不存在的用户或密码");

                        throw;
                    }
                
                    if(!con)
                    Console.WriteLine("不存在的用户或密码");
                connection.Close();
            }

            return con;
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
                throw new Exception(address+"地址不合法！");
            }
        }
        static bool IsValidStatus(string status)
        {
            // 读取配置文件并加载状态
            List<string> statuses = new List<string>();
            string configFile = "config/account_status.txt";
            string[] lines = File.ReadAllLines(configFile);
            statuses.AddRange(lines);
            if (statuses.Contains(status))
                return true;
            else
            {
                throw new Exception(status + "状态不合法！");
            }
        }
        /// <summary>
        /// 校验信息并注册
        /// </summary>
        /// <param name="Username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="Address">地址</param>
        public static void InsertUser(string Username,string pwd,string phoneNumber,string Address="Beijing")
        {
            // 添加新行
            try
            {
                IsValidAddress(Address);
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                        string account_status = "In Good Standing";
                        command.CommandText = "INSERT INTO user2 (user_id, user_name, password, phone_number, account_status, address) " +
                            "VALUES (user_id_seq.NEXTVAL, :user_name, :password, :phone_number, :account_status, :address)";
                        command.Parameters.Clear();
                        command.Parameters.Add("user_name", OracleDbType.Varchar2, Username, ParameterDirection.Input);
                        command.Parameters.Add("password", OracleDbType.Varchar2, pwd, ParameterDirection.Input);
                        command.Parameters.Add("phone_number", OracleDbType.Varchar2, phoneNumber, ParameterDirection.Input);
                        command.Parameters.Add("account_status", OracleDbType.Varchar2, account_status, ParameterDirection.Input);
                        command.Parameters.Add("address", OracleDbType.Varchar2, Address, ParameterDirection.Input);

                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (OracleException ex)
                        {
                            Console.WriteLine("错误码"+ex.ErrorCode.ToString());
                        
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
    }
}
