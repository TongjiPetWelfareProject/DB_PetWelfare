using Glue.PetFoster.BLL;
using PetFoster.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.BLL
{
    public class VetManager
    {
        public static DataTable ShowVetProfile(int Limitrow=-1,string Orderby=null)
        {
            DataTable dt=VetServer.VetInfo(Limitrow,Orderby);
            //调试用
            Console.WriteLine("开始显示管理员端的兽医管理界面");
            return dt;  //交给连接层转换并返回给前端
        }
        public static DataTable ShowVetProfileForUser(int Limitrow = -1, string Orderby = null)
        {
            DataTable dt = VetServer.VetInfoForApmt(Limitrow, Orderby);
            //调试用
            Util.DebugTable(dt);
            return dt;  //交给连接层转换并返回给前端
        }
        //获取医生照片
        public static byte[] GetPic(string VID) {
            byte[] img=VetServer.GetPortrait(VID).portrait;
            return img;
        }
        
        public static void UpdateVet(string VID,string vetname, double Salary, string PhoneNumber, double hours)
        {
            VetServer.UpdateVet(VID,vetname, Salary, PhoneNumber, 8, 0, Convert.ToInt32(8 + hours), Convert.ToInt32(60 * (hours - Math.Floor(hours))));
        }
        public static void RecruitVet(string vetname, decimal Salary, string PhoneNumber, double hours)
        {
            VetServer.InsertVet(vetname, Salary, PhoneNumber, 8, 0, Convert.ToInt32(8 + hours), Convert.ToInt32(60 * (hours - Math.Floor(hours))));
        }
    }
}
