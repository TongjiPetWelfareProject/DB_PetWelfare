using Glue.PetFoster.BLL;
using PetFoster.DAL;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.BLL
{
    public class BulletinManager
    {
        //搜索公告 最新
        public static void ShowBulletins(string text)
        {
            DataTable dt = BulletinServer.BulletinInfo(text);
            //调试用
            Util.DebugTable(dt);
        }
        //公告详细界面
        public static void ShowBulletinVerbose(string BID)
        {
            PetFoster.Model.Bulletin target = BulletinServer.SelectBulletin(BID);
        }

        // 获取所有公告信息列表
        public static List<Bulletin> GetAllBulletins()
        {
            List<Bulletin> bulletinList = new List<Bulletin>();

            bulletinList = BulletinServer.GetAllBulletins(); // Empty string for all bulletins
            return bulletinList;
        }

        public static DataTable ShowBulletinsForAdmin()
        {
            try
            {
                DataTable dt = BulletinServer.BulletinInfoForAdmin();
                //调试用
                Console.WriteLine("开始选择公告！");
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("数据库执行错误");
            }
        }
    }
}
