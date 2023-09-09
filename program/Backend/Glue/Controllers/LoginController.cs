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
using System.Security.Claims;
using Glue.Controllers;

namespace WebApplicationTest1
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TokenHelper _tokenHelper;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenHelper = new TokenHelper(configuration);
        }
        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }

            public LoginModel()
            {
                Username = string.Empty;
                Password = string.Empty;
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
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            string uid = loginModel.Username;
            string password = loginModel.Password;
            Console.WriteLine(uid + " " + password);
            //int exit = UserServer.CheckUserExistence(uid);
            //if(exit == -1) {
            //    return Unauthorized(-3); //"用户不存在"
            //}
            User candidate = UserManager.Login(uid, password);
            password = ComputeSHA256Hash(password);
            if (candidate.Password != password)
            {
                return Unauthorized(-1);//"密码错误，请重新输入"
            }
            else if (candidate.Account_Status == "Banned")
            {
                return Unauthorized(-2);//"账号已被封禁，请等待解禁"
            }
            else if (candidate.User_ID == "-1")
            {
                return Unauthorized(-3); //"用户不存在"
            }
            else
            {
                //用户身份验证成功，生成JWT令牌
                string tokenString = _tokenHelper.GenerateToken(candidate.User_ID);

                var responseData = new
                {
                    data = new
                    {
                        token = tokenString,
                        User_ID = candidate.User_ID,
                        User_Name = candidate.User_Name,
                        Password = candidate.Password,
                        Phone_Number = candidate.Phone_Number,
                        Address = JsonHelper.TranslateBackToChinese(candidate.Address),
                        Role = candidate.Role,
                        Account_Status = candidate.Account_Status,
                        Avatar = candidate.Avatar
                    }
                };
                string responseJson = JsonSerializer.Serialize(responseData);
                return Ok(responseJson);
            }
        }
    }
}
