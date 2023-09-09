using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using PetFoster.DAL;
using System.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class AdminCensorController : ControllerBase
    {
        public class PostModel
        {
            public string? postId { get; set; }
            public string? employeeId { get; set; }
            public string? postTime { get; set; }
            public string? content { get; set; }
            public string? censor_status { get; set; }
            public List<string> urls { get; set; }
            public PostModel()
            {
                censor_status = "to be censored";
            }
        }
        // GET: api/<AdminCensorController>
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("check")]
        public IActionResult Get()
        {
            try
            {
                DataTable dt = ForumPostManager.ShowForumProfile();
                List<PostModel> PostsList = new List<PostModel>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PostModel PostItem = new PostModel();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.ToLower() == "post_id")
                        {
                            PostItem.postId = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "user_name")
                        {
                            PostItem.employeeId = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "post_time")
                        {
                            PostItem.postTime = dt.Rows[i][j].ToString();
                        }
                        else if(dt.Columns[j].ColumnName.ToLower() == "post_contents")
                        {
                            PostItem.content = dt.Rows[i][j].ToString();
                        }
                    }
                    PostItem.urls = PostImagesServer.GetImages(Convert.ToInt32(PostItem.postId));
                    PostsList.Add(PostItem);
                }
                //Console.WriteLine(JsonSerializer.Serialize(PostsList));
                //Console.WriteLine(jsonstring);
                return Ok(PostsList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Controller:" + ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /*
        // GET api/<AdminCensorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AdminCensorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AdminCensorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminCensorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
        // PATCH api/<AdminCensorController>
        [Authorize(Policy = "AdminOnly")]
        [HttpPatch("check-info-update")]
        public IActionResult Patch([FromBody] PostModel PostData)
        {
            if(PostData == null)
            {
                return BadRequest("Invalid Data");
            }
            if(PostData.postId == null)
            {
                return BadRequest("Invalid PostId");
            }
            bool passed;
            if (PostData.censor_status == "aborted")
            {
                passed = false;
            }
            else if(PostData.censor_status == "legitimate")
            {
                passed = true;
            }
            else
            {
                return NoContent();
            }

            try
            {
                ForumPostManager.CensorPost(PostData.postId, passed);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
