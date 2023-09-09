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
    public class AdminUserController : ControllerBase
    {
        public class UserModel
        {
            public string? id { get; set; }
            public string? username { get; set; }
            public string? phone { get; set; }
            public string? address { get; set; }
            public string? account_status { get; set; }
        }

        // GET: api/<AdminUserController>
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("tableuser")]
        public IActionResult Get()
        {
            try
            {
                DataTable dt = UserManager.ShowUserProfile();
                List<UserModel> UsersList = new List<UserModel>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserModel UserItem = new UserModel();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.ToLower() == "user_id")
                        {
                            UserItem.id = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "user_name")
                        {
                            UserItem.username = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "phone_number")
                        {
                            UserItem.phone = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "address")
                        {
                            UserItem.address = JsonHelper.TranslateBackToChinese(dt.Rows[i][j].ToString());
                        }
                        else if(dt.Columns[j].ColumnName.ToLower() == "account_status")
                        {
                            UserItem.account_status = JsonHelper.TranslateToCn(dt.Rows[i][j].ToString(),"status");
                            //UserItem.account_status = dt.Rows[i][j].ToString();
                        }
                    }
                    //Console.WriteLine(UserItem.account_status);
                    UsersList.Add(UserItem);
                }
                //string jsonstring = "{\"data\":" + JsonSerializer.Serialize(UsersList) + "}";
                //Console.WriteLine(jsonstring);
                return Ok(UsersList);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        /*
        // GET api/<AdminUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */

        private IActionResult _ChangeUserStatus(string id, string target_status)
        {
            if (!int.TryParse(id, out int uid))
            {
                return BadRequest("Invalid UID.");
            }
            try
            {
                string message = UserManager.Ban(uid, target_status); // status change to banned
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<AdminUserController>
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("block")] // 封号
        public IActionResult PostBlock([FromBody] string id)
        {
            return _ChangeUserStatus(id, "Banned");
        }
        // POST api/<AdminUserController>
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("ban")] // 禁言
        public IActionResult PostBan([FromBody] string id)
        {
            if (UserServer.GetStatus(id) == "Banned")
                return Ok();
            return _ChangeUserStatus(id, "Under Review");
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("remove-block")]
        [HttpPost("remove-ban")] // 解除禁言/解除封号，逻辑由UserManager.Ban实现，故在这里共用同一函数
        public IActionResult PostRemoveBan([FromBody] string id)
        {
            return _ChangeUserStatus(id, "In Good Standing");
        }
        /*
        // PUT api/<AdminUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
