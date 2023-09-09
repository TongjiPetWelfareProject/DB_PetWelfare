using Microsoft.AspNetCore.Mvc;

using PetFoster.BLL;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.OracleClient;
using PetFoster.DAL;
using PetFoster.Model;
using System.Text.Json;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Numerics;
using Glue.PetFoster.Model;
using static Glue.Controllers.RegisterController;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Authorization;

namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class ForumController : Controller
    {
        private readonly FileHelper _fileHelper;

        public ForumController(FileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }
        public class PostModel
        {
            public string? post_id { get; set; }
            public string? user_id { get; set; }
            public string? post_title { get; set; }
            public string? post_content { get; set; }
            public string? added_comment { get; set; } 
            public DateTime comment_time { get; set; }

        }

        [HttpGet("forum")]
        public IActionResult Post()
        {
            List<ForumPost> postlist = ForumPostManager.GetAllPosts();
            Console.WriteLine("收到论坛请求");
            return Ok(postlist);
        }

        [HttpPost("post")]
        public IActionResult PostInfo([FromBody] PostModel postModel)
        {
            List<ForumPost> post = ForumPostManager.ShowPost(postModel.post_id);
            ForumPostServer.ReadForum(postModel.post_id);
            Console.WriteLine("收到帖子请求："+ postModel.post_id);
            return Ok(post);
        }
        public class PostRequestModel
        {
            public string? user_id { get; set; }
            public string? post_title { get; set; }
            public string? post_content { get; set; }
            public List<IFormFile>? filename { get; set; }
        }
        [Authorize]
        [HttpPost("postcontent")]
        public async Task<IActionResult> PostContent([FromForm] PostRequestModel postModel)
        {
            if(postModel.user_id == null)
            {
                return BadRequest("用户未登录");
            }
            string acstatus = UserServer.GetStatus(postModel.user_id);
            string role = UserServer.GetRole(postModel.user_id);
            
            if ((acstatus == "Warning Issued" || acstatus == "Banned" ||acstatus=="Under Review")&&role=="User")
                return BadRequest("您的账号活动异常，无法发布帖子");
            if (postModel.post_content == null)
                postModel.post_content = "";
            try
            {
                if (postModel.filename != null)
                {
                    List<string> FileNames = await _fileHelper.SaveImagesAsync(postModel.filename);
                    // 把下面的调用要改成传FileNames进去
                    int status = ForumPostManager.PublishPost(postModel.user_id, postModel.post_title, postModel.post_content, FileNames);
                    if (role == "Admin")
                        ForumPostManager.CensorPost(status.ToString(), false);
                    //Console.WriteLine("收到发帖请求：" + postModel.post_id);
                    if (status != -1)
                        return Ok(0);
                    else
                        return BadRequest(-1);
                }
                else
                {
                    int status = ForumPostManager.PublishPost(postModel.user_id, postModel.post_title, postModel.post_content);
                    if (role == "Admin")
                        ForumPostManager.CensorPost(status.ToString(), false);
                    //Console.WriteLine("收到发帖请求：" + postModel.post_id);
                    if (status != -1)
                        return Ok(0);
                    else
                        return BadRequest(-1);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Upload failed");
            } 
        }

        [HttpPost("postcomment")]
        public IActionResult PostComment([FromBody] PostModel postModel)
        {
            List<PostComment> AllComment= CommentPostManager.ShowPIDPost(postModel.post_id);
            Console.WriteLine("收到帖子评论列表请求：" + postModel.post_id);
            return Ok(AllComment);
        }
        [Authorize]
        [HttpPost("addcomment")]
        public IActionResult AddComment([FromBody] PostModel postModel)
        {
            string acstatus = UserServer.GetStatus(postModel.user_id);
            if (acstatus == "Warning Issued" || acstatus == "Banned" || acstatus == "Under Review")
                return BadRequest("您的账号活动异常，无法发布评论");
            Console.WriteLine("收到帖子评论请求,帖子ID：" + postModel.post_id+ "评论内容："+postModel.added_comment+"评论人ID："+ postModel.user_id);
            int status = CommentPostManager.GiveACommentPost(postModel.user_id,postModel.post_id,postModel.added_comment);
            if(status!=-1) 
                return Ok("评论成功");
            return BadRequest(-1);
        }
        [Authorize]
        [HttpPost("likepost")]
        public IActionResult LikePost([FromBody] PostModel postModel)
        {
            Console.WriteLine("收到点赞帖子请求,帖子ID：" + postModel.post_id + "点赞人ID：" + postModel.user_id);
            LikePostManager.GiveALike(postModel.user_id, postModel.post_id);
            return Ok("点赞或取消成功");
        }
        [HttpPost("iflikepost")]
        public IActionResult IfLikePost([FromBody] PostModel postModel)
        {
            Console.WriteLine("收到获取点赞信息请求,帖子ID：" + postModel.post_id + "点赞人ID：" + postModel.user_id);
            int status = LikePostManager.IfLike(postModel.user_id, postModel.post_id);
            return Ok(status);
        }
        [Authorize]
        [HttpPost("deletecomment")]
        public IActionResult DeleteComment([FromBody] PostModel postModel)
        {
            Console.WriteLine("收到获取点赞信息请求,帖子ID：" + postModel.post_id + "点赞人ID：" + postModel.user_id);
            CommentPostManager.UndoACommentPost(postModel.user_id, postModel.post_id,postModel.comment_time);
            return Ok();
        }
        [Authorize]
        [HttpPost("deletepost")]
        public IActionResult DeletePost([FromBody] PostModel postModel)
        {
            PostImagesServer.DeleteImages(postModel.post_id);
            bool status = ForumPostManager.DeleteForumProfile(postModel.post_id,postModel.user_id);
            Console.WriteLine("收到删除帖子请求：" + postModel.post_id+"；用户id："+postModel.user_id);
            if(status)
                return Ok();
            else
                return BadRequest();
        }
    }
}
