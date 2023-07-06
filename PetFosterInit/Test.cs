using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Text.Json;


namespace Database_CourseDesign
{
    internal class Test
    {
        private string connectionStr;
        private string path;
        private string userID;
        public Test(string programPath)
        { 
            path = programPath;
            //根据项目路径设置配置文件路径
            string filePath = Path.Combine(path, "config.json");
            string jsonFromFile = File.ReadAllText(filePath);
            var configFromFile = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonFromFile);
            //读取配置文件
            string dataSourse = configFromFile["dataSource"];
            //使用SYSTEM登录
            userID = configFromFile["systemID"];
            string userPwd = configFromFile["systemPwd"];
            connectionStr = "DATA SOURCE=" + dataSourse + ";" +
                            "USER ID='\"" + userID + "\"';" +
                            "PASSWORD='" + userPwd + "'";
        }

        public void ConnectTest()
        {
            Console.WriteLine(userID + " 开始获取数据库连接");
            //根据连接配置创建数据库连接
            OracleConnection systemConnection = new OracleConnection(connectionStr);
            Console.WriteLine(userID + " 获取数据库连接成功");

            try
            {
                Console.WriteLine(userID + " 开始连接数据库");
                //启动数据库连接
                systemConnection.Open();
                Console.WriteLine(userID + " 连接数据库成功");
            }
            catch (OracleException ex)
            {
                Console.WriteLine(userID + " 连接数据库失败: " + ex.Message);
                return;
            }

            Console.WriteLine(userID + " 关闭数据库连接");
            //关闭数据库连接
            systemConnection.Close();
        }

        public void PrintTableTest()
        {
            Console.WriteLine(userID + " 开始获取数据库连接");
            //根据连接配置创建数据库连接
            OracleConnection systemConnection = new OracleConnection(connectionStr);
            Console.WriteLine(userID + " 获取数据库连接成功");

            try
            {
                Console.WriteLine(userID + " 开始连接数据库");
                //启动数据库连接
                systemConnection.Open();
                Console.WriteLine(userID + " 连接数据库成功");
            }
            catch (OracleException ex)
            {
                Console.WriteLine(userID + " 连接数据库失败: " + ex.Message);
                return;
            }

            string SQLStr = "SELECT table_name FROM user_tables";
            OracleCommand command = new OracleCommand(SQLStr, systemConnection);
            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            }

            Console.WriteLine(userID + " 关闭数据库连接");
            //关闭数据库连接
            systemConnection.Close();
        }
    }
}
