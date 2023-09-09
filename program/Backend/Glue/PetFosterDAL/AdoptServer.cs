using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.DAL
{
    public class AdoptServer
    {
        public static string conStr = AccommodateServer.conStr;
        public static DataTable PetInfo(decimal Limitrows = -1, string Orderby = null)
        {
            string query = "SELECT * FROM pet_profile" +
                " where pet_id not in (select pet_id from foster)" +
                " and pet_id not in (select pet_id from appointment)";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        public static DataTable PetInfoForUser(decimal Limitrows = -1, string Orderby = null)
        {
            string query = "SELECT * FROM pet_profile" +
                " left join pet_source on pet_source.pet_id=pet_profile.pet_id" +
                " where status='Wander' and (health_state='Decent' or" +
                " health_state='Healthy' or health_state='Well'" +
                " or health_state='Vibrant')";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        public static void InsertAdopt(string UID, string PID, DateTime dt, out int errcode)
        {
            // 添加新行
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO adopt (adopter_id, pet_id,adoption_time)" +
                    $"VALUES ({UID},{PID},:dt)";
                    //延期了
                    if(DateTime.Now.Subtract(dt).TotalDays<0)
                        command.Parameters.Add("dt", OracleDbType.Date, DateTime.Now, ParameterDirection.Input);
                    else
                        command.Parameters.Add("dt", OracleDbType.Date, dt, ParameterDirection.Input);
                    try
                    {
                        command.ExecuteNonQuery();
                        errcode = 0;
                        Console.WriteLine($"{UID}申请成功，被分配到的宠物是{PID}");
                    }
                    catch (OracleException ex)
                    {
                        errcode = -1;
                        Console.WriteLine("不存在的用户或宠物");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                errcode = -2;
                Console.WriteLine(ex.ToString());
            }
        }
    }
}