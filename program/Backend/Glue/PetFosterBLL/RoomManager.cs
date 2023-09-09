using Glue.PetFoster.BLL;
using Oracle.ManagedDataAccess.Client;
using PetFoster.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.BLL
{
    public class RoomManager
    {
        /// <summary>
        /// 展示楼层房间信息
        /// </summary>
        public static void ShowStories()
        {
            DataTable dt = RoomServer.StoreyInfo();
            //调试用
            Util.DebugTable(dt);
        }
        /// <summary>
        /// 展示房源
        /// </summary>
        /// <param name="Limitrows">展示的最大行数</param>
        /// <param name="Orderby">排序依据(降序)</param>
        public static DataTable ShowRooms(decimal Limitrows = -1, string Orderby = null,bool OnlyAvailable=false)
        {
            DataTable dt = RoomServer.RoomInfo(Limitrows,Orderby,OnlyAvailable);
            //调试用
            Console.WriteLine("开始显示10层楼300间房的使用情况");
            return dt;
        }
        public static void CleanRoom(short storey,short compartment)
        {
            RoomServer.UpdateRoom(storey, compartment, true);
        }
        /// <summary>
        /// 这只是一个大致的征用房间函数，需要考虑房间的大小是否在某一个范围内
        /// </summary>
        public static void RentRoom()
        {
            bool rooted = false;
            short storey = 0;
            short compartment = 0;
            DataTable dt = RoomServer.RoomInfo(Orderby:"room_size", OnlyAvailable:true);
            foreach (DataRow row in dt.Rows)
            {
                if(row.ItemArray[0].ToString() == "N")
                {
                    Console.WriteLine($"租房成功，房间状态为{row.ItemArray[0]},房间号为{row.ItemArray[2]}-{row.ItemArray[3]},房间大小为{row.ItemArray[1]}");
                    storey = Convert.ToInt16(row.ItemArray[2]);
                    compartment = Convert.ToInt16(row.ItemArray[3]);
                    rooted= true;
                    break;
                }
            }
            if (rooted)
                RoomServer.UpdateRoom(storey, compartment,room_status:"Y");
            else
                Console.WriteLine("没有符合要求的房源！");
        }
        public static void ReturnRoom(short storey, short compartment)
        {
            RoomServer.UpdateRoom(storey, compartment, room_status: "N");
        }
    }
}
