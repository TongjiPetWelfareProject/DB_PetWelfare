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
    public class FosterManager
    {
        /// <summary>
        /// 展示未审核/已审核/未通过界面
        /// </summary>
        /// <param name="censorstate">0为未审核，1为未通过审核，2为通过审核</param>
        /// <param name="Limitrow"></param>
        /// <param name="Orderby"></param>
        public static DataTable CensorFoster(out string censorStr,int censorstate=0,int Limitrow = -1, string Orderby = null,bool verbose=false)
        {
            censorStr=JsonHelper.GetErrorMessage("censor_state",censorstate);

            DataTable dt = FosterServer.FosterInfo("",Limitrow, Orderby,verbose);
            //调试用
            Console.WriteLine("显示寄养申请列表");
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UID">用户登录后获得潜在UID</param>
        /// <param name="Petname">宠物名</param>
        /// <param name="Breed">品种</param>
        /// <param name="size">大小</param>
        /// <param name="dateTime">日期</param>
        /// <param name="duration">寄养时长</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public static int ApplyFoster(string UID, string Petname,string Breed,string size, DateTime dateTime, int duration, string remark)
        {
            //0.该宠物不存在，则需要注册
            string PID=PetFoster.DAL.PetServer.InsertPet(Petname, Breed, size, DateTime.MinValue);
            //1. 判断foster项是否符合要求
            if(!FosterServer.NoOverlapping(UID, PID, dateTime, duration))
                FosterServer.InsertFoster(UID, PID, dateTime, duration, remark);//申请抚养初步通过
            else
            {
                    Console.WriteLine($"{UID}已经在{dateTime.Date}~{dateTime.AddDays(duration).Date}时间段寄养foster该宠物{PID}");
                    return 1;
            }
            return _ResumeApplyFoster(UID, Convert.ToInt32(PID), dateTime);
        }
        /// <summary>
        /// 旧有宠物的申请
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        /// <param name="Breed"></param>
        /// <param name="size"></param>
        /// <param name="dateTime"></param>
        /// <param name="duration"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static int ApplyFoster(string UID, int PID, string Breed, string size, DateTime dateTime, int duration, string remark)
        {
            //0.该宠物不存在，则需要注册
            Model.Pet test=PetFoster.DAL.PetServer.SelectPet(PID.ToString());
            if (test.Pet_ID == "-1")
            {
                Console.WriteLine($"PID为{PID}的宠物不存在");
                return 2;
            }
            //1. 判断foster项是否符合要求
            if (!FosterServer.NoOverlapping(UID, PID.ToString(), dateTime, duration))
                FosterServer.InsertFoster(UID, PID.ToString(), dateTime, duration, remark);//申请抚养初步通过
            else
            {
                Console.WriteLine($"{UID}已经在{dateTime.Date}~{dateTime.AddDays(duration).Date}时间段寄养foster该宠物{PID}");
                return 1;
            }
            return _ResumeApplyFoster(UID, PID, dateTime);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="UID">用户ID</param>
        /// <param name="PID">宠物ID</param>
        /// <param name="date"></param>
        /// <param name="censorcode">1表示审核未通过，2表示通过，3表示过期，4表示无效（没有房间），默认为未审核</param>
        public static void Censorship(string UID, int PID, DateTime date,int censorcode=0)
        {
            string msg = JsonHelper.GetErrorMessage("censor_state", censorcode);
            FosterServer.UpdateFosterEntry(UID, PID.ToString(), date,msg);
        }
        public static void Censorship(string UID, string PID, DateTime date, string censor_state)
        {
            //string msg = JsonHelper.GetErrorMessage("censor_state", censorcode);
            FosterServer.UpdateFosterEntry(UID, PID.ToString(), date, censor_state);
        }
        public static int ReapplyFoster(string UID, int PID, DateTime dateTime)
        {
            //0. 是否存在该条目（必须为invalid或at capacity）
            bool exist = FosterServer.GetPendingUser(UID,PID.ToString(), dateTime);
            if (!exist)
            {
                Console.WriteLine($"用户{UID}为宠物{PID}在{dateTime}时没有申请寄养或申请已被驳回！");
                return 5;
            }
            //1. 准备重新分配房源
            FosterServer.UpdateFosterEntry(UID, PID.ToString(), dateTime, "to be censored");
            //2. 分配房源
            return _ResumeApplyFoster(UID, PID, dateTime);
        }
        /// <summary>
        /// 本函数有两个用途，一是供ApplyFoster分配房间使用，
        /// 另一个就是为故障的或因房源不足而暂缓的申请记录恢复分配房间
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PID"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static int _ResumeApplyFoster(string UID,int PID,DateTime dateTime)
        {
            try
            {
                short storey, compartment;
                RoomServer.AllocateRoom(out storey, out compartment);
                if (storey == -1 && compartment == -1)
                {
                    Censorship(UID, PID, dateTime, 5);
                    return 3;//房间分配已满
                }
                RoomServer.OccupyRoom(storey, compartment,"N");
                //4. 在accommodate中登记入住
                AccommodateServer.InsertAccommodate(UID, PID.ToString(), storey, compartment);
            }
            catch (Exception e)
            {
                Censorship(UID, PID, dateTime, 4);
                Console.WriteLine($"错误消息：{e.Message},系统故障");
                return 4;
            }
            return 0;
        }

    }   
}
