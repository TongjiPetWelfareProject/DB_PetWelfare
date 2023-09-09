using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using PetFoster.BLL;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetFoster.DAL
{
    public class PetServer
    {
        public static string conStr = AccommodateServer.conStr;
        private static byte[] GetRandomAvatar(string imagePath)
        {
            // 获取随机的 avatar
            // ...
            byte[] imageBytes = ConvertImageToByteArray(imagePath);
            return imageBytes;
        }


        public static byte[] ConvertImageToByteArray(string imagePath)
        {
            byte[] imageData;

            using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    imageData = binaryReader.ReadBytes((int)fileStream.Length);
                }
            }

            return imageData;
        }
        public static DataTable PetInfo(decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT pet_profile.pet_id,pet_name,species,sex,psize ," +
                "TRUNC(MONTHS_BETWEEN(CURRENT_TIMESTAMP, birthdate) / 12) AS age," +
                "health_state,vaccine,popularity,status,avatar FROM pet_profile " +
                "left join pet_source on pet_source.pet_id=pet_profile.pet_id";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        //管理员端——人气榜
        public static DataTable PetInfoForAdmin(decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT distinct pet.pet_id,pet.pet_name,pet.avatar, pet_profile.read_num,pet_profile.like_num,popularity FROM pet left join pet_profile" +
                " on pet_profile.pet_id=pet.pet_id order by popularity desc";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        /// <summary>
        /// 查看宠物信息或宠物是否存在
        /// </summary>
        /// <param name="user">用户行</param>
        /// <returns></returns>
        public static Pet SelectPet(string PID)
        {
            bool con = false;
            Pet pet = new Pet();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"select *from pet where Pet_ID={PID}";
                try
                {
                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // 访问每一行的数据
                        // 其他列..
                        pet.Pet_ID = reader["Pet_ID"].ToString();
                        pet.Pet_Name = reader["Pet_Name"].ToString();
                        pet.Species = reader["Species"].ToString();
                        pet.birthdate = Convert.ToDateTime(reader["BIRTHDATE"]);
                        pet.Avatar = reader["Avatar"].ToString();
                        pet.Read_Num = Convert.ToDecimal(reader["Read_Num"]);
                        pet.Like_Num = Convert.ToDecimal(reader["Like_Num"]);
                        pet.Collect_Num = Convert.ToDecimal(reader["Collect_Num"]);
                        // 执行你的逻辑操作，例如将数据存储到自定义对象中或进行其他处理

                    }
                    if (pet.Pet_ID == "-1")
                        throw new Exception("不存在的宠物！");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                connection.Close();
            }

            return pet;
        }
        public static void ReadPet(string PID)
        {
            UpdateAddr(PID, "read_num=read_num+1");
        }
        private static void UpdateAddr(string PID,string kv)
        {
            // 更改信息
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE pet SET "+kv+ $" where pet_id='{PID}'";
                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"宠物{PID}的阅读量+1!");
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
        public static void UpdateStatus(string PID,string hstatus)
        {
            // 更改信息
            UpdateAddr(PID,$" health_state='{hstatus}' ");
        }
        public static byte[] SerializeObject(object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                return stream.ToArray();
            }
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
        public static DataTable SelectPetInfo(decimal Limitrows = -1, string Orderby = null)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT pet_id,pet_name,species FROM pet where" +
                " pet_id not in (select pet_id from foster)" +
                " and pet_id not in (select pet_id from adopt)";
            return DBHelper.ShowInfo(query, Limitrows, Orderby);
        }
        private static string GetAttr(string PID,string attribute)
        {
            bool con = false;
            User user1 = new User();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"select {attribute} from pet where Pet_ID=:pet_id";
                command.Parameters.Clear();
                command.Parameters.Add("pet_id", OracleDbType.Varchar2, PID, ParameterDirection.Input);
                try
                {
                    string petname = command.ExecuteScalar() as string;
                    return petname;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "Error";
                }
            }
        }
        public static string GetName(string PID)
        {
            return GetAttr(PID, "Pet_Name");
        }
        public static string GetSpecies(string PID)
        {
            string species=GetAttr(PID, "Species") ;
            const string defaultSpecies = "dog";
            return species == null ? defaultSpecies : species;
        }
        public static string GetHealthStatus(string PID)
        {
            return GetAttr(PID, "Health_State");
        }
        public static void UpdatePet(string PID, string Petname, string Health_State, bool Vaccine)
        {
            Health_State = JsonHelper.TranslateToEn(Health_State, "health_state");
            string vaccine = "";
            if (Vaccine)
                vaccine = Vaccine == true ? "Y" : "N";
            // 更改信息
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE pet SET pet_name=:pname,health_state=:hs," +
                        " vaccine=:vc" + $" where pet_id='{PID}'";
                    command.Parameters.Add("pname", OracleDbType.Varchar2, Petname, ParameterDirection.Input);
                    command.Parameters.Add("hs", OracleDbType.Varchar2, Health_State, ParameterDirection.Input);
                    command.Parameters.Add("vc", OracleDbType.Varchar2, vaccine, ParameterDirection.Input);
                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"宠物{PID}的阅读量+1!");
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
        public static void UpdatePet(string PID, string Petname, string Health_State, bool Vaccine,string avatar="")
        {
            Health_State = JsonHelper.TranslateToEn(Health_State, "health_state");
            string vaccine = "";
            if (Vaccine)
                vaccine = Vaccine == true ? "Y" : "N";
            // 更改信息
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE pet SET pet_name=:pname,health_state=:hs," +
                        " vaccine=:vc,avatar=:img" + $" where pet_id='{PID}'";
                    command.Parameters.Add("pname", OracleDbType.Varchar2, Petname, ParameterDirection.Input);
                    command.Parameters.Add("hs", OracleDbType.Varchar2, Health_State, ParameterDirection.Input);
                    command.Parameters.Add("vc", OracleDbType.Varchar2, vaccine, ParameterDirection.Input);
                    command.Parameters.Add("img", OracleDbType.Varchar2, avatar, ParameterDirection.Input);
                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"宠物{PID}的阅读量+1!");
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
        /// 插入宠物信息
        /// </summary>
        /// <param name="Petname">宠物名</param>
        /// <param name="Breed">宠物品种，必须符合JSON中列出的几种类别</param>
        /// <param name="birthDate">出生日期，由于年龄为派生类，不好维护，因此改用出生日期</param>
        /// <param name="Avatar">图像，用BLOB存储</param>
        /// <param name="Health_State">健康状态</param>
        /// <param name="HaveVaccinated">是否接种疫苗</param>
        public static string InsertPet(string Petname, string Breed ,string Psize,DateTime birthDate,byte[]Avatar=null, string Health_State= "Vibrant", bool HaveVaccinated=false,string sex="M")
        {
            string vaccine = "";
            vaccine = (HaveVaccinated==true ? "Y" : "N");
            Breed = JsonHelper.TranslateToEn(Breed, "species");
            if (Breed == "dog")
            {
                if (Psize != "small" && Psize != "medium" && Psize != "large")
                    Psize = JsonHelper.TranslateToEn(Psize, "size");
            }
            else
                Psize = "small";
            if (sex == "男") 
                sex = "M";
            else if (sex == "女") 
                sex = "F";
            // 添加新行
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO pet (pet_id,pet_name, species, birthdate, avatar, health_state, vaccine) " +
                        "VALUES (pet_id_seq.NEXTVAL,:pet_name,:species, :birthdate, :avatar, :health_state, :vaccine)"
                        ;
                    command.Parameters.Clear();
                    command.Parameters.Add("pet_name", OracleDbType.Varchar2, Petname, ParameterDirection.Input);
                    command.Parameters.Add("species", OracleDbType.Varchar2, Breed, ParameterDirection.Input);
                    command.Parameters.Add("birthdate", OracleDbType.Date, birthDate, ParameterDirection.Input);
                    command.Parameters.Add("avatar", OracleDbType.Varchar2, ParameterDirection.Input);
                    command.Parameters.Add("health_state", OracleDbType.Varchar2, Health_State, ParameterDirection.Input);
                    command.Parameters.Add("vaccine", OracleDbType.Varchar2, vaccine, ParameterDirection.Input);
                    //command.Parameters.Add("new_pet_id", OracleDbType.Int32, ParameterDirection.Output);
                    try
                    {
                        command.ExecuteNonQuery();
                        command.CommandText = "SELECT pet_id_seq.CURRVAL FROM DUAL";
                        int currentPetId = Convert.ToInt32(command.ExecuteScalar());
                        string newPetId = currentPetId.ToString();
                        UpdateAddr(newPetId, $"psize='{Psize}',sex='{sex}'");
                        return newPetId;
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("错误码" + ex.ErrorCode.ToString());
                        return "-1";
                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.ToString());
            }
            return "-1";
        }
    }
}
