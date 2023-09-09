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
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class ManageNoticeController : Controller
    {
        public class NoticeModel
        {
            public string Id { get; set; }
            public string noticeId { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public string time { get; set; }
            public string employeeId { get; set; }
            public string employeeName { get; set; }
            public NoticeModel()
            {
                Id = "";
                noticeId = "";
                employeeId = "";
                title = "";
                employeeName = "";
                content = "";
                time = "";
            }
        }

        //added by rqx 8.24
        // 获取全部公告
        [HttpGet("notice")]
        public IActionResult getNotice(int page = 1, int pageSize = 10)
        {
            try
            {
                // 在这里编写获取全部公告的逻辑
                // 返回公告数据
                DataTable dt = BulletinManager.ShowBulletinsForAdmin();

                int totalCount = dt.Rows.Count;
                int startIndex = (page - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, totalCount);

                List<NoticeModel> NoticeList = new List<NoticeModel>();

                for (int i = startIndex; i < endIndex; i++)
                {
                    NoticeModel NoticeItem = new NoticeModel();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.ToLower() == "bulletin_id")
                        {
                            NoticeItem.Id = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "heading")
                        {
                            NoticeItem.title = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "bulletin_contents")
                        {
                            NoticeItem.content = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "published_time")
                        {
                            NoticeItem.time = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "employee_id")
                        {
                            NoticeItem.employeeId = dt.Rows[i][j].ToString();
                            NoticeItem.employeeName = EmployeeServer.GetName(dt.Rows[i][j].ToString());
                        }
                    }
                    NoticeList.Add(NoticeItem);
                }
                return Ok(new { data = NoticeList, total = totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // 获取捐赠记录
        /*[HttpGet]
        [Route("donation")]
        public IActionResult getDonation()
        {
            try
            {
                // 在这里编写获取捐赠记录的逻辑
                // 返回捐赠记录数据
                return Ok(donationData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }*/

        // 发送新公告
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("send-notice")]
        public IActionResult sendNewNotice([FromBody] NoticeModel notice)
        {
            if (string.IsNullOrEmpty(notice.employeeId) || !int.TryParse(notice.employeeId, out int eid))
            {
                return BadRequest("Invalid Employee Id.");
            }
            if (string.IsNullOrEmpty(notice.title))
            {
                return BadRequest("Empty title");
            }
            if (UserServer.GetRole(notice.employeeId) != "Admin")
            {
                return BadRequest("Only admin/employee can edit the bulletin");
            }
            //调试
            Console.WriteLine("eid:" + eid);
            Console.WriteLine("title:" + notice.title);
            Console.WriteLine("content:" + notice.content);
            try
            {
                // 在这里编写发送新公告的逻辑
                BulletinServer.AddBulletin(notice.title, eid.ToString(), notice.content);
                // 返回发送结果
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // 获取公告内容
        /*[HttpGet]
        [Route("get-notice-content/{noticeId}")]
        public IActionResult getNoticeContent(int noticeId)
        {
            try
            {
                // 在这里编写获取公告内容的逻辑
                // 返回公告内容数据
                return Ok(noticeContent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }*/
        
        // 发送编辑过的公告
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("send-edited-notice")]
        public IActionResult sendEditedNotice([FromBody] NoticeModel notice)
        {
            if(string.IsNullOrEmpty(notice.noticeId) || !int.TryParse(notice.noticeId, out int bulletin_id))
            {
                return BadRequest("Invalid Notice Id.");
            }
            if (string.IsNullOrEmpty(notice.employeeId) || !int.TryParse(notice.employeeId, out int eid))
            {
                return BadRequest("Invalid Employee Id.");
            }
            if (string.IsNullOrEmpty(notice.title))
            {
                return BadRequest("Empty title");
            }
            //调试
            //Console.WriteLine("bulletin_id:" + bulletin_id);
            Console.WriteLine("eid:" + eid);
            Console.WriteLine("title:" + notice.title);
            Console.WriteLine("content:" + notice.content);
            try
            {
                // 在这里编写发送编辑过的公告的逻辑
                BulletinServer.EditBulletin(bulletin_id.ToString(),notice.title, notice.content);
                // 返回发送结果
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // 删除公告
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("delete-notice/{noticeId}")]
        public IActionResult deleteNotice(int noticeId)
        {
            try
            {
                // 在这里编写删除公告的逻辑
                // 返回删除结果
                BulletinServer.DeleteBulletin(noticeId.ToString());
                Console.WriteLine($"已删除{noticeId}号公告");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
