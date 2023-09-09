using PetFoster.DAL;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Glue.PetFoster.Model;
using Glue.PetFoster.BLL;

namespace PetFoster.BLL
{
    public class Posts
    {
        string contents;
        //图片相对路径
        List<string> images;
        public Posts(string s, List<string> imgs)
        {
            contents = s;
             images= imgs ;
        }
    };
    public class ForumPostManager
    {
        public static DataTable ShowForumProfile(int Limitrow = -1, string Orderby = null)
        {
            DataTable dt = ForumPostServer.UncensoredForum(Limitrow, Orderby,beingcensored:false);
            //调试用
            Console.WriteLine("正在显示待审核的帖子");
            return dt;
        }

        public static void CensorPost(string FID, bool passed = false)
        {
            ForumPostServer.Censor(FID, passed);
        }
        /// <summary>
        /// 展示帖子界面
        /// </summary>
        /// <param name="PID"></param>
        public static List<ForumPost> ShowPost(string PID)
        {
            List<ForumPost> dt = ForumPostServer.SelectPost(PID);
            return dt;
        }
        public static void UpdateForumProfile(string FID)
        {
            int id;
            if (int.TryParse(FID, out id))
            {
                // FID 全为数字，执行相应的逻辑
                ForumPostServer.UpdateForum(FID);
            }
            else
            {
                // FID 不全为数字，执行相应的错误处理逻辑
                Console.WriteLine("FID is not a valid numeric value.");
            }
        }
        public static bool DeleteForumProfile(string FID,string UID)
        {
            int id;
            int uid;
            bool status=false;
            string role = "";
            if (int.TryParse(FID, out id))
            {
                // FID 全为数字，执行相应的逻辑
                uid = Convert.ToInt32(UID);
                //删除评论
                CommentPostServer.DeleteAllCommentsForPost(FID);
                Console.WriteLine("All relative comments has been removed");
                LikePostServer.DeleteAllLikesForPost(FID);
                Console.WriteLine("All relative likes has been removed");
                status = ForumPostServer.DeleteForum(FID);
                role=UserServer.GetRole(UID);
                if(role=="User")
                    UserManager.Ban(uid, "Warning Issued");
                Console.WriteLine("Forum profile with FID " + FID + " has been deleted.");
                Console.WriteLine("User with UID " + uid + " has been warned.");
            }
            else
            {
                // FID 不全为数字，执行相应的错误处理逻辑
                Console.WriteLine("FID is not a valid numeric value.");
                return false;
            }
            return status;
        }
        /// <summary>
        /// 发布帖子待审阅
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="contents"></param>
        /// <param name="paths">图片路径</param>
        public static int PublishPost(string UID,string heading,string contents,List<string> paths)
        {
            //更新帖子
            int FID = ForumPostServer.InsertPost(UID, heading,contents);
            //上传图片（最多五张）
            foreach(var path in paths)
            {
                PostImagesServer.InsertImage(FID.ToString(), path);
            }
            return FID;
        }
        public static int PublishPost(string UID, string heading, string contents)
        {
            //更新帖子
            int FID = ForumPostServer.InsertPost(UID, heading, contents);
            return FID;
        }
        /// <summary>
        /// 获得帖子的图片和内容，图片以二进制形式存储，需要类型转换
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public static List<Posts> GetPosts(string UID)
        {
            List<string> FID = ForumPostServer.GetPosts(UID);
            List<Posts> posts = new List<Posts>();
            foreach (var sFID in FID)
            {
                //获得图片与帖子内容
                Posts tmp = GetPost(sFID);
                posts.Add(tmp);
            }
            return posts;
        }
        public static Posts GetPost(string FID)
        {
            //获得图片与帖子内容
            string content = ForumPostServer.GetContent(FID);
            List<string> image = PostImagesServer.GetImages(Convert.ToInt32(FID));
            Posts tmp = new Posts(content, image);
            //阅读量+1！
            ForumPostServer.ReadForum(FID);
            return tmp;
        }

        public static List<ForumPost> GetAllPosts()
        {
            List<ForumPost> PostList = new List<ForumPost>();

            PostList = ForumPostServer.GetAllPosts();
            return PostList;
        }
        public static List<ForumPost> GetAllPostsForUser(string UID)
        {
            List<ForumPost> PostList = new List<ForumPost>();

            PostList = ForumPostServer.GetAllPostsForUser(UID);
            return PostList;
        }
        //需要转化为List<Comment>，Comment位于PetFoster.Model
        public static DataTable GetAllComments(string PID)
        {
            return CommentPostServer.CommentPostInfo(PID: PID);
        }
        //获取帖子点赞数
        public static DataTable GetLikeNums(string PID)
        {
            string query =$"select like_num from verbosepost where PID={PID}";
            return DBHelper.ShowInfo(query);
        }
        //获取帖子评论数
        public static DataTable GetCommentNums(string PID)
        {
            string query = $"select comment_num from verbosepost where PID={PID}";
            return DBHelper.ShowInfo(query);
        }
        //获取用户帖子点赞数
        public static DataTable GetTotalLikeNums(string UID)
        {
            string query = $"select sum(like_num) from verbosepost where UID={UID}";
            return DBHelper.ShowInfo(query);
        }
        //获取用户帖子评论数
        public static DataTable GetTotalCommentNums(string UID)
        {
            string query = $"select sum(comment_num) from verbosepost where UID={UID}";
            return DBHelper.ShowInfo(query);
        }

        public static DataTable ShowUIDPosts(string user_id)
        {
            string query = "select post_id,heading,read_count,comment_numpost_func(post_id) as comment_num,like_numpost_func(post_id) as like_num,post_time from forum_posts" +
                $" where user_id={user_id}";
            return DBHelper.ShowInfo(query);
        }
    }
}
