using Oracle.ManagedDataAccess.Client;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace PetFoster.DAL
{
    public class RoomServer
    {
        public static string conStr = AccommodateServer.conStr;
        /// <summary>
        /// 查看每层楼的房源信息，由ShowAvailable(DataTable dt)调用
        /// </summary>
        /// <param name="Limitrows">最多显示的行数</param>
        /// <param name="Orderby">排序的依据（降序）</param>
        /// <returns>返回数据表</returns>
        public static DataTable StoreyInfo()
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM room_avaiable ";
            return DBHelper.ShowInfo(query);
        }
        /// <summary>
        /// 查看房间信息，由ShowProfiles(DataTable dt)调用
        /// </summary>
        /// <param name="Limitrows">最多显示的行数</param>
        /// <param name="Orderby">排序的依据（降序）</param>
        /// <returns>返回数据表</returns>
        public static DataTable RoomInfo(decimal Limitrows = -1, string Orderby = null,bool OnlyAvailable=false)
        {
            string query = "SELECT compartment,room_status,storey,cleaning_time FROM room ";
            if (Limitrows > 0)
                query += $" where rownum<={Limitrows} ";
            else if (OnlyAvailable)
                query += $"where room_status='N'";
            if (Limitrows > 0 && OnlyAvailable)
                query += "and room_status='N' ";
                query += $" order by storey*30+compartment asc";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        /// <summary>
        /// 更改房间信息，由征用/归还房间函数RentARoom(int requiredsize)和打扫房间调用，需要满足
        /// 房间大小大于某一大小
        /// </summary>
        /// <param name="room_status">房间是否被占用</param>
        /// <param name="storey">楼层数</param>
        /// <param name="compartment">房间号</param>
        /// <param name="HaveCleaned">true为打扫，false为退房/租房</param>
        public static void UpdateRoom(short storey, short compartment, bool HaveCleaned=false,string room_status=null)
        {
            // 更改信息
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    
                    if (HaveCleaned)
                    {
                        command.CommandText = "UPDATE room SET cleaning_time=TO_CHAR(:Cleaning_Time,'YYYY-MM-DD HH24:MI:SS') " +
                        "where compartment=:compartment and storey=:storey";
                        command.Parameters.Clear();
                        command.Parameters.Add("Cleaning_Time", OracleDbType.TimeStamp, DateTime.Now, ParameterDirection.Input);
                        command.Parameters.Add("compartment", OracleDbType.Int32, compartment, ParameterDirection.Input);
                        command.Parameters.Add("storey", OracleDbType.Int32, storey, ParameterDirection.Input);
                    }
                    else
                    {
                        command.Parameters.Clear();
                        command.CommandText = "UPDATE room SET room_status='Y' " +
                        $"where compartment={compartment} and storey={storey}";
                        
                    }
                    try
                    {
                        PetData petData = new PetData();
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
        /// <summary>
        /// 随机分配空闲的房间
        /// </summary>
        /// <param name="storey"></param>
        /// <param name="compartment"></param>
        public static void AllocateRoom(out short storey,out short compartment)
        {
            bool con = false;
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                Random random = new Random();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from room where room_status='Y'"; 
                try
                {
                    OracleDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        double randomDouble = random.NextDouble();
                        storey = reader.GetInt16(2);
                        compartment = reader.GetInt16(3);
                        if (randomDouble > 0.5)
                            continue;
                        return;

                    }
                    storey = -1;
                    compartment = -1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                connection.Close();
            }
        }

        internal static int GetPet(short storey, short compartment)
        {
            string query = "select pet_id from accommodate where" +
                $" storey={storey} and compartment={compartment}";
            return DBHelper.GetScalarInt(query);
        }

        internal static void OccupyRoom(short storey, short compartment, string v)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE room SET room_status=:occupied " +
                        "where compartment=:compartment and storey=:storey";
                        command.Parameters.Clear();
                    command.Parameters.Add("occupied", OracleDbType.Varchar2, v, ParameterDirection.Input);
                    command.Parameters.Add("compartment", OracleDbType.Int32, compartment, ParameterDirection.Input);
                    command.Parameters.Add("storey", OracleDbType.Int32, storey, ParameterDirection.Input);
                    try
                    {
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("错误码" + ex.ErrorCode.ToString());
                        connection.Close();
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
