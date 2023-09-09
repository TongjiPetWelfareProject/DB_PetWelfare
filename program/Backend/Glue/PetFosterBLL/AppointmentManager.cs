using Oracle.ManagedDataAccess.Client;
using PetFoster.DAL;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.BLL
{
    public class AppointmentManager
    {
        public static DataTable ShowApplies(string UID = null, string PID = null, string Categories = null)
        {
            DataTable dt = AppointmentServer.ApplyInfo(UID, PID, Categories);
            //调试用
            return dt;
        }
        //未注册宠物预约
        public static void Appointment(string UID, string Pname, string species, string VID, DateTime dt, string reason)
        {
            string PID = PetServer.InsertPet(Pname, "dog", "medium", DateTime.Now);
            _Appointment(UID,PID,species,VID,dt,reason);
        }
        //已注册宠物预约DAL.PetServer.SelectPetInfo列举宠物ID与宠物名
        public static void Appointment2(string UID,string PID,string VID, DateTime dt, string reason)
        {
            string species=PetServer.GetSpecies(PID);
            _Appointment(UID, PID, species, VID, dt, reason);
        }
        //展示预约时段有空的医生，医生经过预筛选后每天之能看8只宠物
        private static void _Appointment(string UID, string PID, string species, string VID, DateTime dt, string reason)
        {
            
            try
            {
                string hstatus = PetServer.GetHealthStatus(PID);
                //0.让宠物先生病
                if (hstatus != "Critical" || hstatus != "Unhealthy" || hstatus != "Critical")
                    PetServer.UpdateStatus(PID,"Unhealthy");
                //1.预约时间不能是周六周日
                if (DayOfWeek.Saturday == dt.DayOfWeek || DayOfWeek.Sunday == dt.DayOfWeek)
                {
                    throw new Exception("医生周末休息，请选择工作日预约！");
                }
                //2.预约时间在一周内
                if (dt.Subtract(DateTime.Now)>=TimeSpan.FromDays(7)|| dt.Subtract(DateTime.Now) <= TimeSpan.FromDays(0))
                {
                    throw new Exception("请在一周内预约,并且在今天后预约！");
                }
                //3.挂号
                AppointmentServer.InsertAppointment(UID, PID, VID, dt, reason);
            }catch (Exception ex){
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public static DataTable GetUserAppointment(string UID)
        {
            DataTable dt = new DataTable();
            dt = AppointmentServer.GetUserAppointment(UID);
            return dt;
        }

        public static void DoneTreatment(int pid, int vid, DateTime value)
        {
            AppointmentServer.InsertTreatTime(pid, vid, value);
        }
    }
}