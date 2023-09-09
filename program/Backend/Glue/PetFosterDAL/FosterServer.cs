using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.DAL
{
    public class FosterServer
    {
        public static string conStr = AccommodateServer.conStr;
        public static bool DeleteFosters(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"delete foster where Fosterer= {UID}";
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
        /// 查看寄养信息
        /// </summary>
        /// <param name="censorStr">审核状态</param>
        /// <param name="Limitrows"></param>
        /// <param name="Orderby"></param>
        /// <param name="verbose">是否查看详细信息</param>
        /// <returns></returns>
        public static DataTable FosterInfo(string censorStr="",decimal Limitrows = -1, string Orderby = null,bool verbose=false)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT duration,fosterer,foster.pet_id,to_char(start_year)||'-'||to_char(start_month)||'-'||to_char(start_day) AS STARTDATE" +
                    ",REMARK,censor_state,user_name,pet_name FROM foster " +
                    " left join user2 on user2.user_id=foster.fosterer" +
                    " left join pet on pet.pet_id=foster.pet_id";
            if (verbose)
                query = "SELECT * from foster_window";
            if (Limitrows > 0)
            {
                if (censorStr != "")
                    query += $" where rownum<={Limitrows} and censor_state='{censorStr}'";
                else
                    query += $" where rownum<={Limitrows}";
            }
            if ((Orderby) != null)
                query += $" order by {Orderby} desc";
            return DBHelper.ShowInfo(query);
        }
        public static void UpdateFosterEntry(string UID, string PID,DateTime date,string censor_status)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"UPDATE foster SET censor_state='{censor_status}'" +
                    $" where fosterer={UID} and pet_id={PID} and start_year={date.Year}" +
                    $" and start_month={date.Month} and start_day={date.Day}";
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"用户:{UID}对宠物{PID}的寄养申请通过状态为{censor_status}");
                }
                catch (OracleException ex)
                {
                    Console.WriteLine("错误码" + ex.ErrorCode.ToString());

                    throw;
                }
                connection.Close();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Petname">宠物名</param>
        /// <param name="size">宠物类型进一步细分（eg：大中小型犬）</param>
        /// <param name="dateTime">date1:寄养开始时间</param>
        /// <param name="duration"></param>
        /// <param name="remark"></param>
        /// <returns>错误码</returns>
        public static int InsertFoster(string UID,string PID,DateTime dateTime,int duration,string remark)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = $"INSERT INTO foster (fosterer,pet_id,start_year,start_month,start_day,duration,remark) " +
                        $"VALUES ('{UID}','{PID}',{dateTime.Year},{dateTime.Month},{dateTime.Day},'{duration}','{remark}')";
                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"{UID}给{PID}在{DateTime.Now}抚养宠物");
                        return 0;
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("不存在的用户或宠物");
                        connection.Close();
                        return 2;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.ToString());
            }
            return 3;
        }
        /// <summary>
        /// 判断对于同一个人foster同一个宠物，那么是否会出现时间区间重叠
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        /// <param name="dateTime"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static bool NoOverlapping(string UID, string PID, DateTime dateTime, int duration)
        {
            bool con = false;
            //User user1 = new User();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT * FROM foster WHERE pet_id = {PID} AND fosterer = {UID}" +
                    $" AND ( (start_year < {dateTime.Year} AND start_year + duration >= start_year) OR" +
                    $" (start_year = {dateTime.Year} AND start_month < {dateTime.Month} AND start_month + duration >= {dateTime.Month}) OR " +
                    $" (start_year = {dateTime.Year} AND start_month = {dateTime.Month} AND start_day <= {dateTime.Day} AND start_day + duration >= {dateTime.Day}))";
                command.Parameters.Clear();
                try
                {
                    OracleDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        connection.Close();
                        return true;
                        // 执行你的逻辑操作，例如将数据存储到自定义对象中或进行其他处理
                    }
                    connection.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public static bool GetPendingUser(string UID, string PID, DateTime date)
        {
            bool exist = false;
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"select count(*) from foster where User_ID='{UID}' and fosterer='{PID}' " +
                    $" and start_year={date.Year} and start_month={date.Month} and start_day={date.Day} and " +
                    $"(censor_state='at capacity' or censor_state='invalid')";
                try
                {
                    exist=Convert.ToBoolean(command.ExecuteScalar());
                    connection.Close();
                    return exist;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                    return false;
                }
                
            }
        }

        public static DataTable GetFosterPets(string user_id)
        {
            string query = "select foster.pet_id,pet_name," +
                "to_char(foster.start_year)||'-'||to_char(foster.start_month)||'-'" +
                "||to_char(foster.start_day) as start_date" +
                $",duration,case when species='dog' and psize='small' then duration*20" +
                $"when species='dog' and psize='medium' then duration*25" +
                $"when species='dog' and psize='large' then duration*30" +
                $"when species='cat' then duration*20" +
                $"else duration*100 end as expense" +
                $",CASE WHEN CENSOR_STATE = 'invalid' THEN '无效' WHEN CENSOR_STATE = 'legitimate' " +
                $"THEN '通过' WHEN CENSOR_STATE = 'to be censored' THEN '待审核' WHEN CENSOR_STATE = 'aborted' " +
                $"then '未通过' WHEN CENSOR_STATE = 'outdated' then '过期' end as censor_state " +
                $"from foster left join pet on pet.pet_id=foster.pet_id " +
                $"where fosterer={user_id}";
            return DBHelper.ShowInfo(query);
        }
    }

}
