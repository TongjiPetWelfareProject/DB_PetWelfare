using Oracle.ManagedDataAccess.Client;
using PetFoster.DAL;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.BLL
{
    public class AppointmentManager
    {
        public static void ShowApplies(string UID = null, string PID = null, string Categories = null)
        {
            DataTable dt = AppointmentServer.ApplyInfo(UID, PID, Categories);
            //调试用
            foreach (DataColumn column in dt.Columns)
            {
                Console.Write("{0,-20}", column.ColumnName);
            }
            Console.WriteLine();

            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write("{0,-20}", item);
                }
                Console.WriteLine();
            }
        }
        public void Appointment(string UID, string Pname, string species, DateTime dt, string reason)
        {
            //1.注册宠物信息
            string PID = PetFoster.DAL.PetServer.InsertPet(Pname, species, "medium", DateTime.MinValue);
            //2.挂号
            AppointmentServer.InsertAppointment(UID, PID, dt, reason);
        }
    }
}
