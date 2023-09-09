using Oracle.ManagedDataAccess.Client;
using PetFoster.DAL;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PetFoster.Model.PetData;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Data;
using Glue.PetFoster.BLL;
using System.Reflection.PortableExecutable;

namespace PetFoster.BLL
{
    public class PetManager
    {
        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        private static string conStr = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";"; // 替换为实际的数据库连接字符串

        public static string GetHealth(string health)
        {
            string result;
            if (JsonHelper.TranslateToCn(health, "health_state") != null)
            {
                return health;
            }
            if((result = JsonHelper.TranslateToEn(health,"health_state")) != null)
            {
                return result;
            }
            return null;
        }
        public static DataTable ShowPetProfile(int Limitrow = -1, string Orderby = null)
        {
            DataTable dt = DAL.PetServer.PetInfo(Limitrow, Orderby);
            //调试用
            
            return dt;
        }
        public static DataTable ShowPetNames(int Limitrow = -1, string Orderby = null)
        {
            DataTable dt = DAL.PetServer.SelectPetInfo(Limitrow, Orderby);
            //调试用
            Console.WriteLine("开始选择可治疗宠物！");
            return dt;
        }
        //人气榜
        public static DataTable ShowBoards(int Limitrow = -1, string Orderby = null)
        {
            DataTable dt = PetServer.PetInfoForAdmin(Limitrow, Orderby);
            //调试用
            Console.WriteLine("开始选择人气榜！");
            return dt;
        }
        public static bool ViewProfile(int PetID, out Pet Candidate)
        {
            string PID = PetID.ToString();
            bool con = false;
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                // 在此块中执行数据操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                Candidate = DAL.PetServer.SelectPet(PID);
                DAL.PetServer.ReadPet(PID);
                if (Candidate.Pet_ID == "-1")
                    Console.WriteLine("PID不存在！");
                else
                    Console.WriteLine($"宠物叫做{Candidate.Pet_Name}\n,品种是{Candidate.Species}\n,年龄{Candidate.birthdate}\n!");
                connection.Close();
            }

            return con;
        }
        public static void RegisterPet(string Petname, string Breed, string Psize,int Age, string Path = null, string Health_State = "充满活力", bool HaveVaccinated = false, string gender = "男")
        {
            DateTime birthDate = DateTime.Now.AddYears(-Age);
            // 读取图像文件
            byte[] BinImage = null ;
            if (Path!=null)
                BinImage = DAL.PetServer.ConvertImageToByteArray(Path);

            DAL.PetServer.InsertPet(Petname, Breed, Psize, birthDate, BinImage, Health_State, HaveVaccinated,sex:gender);

        }
        public static void RegisterPet(string Petname, string Breed, string Psize,DateTime birthDate, string Path = null, string Health_State = "充满活力", bool HaveVaccinated = false,string gender="男")
        {
            // 读取图像文件
            byte[] BinImage = DAL.PetServer.ConvertImageToByteArray(Path);

            DAL.PetServer.InsertPet(Petname,Breed,Psize,birthDate,BinImage,Health_State,HaveVaccinated,sex:gender);

        }
        public static void UpdatePet(string PID, string Petname, string Health_State, bool Vaccine)
        {
            DAL.PetServer.UpdatePet(PID, Petname, Health_State, Vaccine);
        }
        public static void UpdatePet(string PID, string Petname, string Health_State, bool Vaccine,string avatar="")
        {
            DAL.PetServer.UpdatePet(PID, Petname, Health_State, Vaccine,avatar);
        }
    }
}
