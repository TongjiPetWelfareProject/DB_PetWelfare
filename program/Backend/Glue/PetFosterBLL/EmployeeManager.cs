using Glue.PetFoster.BLL;
using OracleInternal.Secure.Network;
using PetFoster.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.BLL
{
    public class EmployeeManager
    {
        public static DataTable ShowProfile(int Limitrow = -1, string Orderby = null)
        {
            DataTable dt = EmployeeServer.EmployeeInfo(Limitrow, Orderby);
            //调试用
            Console.WriteLine("开始显示员工列表");
            return dt;
        }
        public static void UpdateEmployee(string EID,string employeename, double Salary, string PhoneNumber, string Duty, double hours)
        {
            EmployeeServer.UpdateEmployee(EID,employeename, Salary, JsonHelper.TranslateToEn(Duty, "duties"), 8,0,Convert.ToInt32(8+hours), Convert.ToInt32(60 *(hours-Math.Floor(hours))));
            UserServer.UpdateUser(EID, employeename, UserManager.ComputeSHA256Hash("Password1!"),PhoneNumber);
        }
        public static void RecruitEmployee(string employeename, decimal Salary, string PhoneNumber, string Duty, double hours)
        {
            string EID = UserServer.InsertUser(employeename, UserManager.ComputeSHA256Hash("Password1!"), PhoneNumber,Role:"Admin");
            EmployeeServer.InsertEmpolyee(EID,employeename, Salary, JsonHelper.TranslateToEn(Duty,"duties"),hours);
            
        }
    }
}
