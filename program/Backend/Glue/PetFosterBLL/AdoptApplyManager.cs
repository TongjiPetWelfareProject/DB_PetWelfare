using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFoster.Model;
using PetFoster.DAL;
using Glue.PetFoster.BLL;

namespace PetFoster.BLL
{
    public class AdoptApplyManager
    {
        public static DataTable ShowCensorAdopt(int Limitrow = -1, string Orderby = null)
        {

            DataTable dt = AdoptApplyServer.AdoptInfo(Limitrow, Orderby);
            //调试用
            Console.WriteLine("显示收养请求列表");
            return dt;
        }
        public static bool ApplyAbuseOrNot(string UID,string PID)
        {
            //1. 一个人不能重复申请领养
            try
            {
                string query1 = $"select count(*) from adopt_apply where adopter_id={UID} and pet_id={PID} " +
                    $"where censor_state='to be censored'";
                bool repeat = DBHelper.GetScalarInt(query1) > 0;
                //2. 一只宠物最多能被10个人申请领养
                string query2 = $"select count(*) from adopt_apply where pet_id={PID} where" +
                    $" censor_state='legitimate' or censor_state='to be censored'";
                bool atcapacity = DBHelper.GetScalarInt(query1) > 10;
                if (repeat)
                    throw new Exception("请不要重复申请领养宠物");
                if (atcapacity)
                    throw new Exception("当前宠物申请领养人数过多!");
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
                return true;
            }
            return false;
        }
        public static void ApplyAdopt(string UID, string PID, bool gender, bool pet_exp, bool long_term_care,
            bool w_to_treat, decimal d_care_h, string P_Caregiver, decimal f_popul, bool be_children, bool accept_vis)
        {
            //1.获取宠物的健康状况
            try
            {
                string status = PetServer.GetHealthStatus(PID.ToString());
                if (status == "Critical" || status == "Sicky" || status == "Unhealthy")
                {
                    throw new Exception("宠物健康情况危急，不能领养");
                }

                AdoptApplyServer.InsertAdoptApply(UID, PID, gender, pet_exp, long_term_care, w_to_treat,
                d_care_h, P_Caregiver, f_popul, be_children, accept_vis);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        //审核
        public static int CensorAdopt(string UID, int pid, DateTime dt, bool pass, out int errcode)
        {
            if (pid == -1)
            {
                Console.WriteLine($"宠物已经全被领养或寄养（正在申请寄养）走了!");
                errcode = 1;//如上
                return -1;
            }
            else if (pid == -2)
            {
                Console.WriteLine($"审核过程异常!");
                errcode = 2;
                return -1;
            }
            if (pass)
            {
                //2.将此宠物交给主人领养
                int err = 0;
                AdoptServer.InsertAdopt(UID, pid.ToString(), dt, out err);
                if (err == -1)
                {
                    Console.WriteLine($"不存在该申请人!");
                    errcode = 3;
                    return -1;
                }
                //3.审核状态为通过
                AdoptApplyServer.UpdateAdoptEntry(UID, pid.ToString(), "legitimate");
                //4.其他申请人被淘汰
                AdoptApplyServer.UpdateAdoptEntries(UID, pid.ToString(), "aborted");
                
            }
            else
            {
                // 审核拒绝
                AdoptApplyServer.UpdateAdoptEntry(UID, pid.ToString(), "aborted");
            }
            errcode = 0;//正常运行
            return pid;
        }
        public static Pet2 RetrievePet(int PID)
        {
            Pet2 pet = new Pet2();
            pet = AdoptApplyServer.SelectPet(PID.ToString());
            return pet;
        }
    }
}