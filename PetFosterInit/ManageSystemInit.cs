using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Database_CourseDesign
{
    public class ManageSystemInit
    {
        private string? connectionStr;
        private string? tableSpacePath;
        private string? managerID;
        private string? managerPwd;
        private string? tableSpaceTemp;
        private string? tableSpaceData;
        private string? picturePath;
        private string? path;
        public void PreInit()
        {
            var directory = System.AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar);
            directory[0] += '/';
            var slice = new ArraySegment<string>(directory, 0, directory.Length - 4);
            var programPath = Path.Combine(slice.ToArray());
            path = programPath;
            Console.WriteLine($"项目路径: {programPath}");
            ManageSystemInit manageSystemInit = new ManageSystemInit();
            manageSystemInit.Init(programPath);
            
        }
        public void Init(string programPath)
        {
            path = programPath;
            //根据项目路径设置配置文件路径
            string filePath = Path.Combine(path, "config.json");
            string jsonFromFile = File.ReadAllText(filePath);
            Dictionary<string, string> configFromFile = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonFromFile);
            //读取配置文件
            string dataSourse = configFromFile["dataSource"];
            tableSpacePath = configFromFile["tableSpacePath"];
            tableSpaceTemp = configFromFile["tableSpaceTemp"];
            tableSpaceData = configFromFile["tableSpaceData"];
            picturePath = configFromFile["picturePath"];
            //使用SYSTEM登录
            string userID = configFromFile["systemID"];
            string userPwd = configFromFile["systemPwd"];
            connectionStr = "DATA SOURCE=" + dataSourse + ";" +
                            "USER ID='\"" + userID + "\"';" +
                            "PASSWORD='" + userPwd + "'";

            Console.WriteLine("开始数据库初始化");
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

            InitTableSpace(systemConnection, userID, tableSpaceTemp);
            InitTableSpace(systemConnection, userID, tableSpaceData);
            InitUser(systemConnection, configFromFile);
            Console.WriteLine(userID + " 关闭数据库连接");
            //关闭数据库连接
            systemConnection.Close();

            //使用MANAGER登录
            userID = configFromFile["managerID"];
            userPwd = configFromFile["managerPwd"];
            connectionStr = "DATA SOURCE=" + dataSourse + ";" +
                            "USER ID='\"" + userID + "\"';" +
                            "PASSWORD='" + userPwd + "'";

            Console.WriteLine(userID + " 开始获取数据库连接");
            //根据连接配置创建数据库连接
            OracleConnection managerConnection = new OracleConnection(connectionStr);
            Console.WriteLine(userID + " 获取数据库连接成功");
            try
            {
                Console.WriteLine(userID + " 开始连接数据库");
                //启动数据库连接
                managerConnection.Open();
                Console.WriteLine(userID + " 连接数据库成功");
            }
            catch (OracleException ex)
            {
                Console.WriteLine(userID + " 连接数据库失败: " + ex.Message);
                return;
            }

            InitTable(managerConnection, userID);
            InitSequence(managerConnection, userID);
            InitTrigger(managerConnection, userID);
            InitView(managerConnection, userID);    
            Console.WriteLine(userID + " 关闭数据库连接");
            //关闭数据库连接
            systemConnection.Close();
        }

        private void InitTableSpace(OracleConnection connection, string userID, string Tablespace)
        {
            try
            {
                Console.WriteLine(userID + " 开始创建数据库表空间 temp");
                string createTableSpaceStr_1 =
                    "CREATE TEMPORARY TABLESPACE " + Tablespace + " " +
                    "TEMPFILE '" + tableSpacePath + Tablespace + ".dbf' " +
                    "SIZE 50m " +
                    "AUTOEXTEND ON " +
                    "NEXT 50m " +
                    "MAXSIZE 20480m";
                OracleCommand command_1 = new OracleCommand(createTableSpaceStr_1, connection);
                command_1.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                if (ex.ErrorCode == 1543)
                {
                    Console.WriteLine("已有同名表空间");
                }
                else
                {
                    Console.WriteLine("创建数据库表空间失败: " + ex.Message);
                }
            }
        }

        private void InitUser(OracleConnection connection, Dictionary<string, string> configFromFile)
        {
            managerID = configFromFile["managerID"];
            managerPwd = configFromFile["managerPwd"];
            string createManagerStr =
                "CREATE USER " + managerID + " IDENTIFIED BY " + managerPwd + " DEFAULT TABLESPACE " + tableSpaceData + " TEMPORARY TABLESPACE " + tableSpaceTemp;
            string[] grantManagerStrs = {
                "GRANT connect,resource TO " + managerID,
                "GRANT CREATE TABLE TO "+managerID,
                "GRANT CREATE SESSION TO "+managerID,
                "GRANT CREATE VIEW TO "+managerID,
                "GRANT CREATE TRIGGER TO "+managerID,
                "GRANT UNLIMITED TABLESPACE TO "+managerID+" -- 为用户在所有表空间上赋予无限制的配额",
                "GRANT CREATE SEQUENCE TO "+managerID,
                "GRANT CREATE MATERIALIZED VIEW TO "+managerID,
                $"CREATE OR REPLACE DIRECTORY my_dir AS '{picturePath}'--将D:// Desktop转化为存储图片的路径",
                "GRANT READ, WRITE ON DIRECTORY my_dir TO "+managerID
            };
            CreateUser(connection, createManagerStr, grantManagerStrs, managerID);
            GrantUser(connection, createManagerStr, grantManagerStrs, managerID);
        }
        private void InitTable(OracleConnection connection, string userID)
        {
            Console.WriteLine(userID + " 开始创建数据库表");
            XDocument doc = XDocument.Load(path + "\\SQLXML.xml");
            for (int i = 0; i < 17; i++)
            {
                try
                {
                    string sql = doc.Descendants("Query").Where(x => (string)x.Attribute("name") == ("CreateTable" + i.ToString())).FirstOrDefault()?.Value;
                    OracleCommand command = new OracleCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
                    if (ex.Code == 955)
                    {
                        Console.WriteLine(i.ToString() + " 已存在同名表");
                    }
                    else
                    {
                        Console.WriteLine(i.ToString() + " exception: " + ex.Message);
                    }
                }
            }
        }
        private void InitView(OracleConnection connection, string userID)
        {
            Console.WriteLine(userID + " 开始创建数据库视图/物化视图");
            XDocument doc = XDocument.Load(path + "\\SQLXML.xml");
            for (int i = 0; i < 6; i++)
            {
                try
                {
                    string sql = doc.Descendants("Query").Where(x => (string)x.Attribute("name") == ("CreateView" + i.ToString())).FirstOrDefault()?.Value;
                    OracleCommand command = new OracleCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
                    if (ex.Code == 955)
                    {
                        Console.WriteLine(i.ToString() + " 已存在同名视图/物化视图");
                    }
                    else
                    {
                        Console.WriteLine(i.ToString() + " exception: " + ex.Message);
                    }
                }
            }
        }
        private void InitTrigger(OracleConnection connection, string userID)
        {
            Console.WriteLine(userID + " 开始创建数据库触发器");
            XDocument doc = XDocument.Load(path + "\\SQLXML.xml");
            for (int i = 0; i < 8; i++)
            {
                try
                {
                    string sql = doc.Descendants("Query").Where(x => (string)x.Attribute("name") == ("CreateTrigger" + i.ToString())).FirstOrDefault()?.Value;
                    OracleCommand command = new OracleCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
                    if (ex.Code == 955)
                    {
                        Console.WriteLine(i.ToString() + " 已存在同名触发器");
                    }
                    else
                    {
                        Console.WriteLine(i.ToString() + " exception: " + ex.Message);
                    }
                }
            }
        }
        private void InitSequence(OracleConnection connection, string userID)
        {
            Console.WriteLine(userID + " 开始创建数据库序列");
            XDocument doc = XDocument.Load(path + "\\SQLXML.xml");
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    string sql = doc.Descendants("Query").Where(x => (string)x.Attribute("name") == ("CreateSequence" + i.ToString())).FirstOrDefault()?.Value;
                    OracleCommand command = new OracleCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
                    if (ex.Code == 955)
                    {
                        Console.WriteLine(i.ToString() + " 已存在同名序列");
                    }
                    else
                    {
                        Console.WriteLine(i.ToString() + " exception: " + ex.Message);
                    }
                }
            }
        }
        private void GrantUser(OracleConnection connection, string createUserStr, string[] grantUserStrs, string userID)
        {
            try
            {
                Console.WriteLine("开始授权用户 " + userID);
                
                foreach (var grantUserStr in grantUserStrs)
                {
                    Console.WriteLine(grantUserStr);
                    OracleCommand grantCommand = new OracleCommand(grantUserStr, connection);
                    grantCommand.ExecuteNonQuery();
                }
                Console.WriteLine("授权用户成功");
            }catch(OracleException ex)
            {
                if (ex.ErrorCode == 1920)
                {
                    Console.WriteLine("已有同名用户");
                }
                else
                {
                    Console.WriteLine("exception: " + ex.Message);
                }
            }
        }
        private void CreateUser(OracleConnection connection, string createUserStr, string[] grantUserStrs, string userID)
        {
            try
            {
                Console.WriteLine("开始创建用户 " + userID);
                OracleCommand createCommand = new OracleCommand(createUserStr, connection);
                createCommand.ExecuteNonQuery();
                Console.WriteLine("创建用户成功");
            }
            catch (OracleException ex)
            {
                if (ex.ErrorCode == 1920)
                {
                    Console.WriteLine("已有同名用户");
                }
                else
                {
                    Console.WriteLine("exception: " + ex.Message);
                }
            }
        }
        private void DropTables(string[] tables)
        {

        }
    }
}
