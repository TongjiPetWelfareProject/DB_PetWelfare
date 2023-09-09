using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using PetFoster.BLL;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.OracleClient;
using PetFoster.DAL;
using PetFoster.Model;
using System.Text.Json;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Glue.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace WebApplicationTest1
{

    [Route("api")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly FileHelper _fileHelper;
        public UserInfoController(FileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }
        public class UserInfoModel
        {
            public string user_id { get; set; }
            public string currentpassword { get; set; }
            public string editedpassword { get; set; }
            public string user_name { get; set; }
            public string phone { get; set; }
            public string province { get; set; }
            public string city { get; set; }
            public string avatar { get; set; }
            public UserInfoModel()
            {
                user_id = string.Empty;
                currentpassword = string.Empty;
                editedpassword = string.Empty; 
                user_name = string.Empty;
                phone = string.Empty;
                province = string.Empty;
                city = string.Empty;
                avatar = string.Empty;
            }

        }
        static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // 将输入字符串转换为字节数组
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // 计算哈希值
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // 将字节数组转换为十六进制字符串
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2")); // 使用 "x2" 格式将字节转换为两位十六进制
                }

                return stringBuilder.ToString();
            }
        }
        [Authorize]
        [HttpPost("editinfo")]
        public IActionResult EditUserInfo([FromBody] UserInfoModel userinfoModel)
        {
            int exit = UserServer.CheckUserPhoneExistence(userinfoModel.phone);
            User status = UserServer.GetUserByTel(userinfoModel.phone);

            if (exit == 1)
            {
                if (status.User_ID == userinfoModel.user_id)
                {
                    try
                    {
                        UserServer.UpdateUser2(userinfoModel.user_id, userinfoModel.user_name,
                            userinfoModel.phone, userinfoModel.city + "," + userinfoModel.province);
                        return Ok();
                    }
                    catch
                    {
                        return BadRequest("更改个人信息失败！");
                    }
                }
                return BadRequest("该手机号已被使用");
            }
            try
            {
                UserServer.UpdateUser2(userinfoModel.user_id, userinfoModel.user_name,
                    userinfoModel.phone, userinfoModel.city + "," + userinfoModel.province);
                return Ok();
            }
            catch
            {
                return BadRequest("更改个人信息失败！");
            }
        }
        [Authorize]
        [HttpPost("editpassword")]
        public IActionResult EditUserPassword([FromBody] UserInfoModel userinfoModel)
        {
            try
            {
                int result=UserManager.ChangePassword(userinfoModel.user_id, ComputeSHA256Hash(userinfoModel.currentpassword), ComputeSHA256Hash(userinfoModel.editedpassword));
                if(result == 0) { return Ok(0); }
                else if(result == -2) { return BadRequest(-2); }//密码不符合要求
                else if(result == -1) { return BadRequest(-1); }//密码不正确
                else { return BadRequest(1); }//响应超时
            }
            catch
            {
                return BadRequest("更改密码失败！");
            }

        }
        public class AvatarRequestModel
        {
            public string user_id { get; set; }
            public List<IFormFile>? filename { get; set; }
        }
        [Authorize]
        [HttpPost("editavatar")]
        public async Task<IActionResult> EditUserAvatar([FromForm] AvatarRequestModel avatarRequest)
        {
            try
            {
                string FileName = await _fileHelper.SaveFileAsync(avatarRequest.filename[0]);
                UserManager.UpdateAvatar(avatarRequest.user_id, FileName);
                return Ok("上传头像成功");
            }
            catch
            {
                return BadRequest("更改头像失败！");
            }
        }
        [Authorize]
        [HttpPost("userinfo")]
        public IActionResult GetUserInfo([FromBody] UserInfoModel userinfoModel)
        {
            int likenum = UserManager.GetLikeNum(userinfoModel.user_id);
            int readnum = UserManager.GetReadNum(userinfoModel.user_id);
            string user_name = UserServer.GetName(userinfoModel.user_id);
            string phone = UserServer.GetPhone(userinfoModel.user_id);
            string address = UserServer.GetAddress(userinfoModel.user_id);
            string avatar = UserServer.GetAvatar(userinfoModel.user_id);
            var userInfo = new
            {
                User_name = user_name, 
                Phone = phone,
                Address = JsonHelper.TranslateBackToChinese(address),
                Likes = likenum,
                Reads = readnum,
                Avatar=avatar
            };

            return Ok(userInfo);
        }
        [HttpPost("userpostcomment")]
        public IActionResult GetUserPostComment([FromBody] UserInfoModel userinfoModel)
        {
            List<PostComment> usercomment = CommentPostManager.ShowUIDComment(userinfoModel.user_id);
            return Ok(usercomment);
        }
        [HttpPost("userpostsend")]
        public IActionResult GetUserPostSend([FromBody] UserInfoModel userinfoModel)
        {
            DataTable userposts = ForumPostManager.ShowUIDPosts(userinfoModel.user_id);
            string json = DataTableToJson(userposts);
            return Content(json, "application/json");
        }
        [HttpPost("usercollectpet")]
        public IActionResult GetUserCollectPet([FromBody] UserInfoModel userinfoModel)
        {
            DataTable collectpets = CollectPetInfoServer.GetCollectPetInfos(userinfoModel.user_id);
            string json = DataTableToJson(collectpets);
            json = json.Replace("\\", "/");
            return Content(json, "application/json");
        }
        [HttpPost("userpostlike")]
        public IActionResult GetUserLikePost([FromBody] UserInfoModel userinfoModel)
        {
            DataTable likedposts = LikePostServer.GetLikePosts(userinfoModel.user_id);
            string json = DataTableToJson(likedposts);
            return Content(json, "application/json");
        }
        [HttpPost("usercommentpet")]
        public IActionResult GetUserCommentPet([FromBody] UserInfoModel userinfoModel)
        {
            DataTable collectpets = CommentPetServer.GetCommentPets(userinfoModel.user_id);
            string json = DataTableToJson(collectpets);
            return Content(json, "application/json");
        }
        [HttpPost("useradoptpet")]
        public IActionResult GetUserAdoptPet([FromBody] UserInfoModel userinfoModel)
        {
            DataTable adoptedpets = AdoptApplyServer.GetAdoptPets(userinfoModel.user_id);
            string json = DataTableToJson(adoptedpets);
            json = json.Replace("\\", "/");
            return Content(json, "application/json");
        }
        [HttpPost("userfosterpet")]
        public IActionResult GetUserFosterPet([FromBody] UserInfoModel userinfoModel)
        {
            DataTable adoptedpets = FosterServer.GetFosterPets(userinfoModel.user_id);
            string json = DataTableToJson(adoptedpets);
            return Content(json, "application/json");
        }
        [HttpPost("userlikepet")]
        public IActionResult GetUserLikePet([FromBody] UserInfoModel userinfoModel)
        {
            DataTable collectpets = LikePetServer.GetLikePet(userinfoModel.user_id);
            string json = DataTableToJson(collectpets);
            json = json.Replace("\\", "/");
            return Content(json, "application/json");
        }
        [HttpPost("userdonation")]
        public IActionResult GetUserDonation([FromBody] UserInfoModel userinfoModel)
        {
            DataTable donation = DonationManager.DonateIDsForUser(userinfoModel.user_id);
            string json = DataTableToJson(donation);
            return Content(json, "application/json");
        }
        [Authorize]
        [HttpPost("usermedical")]
        public IActionResult GetUserMedical([FromBody] UserInfoModel userinfoModel)
        {
            DataTable medical = AppointmentManager.GetUserAppointment(userinfoModel.user_id);
            string json = DataTableToJson(medical);
            return Content(json, "application/json");
        }
        private string DataTableToJson(DataTable table)
        {
            var jsonString = new StringBuilder();

            if (table.Rows.Count > 0)
            {
                jsonString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Append("{");

                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        jsonString.AppendFormat("\"{0}\":\"{1}\"",
                            table.Columns[j].ColumnName,
                            table.Rows[i][j]);

                        if (j < table.Columns.Count - 1)
                        {
                            jsonString.Append(",");
                        }
                    }

                    jsonString.Append("}");
                    if (i < table.Rows.Count - 1)
                    {
                        jsonString.Append(",");
                    }
                }
                jsonString.Append("]");
            }

            return jsonString.ToString();
        }
    }
}
