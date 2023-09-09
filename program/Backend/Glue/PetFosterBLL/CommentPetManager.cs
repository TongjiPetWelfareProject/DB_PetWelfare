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
    public class CommentPetManager
    {
        public static DataTable ShowCommentPet(int Limitrow = -1, string Orderby = null, string UID = "-1", string PID = "-1")
        {
            DataTable dt = CommentPetServer.CommentPetInfo(Limitrow, Orderby, UID, PID);
            //调试用
            Console.WriteLine("开始显示宠物的评论信息");
            return dt;
        }
        public static void GiveAComment(string UID, string PID,string content)
        {
            //调试用
            CommentPetServer.InsertCommentPet(UID, PID,content);
            Console.WriteLine($"{UID} gives a comment that reads {content} to {PID}."); // 输出点赞信息
        }
        public static void UndoAComment(string UID, string PID,DateTime dateTime)
        {
            //调试用
            CommentPetServer.DeleteCommentPet(UID, PID, dateTime);
            Console.WriteLine($"{UID} undo a comment to {PID}."); // 输出点赞信息
        }
    }
}
