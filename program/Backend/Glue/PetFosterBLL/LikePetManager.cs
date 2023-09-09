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
    public class LikePetManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Limitrow"></param>
        /// <param name="Orderby"></param>
        public static DataTable ShowLikePet(int Limitrow = -1, string Orderby = null)
        {
            DataTable dt = LikePetServer.LikePetInfo(Limitrow, Orderby);
            //调试用
            Util.DebugTable(dt);
            return dt;
        }
        public static bool HaveUserLiked(string UID, string PID)
        {
            return LikePetServer.GetLikePetEntry(UID, PID);
        }
        public static void GiveALike(string UID, string PID, bool is_give)
        {
            bool dt = LikePetServer.GetLikePetEntry(UID, PID);
            //调试用
            if (!dt)
            {
                LikePetServer.InsertLikePet(UID, PID);
                Console.WriteLine($"{UID} gives a like to {PID}."); // 输出点赞信息
            }
            else if (dt)
            {
                LikePetServer.DeleteLikePet(UID, PID);
                Console.WriteLine($"{UID} undo a like to {PID}."); // 输出点赞信息
            }
            else
            {
                throw new Exception("无效操作");
            }
        }
        public static int GetLikeNums(string PID)
        {
            return LikePetServer._GetLikeNums(PID);
        }
        //取消点赞，
        public static void UndoALike(string UID, string PID)
        {
            LikePetServer.DeleteLikePet(UID, PID);
        }
    }
}
