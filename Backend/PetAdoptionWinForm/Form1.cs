using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PetAdopt.BLL;
using PetAdoptionModel;
using PetAdopt.DAL;
using PetAdopt.Utility;
using PetAdopt.Model;
using Oracle.ManagedDataAccess.Client;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;
using static PetAdopt.Model.DataSet1;

namespace PetAdoptionWinForm
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
        private void button1_Click(object sender, EventArgs e)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                DataSet1 dataset1 = new DataSet1();
                // 获取数据表
                connection.Open();
                USER2Row newrow=dataset1.USER2.NewUSER2Row();
                newrow.PASSWORD = PwdBox.Text;
                newrow.USER_ID = UIDBox.Text;
                
                UserServer.GetUser(newrow);
                connection.Close();
            }

            //
           
        }
    }
}
