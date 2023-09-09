using Oracle.ManagedDataAccess.Client;
using PetFoster;
using PetFoster.DAL;
using PetFoster.Model;
using System;
using System.Data;
using System.Windows.Forms;
using PetFoster.BLL;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
namespace PetFoster.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PwdBox.UseSystemPasswordChar = true;
        }

        public static string user = "\"C##PET\"";
        public static string pwd = "campus";
        public static string db = "localhost:1521/orcl";
        private static string connectionString = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";"; // 替换为实际的数据库连接字符串
        private void loginButton_Click(object sender, EventArgs e)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                PetData dataset1 = new PetData();
                
                // 获取数据表
                connection.Open();
                PetData.USER2Row newrow = dataset1.USER2.NewUSER2Row();
                newrow.PASSWORD = PwdBox.Text;
                newrow.USER_ID = UIDBox.Text;
                UserManager.Login(newrow.USER_ID,newrow.PASSWORD);
                connection.Close();
            }

            //

        }

        private void PwdBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void petbutton_Click(object sender, EventArgs e)
        {
            Pet Candidate=new Pet();
            PetManager.ViewProfile(1,out Candidate);
            ShowPicture(Candidate.Avatar);
        }
        public void ShowPicture(byte[] imageBytes)
        {
            // 创建一个 MemoryStream 来读取 byte[] 数据
            Image image = ConvertToImage(imageBytes);
            // 创建一个 PictureBox 控件用于展示图像
            petAvatar = new PictureBox
            {
                Image = image,
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };
        }
        public static Image ConvertToImage(byte[] imageBytes)
        {
            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                Image image = Image.FromStream(stream);
                return image;
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            UserManager.Register("Babatorse", "Mondestat11!", "13333333333", "Shanghai");
        }

        private void Unregisterbutton_Click(object sender, EventArgs e)
        {
            UserManager.Unregister(1385);
        }

        private void Supervisebutton_Click(object sender, EventArgs e)
        {
            UserManager.Ban(50);
        }

        private void Chpasswdbutton_Click(object sender, EventArgs e)
        {
            UserManager.ChangePassword(50, "Password1!", "OracleCo11!");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //CommentPetManager.ShowCommentPet(UID:"6",PID: "47");
            //CommentPetManager.GiveAComment("5", "7","Hello,I'm 5 I'll give 7 a comment");
            // FosterManager.ApplyFoster("1", "somedog", "Dachshund", "large", DateTime.UtcNow.AddDays(-90).AddMinutes(1), 90, "Hello,I want to rent a room!");
            // FosterManager.Censorship("1",64, DateTime.UtcNow.AddDays(-90).AddMinutes(1), 4);
            //List<string> paths= new List<string> { "D:\\Desktop\\Picture\\pic4.png", "D:\\Desktop\\Picture\\pic5.png" };
            AdoptManager.ShowPetProfile();

        }
    }
}
