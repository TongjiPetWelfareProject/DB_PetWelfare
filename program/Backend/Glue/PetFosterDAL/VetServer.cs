using Oracle.ManagedDataAccess.Client;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace PetFoster.DAL
{
    public class VetServer
    {
        public static string conStr = AccommodateServer.conStr;
        //用户选择医生，需要展示,一名医生每天只能看8只宠物
        public static DataTable VetInfoForApmt(decimal Limitrows = -1, string Orderby = null)
        {
            string query = "SELECT vet_id, vet_name FROM vet v "
            +"WHERE 8 >= (" +
            "SELECT count(*) FROM appointment ap " +
            "WHERE custom_time > SYSTIMESTAMP and ap.vet_id = v.vet_id)";
            if (Limitrows > 0)
                query += $" where rownum<={Limitrows} ";
            if ((Orderby) != null)
                query += $" order by {Orderby} desc";
            DataTable dataTable = DBHelper.ShowInfo(query, Limitrows, Orderby);
            return dataTable;
        }
        //获取医生的图片
        public static Vet GetPortrait(string VID)
        {
            bool con = false;
            Vet vet = new Vet();
            vet.vet_id = "-1"; ;
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select vet_id,portrait from vet where Vet_ID=:vet_id";
                command.Parameters.Clear();
                command.Parameters.Add("vet_id", OracleDbType.Varchar2, VID, ParameterDirection.Input);
                try
                {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // 访问每一行的数据
                        // 其他列..
                        vet.vet_id = reader["Vet_ID"].ToString();
                        vet.portrait = (byte[])reader["Portrait"];
                        // 执行你的逻辑操作，例如将数据存储到自定义对象中或进行其他处理

                    }
                    if (vet.vet_id == "-1")
                        throw new Exception("不存在的用户，请注册新用户！");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                connection.Close();
            }

            return vet;
        }
        // 管理员端，查看兽医信息，由ShowProfiles(DataTable dt)调用
        public static DataTable VetInfo(decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM vet_labor ";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        public static string GetName(string VID)
        {
            bool con = false;
            User user1 = new User();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select vet_name from vet where Vet_ID=:vet_id";
                command.Parameters.Clear();
                command.Parameters.Add("vet_id", OracleDbType.Varchar2, VID, ParameterDirection.Input);
                try
                {
                    string username = command.ExecuteScalar() as string;
                    if (username == null)
                        return "Dr." + VID;
                    else
                        return "Dr."+username;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "Error";
                }
            }
        }
        /// <summary>
        /// 获取兽医的信息
        /// </summary>
        /// <param name="VID">兽医ID</param>
        /// <returns></returns>
        public static Vet GetVet(string VID)
        {
            bool con = false;
            Vet vet = new Vet();
            vet.vet_id = "-1"; ;
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select *from vet where Vet_ID=:vet_id";
                command.Parameters.Clear();
                command.Parameters.Add("vet_id", OracleDbType.Varchar2, VID, ParameterDirection.Input);
                try
                {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // 访问每一行的数据
                        // 其他列..
                        vet.vet_id = reader["Vet_ID"].ToString();
                        vet.vet_name = reader["User_Name"].ToString();
                        vet.phone_number = reader["phone_number"].ToString();
                        vet.working_start_hr = Convert.ToInt32(reader["working_start_hr"]);
                        vet.working_start_min = Convert.ToInt32(reader["working_start_min"]);
                        vet.working_end_hr = Convert.ToInt32(reader["working_end_hr"]);
                        vet.working_end_min = Convert.ToInt32(reader["working_end_min"]);
                        // 执行你的逻辑操作，例如将数据存储到自定义对象中或进行其他处理

                    }
                    if (vet.vet_id == "-1")
                        throw new Exception("不存在的用户，请注册新用户！");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                connection.Close();
            }

            return vet;
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
        /// 插入兽医的数据，由RecruitVet(Vet vet)调用
        /// </summary>
        /// <param name="vetname">兽医名字</param>
        /// <param name="Salary">工资</param>
        /// <param name="PhoneNumber">电话号码</param>
        /// <param name="wsh">Working_Start_Hour 工作开始时间(时)</param>
        /// <param name="wsm">Working_Start_Min 工作开始时间(分钟)</param>
        /// <param name="weh">Working_End_Hour 工作结束时间(时)</param>
        /// <param name="wem">Working_End_Min 工作结束时间(分钟)</param>
        /// <returns>新入职的兽医的ID</returns>
        public static int InsertVet(string vetname, decimal Salary, string PhoneNumber, decimal wsh, decimal wsm, decimal weh, decimal wem)
        {
            // 添加新行
            int PID = -1;
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO vet (vet_id, vet_name, phone_number,salary, working_start_hr,working_start_min," +
                        "working_end_hr,working_end_min) " +
                        "VALUES (vet_id_seq.NEXTVAL, :vet_name, :phone_number,:salary, :working_start_hr,:working_start_min,:working_end_hr,:working_end_min)";
                    command.Parameters.Clear();
                    command.Parameters.Add("vet_name", OracleDbType.Varchar2, vetname, ParameterDirection.Input);
                    command.Parameters.Add("phone_number", OracleDbType.Varchar2, PhoneNumber, ParameterDirection.Input);
                    command.Parameters.Add("salary", OracleDbType.Decimal, Salary, ParameterDirection.Input);
                    command.Parameters.Add("working_start_hr", OracleDbType.Decimal, wsh, ParameterDirection.Input);
                    command.Parameters.Add("working_start_min", OracleDbType.Decimal, wsm, ParameterDirection.Input);
                    command.Parameters.Add("working_end_hr", OracleDbType.Decimal, weh, ParameterDirection.Input);
                    command.Parameters.Add("working_end_min", OracleDbType.Decimal, wem, ParameterDirection.Input);
                    try
                    {
                        command.ExecuteNonQuery();
                        command.CommandText = "select max(cast(user_id as integer)) from user2";
                        command.Parameters.Clear();
                        PID = Convert.ToInt32(command.ExecuteScalar());
                        connection.Close();
                        return PID;
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("错误码" + ex.ErrorCode.ToString());
                        return -1;
                    }

                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
        /// <summary>
        /// 由开除兽医的Dismiss(Vet vet)函数调用，开除需要满足一定的条件，由BLL层判断
        /// </summary>
        /// <param name="VID">兽医的ID</param>
        /// <returns>是否开除成功</returns>
        public static bool DeleteVet(string VID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"delete from vet where Vet_ID={VID}";
                command.Parameters.Clear();
                try
                {
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 更改兽医信息，由涨工资/扣工资函数:SalaryManage(Vet vet,decimal newSalary)调用，修改工作时长函数LabourManage(...)，个人信息修改ProfileManage()控制
        /// 注意展示兽医信息时，需要展示工作时长，开始时分，终止时分，因此需要用到视图VetLabor
        /// </summary>
        /// <param name="vetname">兽医名字</param>
        /// <param name="Salary">工资</param>
        /// <param name="PhoneNumber">电话号码</param>
        /// <param name="wsh">Working_Start_Hour 工作开始时间(时)</param>
        /// <param name="wsm">Working_Start_Min 工作开始时间(分钟)</param>
        /// <param name="weh">Working_End_Hour 工作结束时间(时)</param>
        /// <param name="wem">Working_End_Min 工作结束时间(分钟)</param>
        public static void UpdateVet(string VID,string vetname, double Salary, string PhoneNumber, int wsh, int wsm, int weh, int wem)
        {
            // 更改信息
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE vet SET vet_name=:vet_name, salary=:salary, phone_number=:phone_number, " +
                        "working_start_hr=:working_start_hr, working_start_min=:working_start_min,working_end_hr=:working_end_hr," +
                        " working_end_min=:working_end_min " +
                        $" where vet_id={VID}";
                    command.Parameters.Clear();
                    command.Parameters.Add("vet_name", OracleDbType.Varchar2, vetname, ParameterDirection.Input);
                    command.Parameters.Add("salary", OracleDbType.Double, Salary, ParameterDirection.Input);
                    command.Parameters.Add("phone_number", OracleDbType.Varchar2, PhoneNumber, ParameterDirection.Input);
                    command.Parameters.Add("working_start_hr", OracleDbType.Decimal, wsh, ParameterDirection.Input);
                    command.Parameters.Add("working_start_min", OracleDbType.Decimal, wsm, ParameterDirection.Input);
                    command.Parameters.Add("working_end_hr", OracleDbType.Decimal, weh, ParameterDirection.Input);
                    command.Parameters.Add("working_end_min", OracleDbType.Decimal, wem, ParameterDirection.Input);
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