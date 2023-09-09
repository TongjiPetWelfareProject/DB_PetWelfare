using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.DAL
{
    public class AppointmentServer
    {
        public static string conStr = AccommodateServer.conStr;
        public static bool DeleteAppointments(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"delete appointment where User_ID= {UID}";
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
        public static void InsertTreatTime(int pid,int vid,DateTime dt)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "update appointment set treat_time=CURRENT_TIMESTAMP" +
                        $" where pet_id='{pid}' and vet_id='{vid}' and custom_time=:dt1";
                    command.Parameters.Clear();
                    command.Parameters.Add("dt1", OracleDbType.TimeStamp, dt, ParameterDirection.Input);

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
                throw new Exception("数据库无法执行");
            }
        }
        public static bool DeleteAppointment(string VID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"delete from appointment where Vet_ID= {VID}";
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
        /// 查看申请信息，由ShowProfiles(DataTable dt)调用
        /// </summary>
        /// <param name="Limitrows">最多显示的行数</param>
        /// <param name="Orderby">排序的依据（降序）</param>
        /// <returns>返回数据表</returns>
        public static DataTable ApplyInfo(string UID = null, string PID = null, string Categories = null)
        {
            DataTable dataTable = new DataTable();
            //string query = "SELECT appointment.pet_id,pet_name,appointment.vet_id,vet_name,custom_time,reason FROM appointment" +
            //    " left join pet on pet.pet_id=appointment.pet_id left join vet on vet.vet_id=appointment.vet_id";
            string query = "SELECT appointment.pet_id as pet_id, appointment.vet_id as vet_id,pet_name,vet_name,custom_time as reserve_time," +
                " treat_Time,  reason as category ,case when treat_time is null then '申请' else '记录' end as tag FROM appointment" +
                " left join pet on pet.pet_id=appointment.pet_id left join vet on vet.vet_id=appointment.vet_id";
            if (UID != null && PID == null)
                query += $" where User_ID={UID} ";
            else if (PID != null && UID == null)
                query += $" where Pet_ID={PID}";
            if (UID != null && PID != null)
                query += $" where User_ID={UID} and Pet_ID={PID}";
            if (Categories != null)
                query += $" where category='{Categories}'";
            return DBHelper.ShowInfo(query);
        }
        /// <summary>
        /// 获取某人对某宠物的申请种类
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public static List<string> GetSpecies(string UID, string PID)
        {
            List<string> categories = new List<string>();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from appointment where User_ID=:user_id and Pet_ID:=Pet_ID";
                command.Parameters.Clear();
                command.Parameters.Add("user_id", OracleDbType.Varchar2, UID, ParameterDirection.Input);
                command.Parameters.Add("pet_id", OracleDbType.Varchar2, PID, ParameterDirection.Input);
                try
                {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        categories.Add(reader.GetString(2));

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                connection.Close();
            }

            return categories;
        }
        public static string InsertAppointment(string UID, string PID, string VID, DateTime dt, string reason)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    string account_status = "In Good Standing";
                    command.CommandText = "INSERT INTO appointment (user_id,pet_id,vet_id,reason,custom_time ) " +
                        $"VALUES ({UID},{PID},{VID},:reason,:dt)";
                    command.Parameters.Clear();
                    command.Parameters.Add("reason", OracleDbType.Varchar2, reason, ParameterDirection.Input);
                    command.Parameters.Add("dt", OracleDbType.TimeStamp, dt, ParameterDirection.Input);
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
                throw new Exception("数据库无法执行");
            }
            return UID;
        }
        public static DataTable GetUserAppointment(string UID, decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            string query = $"SELECT * FROM appointment where user_id={UID}";
            dataTable = DBHelper.ShowInfo(query, Limitrows, Orderby);

            dataTable.Columns.Add("VET_NAME", typeof(string));
            dataTable.Columns.Add("PET_NAME", typeof(string));

            foreach (DataRow row in dataTable.Rows)
            {
                string vetId = row["vet_id"].ToString();
                string petId = row["pet_id"].ToString();

                string vetName = VetServer.GetName(vetId);
                string petName = PetServer.GetName(petId);

                row["VET_NAME"] = vetName;
                row["PET_NAME"] = petName;
            }

            return dataTable;
        }

        public static void UpdateAppointment(int vid, int pid, DateTime origin_time, DateTime postpone_time)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "update appointment set custom_time=:dt1" +
                        $" where pet_id={pid} and vet_id={vid} and custom_time=:dt2";
                    command.Parameters.Clear();
                    command.Parameters.Add("dt1", OracleDbType.TimeStamp, postpone_time, ParameterDirection.Input);
                    command.Parameters.Add("dt2", OracleDbType.TimeStamp, origin_time, ParameterDirection.Input);

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
                throw new Exception("数据库无法执行");
            }
        }
    }
}