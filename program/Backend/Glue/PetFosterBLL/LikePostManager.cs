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
    public class LikePostManager
    {
        public static void ShowLikePost(int Limitrow = -1, string Orderby = null)
        {
            DataTable dt = LikePostServer.LikePostInfo(Limitrow, Orderby);
            //调试用
            Util.DebugTable(dt);
        }
        
        public static void GiveALike(string UID, string PID)
        {
            bool dt = LikePostServer.GetLikePostEntry(UID, PID);
            //调试用
            if (!dt)
            {
                LikePostServer.InsertLikePost(UID, PID);
                Console.WriteLine($"{UID} gives a like to {PID}."); // 输出点赞信息
            }
            else
            {
                LikePostServer.DeleteLikePost(UID, PID);
                Console.WriteLine($"{UID} undo a like to {PID}."); // 输出点赞信息
            }
        }
        public static int IfLike(string UID, string PID)
        {
            bool dt = LikePostServer.GetLikePostEntry(UID, PID);
            if (!dt)
            {
                return -1;//未点赞
            }
            else
            {
                return 1;//已点赞
            }
        }
    }
}
