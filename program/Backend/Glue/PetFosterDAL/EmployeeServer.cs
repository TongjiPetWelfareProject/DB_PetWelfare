using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
namespace PetFoster.DAL
{
    public class EmployeeServer
    {
        public static string conStr = AccommodateServer.conStr;
        public static string GetName(string id)
        {
            string query = $"select employee_name from employee where employee_id={id}";
            return DBHelper.GetScalar(query);

        }
        /// <summary>
        /// 查看雇员信息，由ShowProfiles(DataTable dt)调用
        /// </summary>
        /// <param name="Limitrows">最多显示的行数</param>
        /// <param name="Orderby">排序的依据（降序）</param>
        /// <returns>返回数据表</returns>
        public static DataTable EmployeeInfo(decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM employee_labor ";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        /// <summary>
        /// 获取兽医的信息
        /// </summary>
        /// <param name="EID">兽医ID</param>
        /// <returns></returns>
        public static Employee GetEmployee(string EID)
        {
            bool con = false;
            Employee employee = new Employee();
            employee.employee_id = "-1"; ;
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from employee where Employee_ID=:employee_id";
                command.Parameters.Clear();
                command.Parameters.Add("employee_id", OracleDbType.Varchar2, EID, ParameterDirection.Input);
                try
                {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // 访问每一行的数据
                        // 其他列..
                        employee.employee_id = reader["Employee_ID"].ToString();
                        employee.employee_name = reader["Employee_Name"].ToString();
                        employee.phone_number = reader["phone_number"].ToString();
                        employee.working_start_hr = Convert.ToDecimal(reader["working_start_hr"]);
                        employee.working_start_min = Convert.ToDecimal(reader["working_start_min"]);
                        employee.working_end_hr = Convert.ToDecimal(reader["working_end_hr"]);
                        employee.working_end_min = Convert.ToDecimal(reader["working_end_min"]); ;
                        // 执行你的逻辑操作，例如将数据存储到自定义对象中或进行其他处理

                    }
                    if (employee.employee_id == "-1")
                        throw new Exception("不存在的用户，请注册新用户！");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                connection.Close();
            }

            return employee;
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
        /// 插入雇员的数据，由RecruitEmployee(Vet employee)调用
        /// </summary>
        /// <param name="vetname">雇员名字</param>
        /// <param name="Salary">工资</param>
        /// <param name="PhoneNumber">电话号码</param>
        /// <param name="wsh">Working_Start_Hour 工作开始时间(时)</param>
        /// <param name="wsm">Working_Start_Min 工作开始时间(分钟)</param>
        /// <param name="weh">Working_End_Hour 工作结束时间(时)</param>
        /// <param name="wem">Working_End_Min 工作结束时间(分钟)</param>
        /// <returns>新入职的雇员的ID</returns>
        public static int InsertEmpolyee(string EID,string vetname, decimal Salary, string Duty,double hours)
        {
            // 添加新行
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO employee (employee_id, employee_name, salary,duty, " +
                        "working_start_hr,working_start_min,working_end_hr,working_end_min) " +
                        "VALUES (:EID, :employee_name, :salary,:duty, 8,0,:working_end_hr,:working_end_min)";
                    command.Parameters.Clear();
                    command.Parameters.Add("EID", OracleDbType.Varchar2, EID, ParameterDirection.Input);
                    command.Parameters.Add("employee_name", OracleDbType.Varchar2, vetname, ParameterDirection.Input);
                    command.Parameters.Add("salary", OracleDbType.Decimal, Salary, ParameterDirection.Input);
                    command.Parameters.Add("duty", OracleDbType.Varchar2, Duty, ParameterDirection.Input);
                    command.Parameters.Add("working_end_hr", OracleDbType.Decimal, 8+Math.Floor(hours), ParameterDirection.Input);
                    command.Parameters.Add("working_end_min", OracleDbType.Decimal, Math.Floor(60 * (hours-Math.Floor(hours))), ParameterDirection.Input);
                    try
                    {
                        command.ExecuteNonQuery();

                        connection.Close();
                        return 0;
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
        /// 由开除员工的Dismiss(Employee employee)函数调用，开除需要满足一定的条件，由BLL层判断
        /// </summary>
        /// <param name="EID">员工的ID</param>
        /// <returns>是否开除成功</returns>
        public static bool DeleteEmployee(string EID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"delete from employee where employee_ID= {EID}";
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
        /// <summary>
        /// 更改员工信息，由涨工资/扣工资函数:SalaryManage(Vet employee,decimal newSalary)调用，修改工作时长函数LabourManage(...)，个人信息修改ProfileManage()
        /// 等控制
        /// 注意展示员工信息时，需要展示工作时长，开始时分，终止时分，因此需要用到视图VetLabor
        /// </summary>
        /// <param name="employee_name">员工名字</param>
        /// <param name="Salary">工资</param>
        /// <param name="PhoneNumber">电话号码</param>
        /// <param name="duty">职责</param>
        /// <param name="wsh">Working_Start_Hour 工作开始时间(时)</param>
        /// <param name="wsm">Working_Start_Min 工作开始时间(分钟)</param>
        /// <param name="weh">Working_End_Hour 工作结束时间(时)</param>
        /// <param name="wem">Working_End_Min 工作结束时间(分钟)</param>
        public static void UpdateEmployee(string EID,string employee_name, double Salary, string duty, decimal wsh, decimal wsm, decimal weh, decimal wem)
        {
            // 更改信息
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE employee SET employee_name=:employee_name, salary=:salary,  " +
                        "duty=:duty,working_start_hr=:working_start_hr, working_start_min=:working_start_min,working_end_hr=:working_end_hr,working_end_min=:working_end_min" +
                        $" where employee_id={EID}";
                    command.Parameters.Clear();
                    //command.Parameters.Add("EID", OracleDbType.Varchar2, EID, ParameterDirection.Input);
                    command.Parameters.Add("employee_name", OracleDbType.Varchar2, employee_name, ParameterDirection.Input);
                    command.Parameters.Add("salary", OracleDbType.Double, Salary, ParameterDirection.Input);
                    command.Parameters.Add("duty", OracleDbType.Varchar2, duty, ParameterDirection.Input);
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
