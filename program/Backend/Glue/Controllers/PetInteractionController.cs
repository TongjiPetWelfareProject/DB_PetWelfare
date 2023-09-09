using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using PetFoster.DAL;
using System.Data;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class PetInteractionController : ControllerBase
    {
        public class InteractionData
        {
            public string? user { get; set; }
            public string? pet { get; set; }
        }
        public class CommentData : InteractionData
        {
            public string text { get; set; }
            public string time { get; set; }
            public string commentText { get; set; }
            public CommentData()
            {
                text = "";
                time = "";
                commentText= string.Empty;
            }
        }
        
        // GET: api/<PetInteractionController>
        [HttpGet("comment-list")]
        public IActionResult Get()
        {
            try
            {
                DataTable dt = CommentPetManager.ShowCommentPet();
                List<CommentData> CommentsList = new List<CommentData>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CommentData CommentItem = new CommentData();
                    string? date = null;
                    string? time = null;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.ToLower() == "user_id")
                        {
                            CommentItem.user = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "pet_id")
                        {
                            CommentItem.pet = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "comment_contents")
                        {
                            CommentItem.text = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "comment_date")
                        {
                            date = dt.Rows[i][j].ToString();
                        }
                        else if(dt.Columns[j].ColumnName.ToLower() == "comment_time")
                        {
                            time = dt.Rows[i][j].ToString();
                        }
                    }
                    if(!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(time))
                    {
                        CommentItem.time = date + time;
                    }
                    CommentsList.Add(CommentItem);
                }
                return Ok(CommentsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        // GET api/<PetInteractionController>/5
        //查询用户是否收藏过该宠物
        //url和参数为临时
        public class HaveInteractModel
        {
            public bool is_liked { get; set; }
            public bool is_collected { get; set; }
        }
        public class HaveInteractRequest
        {
            public int user { get; set; }
            public int pet { get; set; }
        }
        [HttpPost("ifinteractpet")]
        public IActionResult GetFavored([FromBody] HaveInteractRequest request)
        {
            HaveInteractModel result = new HaveInteractModel();
            try
            {
                result.is_collected = CollectPetInfoManager.HaveUserCollected(request.user.ToString(), request.pet.ToString());
            }
            catch(Exception ex)
            {
                result.is_collected = false;
            }
            try
            {
                result.is_liked = LikePetManager.HaveUserLiked(request.user.ToString(), request.pet.ToString());
            }
            catch
            {
                result.is_liked = false;
            }
            return Ok(result);
        }

        // POST api/<PetInteractionController>
        [Authorize]
        [HttpPost("pet-submit-like")]
        public IActionResult PostLike([FromBody] InteractionData like_data)
        {
            if(like_data == null)
            {
                return BadRequest("Invalid Data");
            }
            if(like_data.user == null)
            {
                return BadRequest("Invalid User_Id");
            }
            if(like_data.pet == null)
            {
                return BadRequest("Invalid Pet_Id");
            }
            try
            {
                LikePetManager.GiveALike(like_data.user, like_data.pet, true);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // POST api/<PetInteractionController>
        [Authorize]
        [HttpPost("pet-cancel-like")]
        public IActionResult PostUndoLike([FromBody] InteractionData like_data)
        {
            if (like_data == null)
            {
                return BadRequest("Invalid Data");
            }
            if (like_data.user == null)
            {
                return BadRequest("Invalid User_Id");
            }
            if (like_data.pet == null)
            {
                return BadRequest("Invalid Pet_Id");
            }
            try
            {
                LikePetManager.GiveALike(like_data.user, like_data.pet, false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // POST api/<PetInteractionController>
        [Authorize]
        [HttpPost("pet-submit-favorite")]
        public IActionResult PostFavorite([FromBody] InteractionData favorite_data)
        {
            if (favorite_data == null)
            {
                return BadRequest("Invalid Data");
            }
            if (favorite_data.user == null)
            {
                return BadRequest("Invalid User_Id");
            }
            if (favorite_data.pet == null)
            {
                return BadRequest("Invalid Pet_Id");
            }
            try
            {
                CollectPetInfoManager.GiveACollect(favorite_data.user, favorite_data.pet, true);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // POST api/<PetInteractionController>
        [Authorize]
        [HttpPost("pet-cancel-favorite")]
        public IActionResult PostUndoFavorite([FromBody] InteractionData favorite_data)
        {
            if (favorite_data == null)
            {
                return BadRequest("Invalid Data");
            }
            if (favorite_data.user == null)
            {
                return BadRequest("Invalid User_Id");
            }
            if (favorite_data.pet == null)
            {
                return BadRequest("Invalid Pet_Id");
            }
            try
            {
                CollectPetInfoManager.GiveACollect(favorite_data.user, favorite_data.pet, false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // POST api/<PetInteractionController>
        [Authorize]
        [HttpPost("pet-submit-comment")]
        public IActionResult PostComment([FromBody] CommentData comment_data)
        {
            if (comment_data == null)
            {
                return BadRequest("Invalid Data");
            }
            if (comment_data.user == null)
            {
                return BadRequest("Invalid User_Id");
            }
            if (comment_data.pet == null)
            {
                return BadRequest("Invalid Pet_Id");
            }
            try
            {
                CommentPetManager.GiveAComment(comment_data.user, comment_data.pet, comment_data.commentText);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // POST api/<PetInteractionController>
        [Authorize]
        [HttpPost("pet-delete-comment")]
        public IActionResult PostUndoComment([FromBody] CommentData comment_data)
        {
            if (comment_data == null)
            {
                return BadRequest("Invalid Data");
            }
            if (comment_data.user == null)
            {
                return BadRequest("Invalid User_Id");
            }
            if (comment_data.pet == null)
            {
                return BadRequest("Invalid Pet_Id");
            }
            string format = "yyyy-MM-ddTHH:mm:ss"; // 格式与输入字符串一致
            DateTime? time = null;
            try
            {
                time = DateTime.ParseExact(comment_data.time, format, CultureInfo.InvariantCulture);
            }
            catch
            {
                time= DateTime.ParseExact(comment_data.time, "yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture);
            }
            if(time == null)
            {
                return BadRequest("Failed to parse the date.");
            }
            try
            {
                CommentPetManager.UndoAComment(comment_data.user, comment_data.pet, (DateTime)time);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*
        // PUT api/<PetInteractionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PetInteractionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
