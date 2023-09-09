using Oracle.ManagedDataAccess.Client;
using PetFoster.DAL;
using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using static PetFoster.Model.PetData;
using System.Security.Cryptography;
namespace PetFoster.BLL
{
    public class UserManager
    {
        static public bool IsValidStatus(string status)
        {
            // 解析JSON字符串
            return JsonHelper.TranslateToCn(status, "status") != null;
        }
        public static DataTable ShowUserProfile(int Limitrow = -1, string Orderby = null)
        {
            DataTable dt = UserServer.UserInfo(Limitrow, Orderby);
            //调试用
            Console.WriteLine("展示用户列表");
            return dt;
        }
        public static string ComputeSHA256Hash(string input)
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
        public static void UpdateAvatar(string UID, string path)
        {
        
            UserServer.UpdateUserAvatar(UID, path);
         
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>返回错误码，在JSON中指定,4为用户，5为管理员</returns>
        public static User Login(string UID, string Pwd)
        {
            // 连接对象将在 using 块结束时自动关闭和释放资源
            // 在此块中执行数据操作
            User Candidate = UserServer.GetUserByTel(UID);
            return Candidate;
        }
        public static User LoginByTel(string Tel, string Pwd)
        {
            User Candidate = UserServer.GetUser(Tel, Pwd);
            return Candidate;
        }
        private static bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^1\d{2}\s\d{4}\s\d{4}$";
            bool isValid = Regex.IsMatch(phoneNumber, pattern);
            return isValid;
        }
        private static bool ValidatePassword(string password)
        {
            bool hasMinimumLength = password.Length >= 10;
            bool hasDigit = Regex.IsMatch(password, @"\d");
            bool hasLowerCase = Regex.IsMatch(password, @"[a-z]");
            bool hasUpperCase = Regex.IsMatch(password, @"[A-Z]");
            bool hasSpecialCharacter = Regex.IsMatch(password, @"[/!@#$%^&*()]");

            bool isValid = hasMinimumLength && hasDigit && hasLowerCase && hasUpperCase && hasSpecialCharacter;
            return isValid;
        }
        public static bool IsValidAddress(string address)
        {
            //string res = JsonHelper.TranslateAddr(address);
            string res = address;
            return res != null;
        }
        public static Profile GetStatistics(int UID)
        {
            int errcode = 0;
            return UserServer.GetStatistics(UID, out errcode);
        }
        private static int ValidRegistration(string Username, string pwd, string phoneNumber, string Address = "Beijing")
        {
            if (!IsValidAddress(Address))
            {
                return 0;
            }
            else if (!ValidatePhoneNumber(phoneNumber))
            {
                return 1;
            }
            else if (!ValidatePassword(pwd))
            {
                return 2;
            }
            else if (Username.Length > 20)
            {
                return 3;
            }
            else
                return 4;

        }
        /// <summary>
        /// 校验信息并注册
        /// </summary>
        /// <param name="Username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="Address">地址</param>
        /// <returns>返回状态string</returns>
        public static int Register(out string UID, string Username, string pwd, string phoneNumber, string Address = "Beijing")
        {
            // 添加新行
            UID = null;
            int code = ValidRegistration(Username, pwd, phoneNumber, Address);
            if (code != 4) { return code; }
            //Address = JsonHelper.TranslateAddr(Address);
            UID = UserServer.InsertUser(Username, ComputeSHA256Hash(pwd), phoneNumber, Address);
            //注册时的其他操作，如验证码等等.....
            if (UID == "-1"){ return -1; }
            return code;
        }
        public static string Unregister(decimal UID)
        {
            bool rows = UserServer.DeleteUser(UID.ToString());
            if (rows)
            {
                return $"{UID},您已经注销成功!";
            }
            else
                return $"不存在UID为{UID}的用户";
        }
        //以下是更改个人信息部分
        //修改密码
        /// <summary>
        /// 封禁或解禁账户
        /// 通过解除禁言，不能解除封禁状态
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="status">设置用户相应的状态</param>
        public static string Ban(decimal UID, string status = "Banned")
        {
            User user = UserServer.GetUser(UID.ToString(), "0", true);
            // 通过解除禁言，不能解除封禁状态
            string current_status = UserServer.GetStatus(UID.ToString());
            if(status == current_status)
            {
                throw new Exception($"用户{user.User_Name}已经处于{status}状态");
            }
            if (status == "Under Review" && current_status == "Banned")
            {
                throw new Exception($"用户{user.User_Name}处于封禁状态，不能解除禁言");
            }

            if (IsValidStatus(status))
            {
                UserServer.UpdateUser(UID.ToString(), user.User_Name, user.Password, user.Phone_Number, user.Address, status);
                return $"已将用户{user.User_Name}状态设置为{status}";
            }
            else
                throw new Exception($"不存在{status}这种状态");
        }
        static int RemainingTime = 5;
        static bool Waiting = false;
        static CountdownTimer countdownTimer;
        /// <summary>
        /// 改密码
        /// </summary>
        /// <param name="UID">用户名</param>
        /// <param name="Password">旧密码</param>
        /// <param name="NewPassword">新密码</param>
        /// <returns></returns>
        public static int ChangePassword(string UID, string Password, string NewPassword)
        {
            TimeSpan timeRemaining;
            User candidate = UserServer.GetUser(UID, Password, true);
            if (Waiting && countdownTimer.GetTimeRemaining().Ticks > 0)
            {
                timeRemaining = countdownTimer.GetTimeRemaining();
                //return $"Time remaining: {timeRemaining.Hours} hours, {timeRemaining.Minutes} minutes, {timeRemaining.Seconds} seconds";
                return 1;
            }
            else if (Waiting && countdownTimer.GetTimeRemaining().Ticks <= 0)
            {
                Waiting = false;
                RemainingTime = 5;
            }
            if (candidate.Password != Password && --RemainingTime > 0)
            {
                //return $"密码不正确,还有{RemainingTime}次机会，共计5次机会";
                return -1;
            }
            else if (RemainingTime == 0)
            {
                DateTime targetTime = DateTime.Now.AddMinutes(180);  // 假设倒计时目标时间为当前时间的10分钟后
                countdownTimer = new CountdownTimer(targetTime);
                Waiting = true;
            }
            else if (candidate.Password == Password)
            {
                UserServer.UpdatePassword(UID,NewPassword);
                //return $"{candidate.User_Name},你好！密码已成功修改，请不要忘记密码";
                return 0;
            }
            timeRemaining = countdownTimer.GetTimeRemaining();
            //return $"Time remaining: {timeRemaining.Hours} hours, {timeRemaining.Minutes} minutes, {timeRemaining.Seconds} seconds";
            return 1;

        }
        public static int GetLikeNum(string UID)
        {
            int num = UserServer.GetTotalLikes(UID);
            return num;
        }
        public static int GetReadNum(string UID)
        {
            int num = UserServer.GetTotalReadCount(UID);
            return num;
        }
    }
    public class CountdownTimer
    {
        private DateTime targetTime;

        public CountdownTimer(DateTime targetTime)
        {
            this.targetTime = targetTime;
        }

        public TimeSpan GetTimeRemaining()
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan timeRemaining = targetTime - currentTime;
            return (timeRemaining.Ticks > 0) ? timeRemaining : TimeSpan.Zero;
        }
    }
}