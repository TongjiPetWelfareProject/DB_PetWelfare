using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PetFoster.Model;
using PetFoster.BLL;

namespace PetFoster.DAL
{
    public class AdoptApplyServer
    {
        public static string conStr = AccommodateServer.conStr;
        public static bool DeleteAdopts(string UID)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 执行删除操作
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"delete adopt_apply where Adopter_ID= {UID}";
                command.Parameters.Clear();
                try
                {
                    command.ExecuteNonQuery();
                    command.CommandText = $"delete adopt where Adopter_ID= {UID}";
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public static DataTable AdoptInfo(decimal Limitrows = -1, string Orderby = null)
        {
            
            string query = "SELECT adopt_view.*,pet_name,user_name" +
                    " FROM adopt_view left join pet on pet.pet_id=adopt_view.pet_id" +
                    " left join user2 on user2.user_id=adopt_view.adopter_id";
            DataTable dataTable = DBHelper.ShowInfo(query, Limitrows, Orderby);
            return dataTable;
        }
        //审核领养界面(没必要传理由，管理员已经看过了)
        
        //选择不在申请寄养或领养中并排除已经被寄养或领养的宠物
        public static int GetRandomPet(string species)
        {
            int? exist = 0;
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT pet_id FROM ( SELECT *  FROM pet  " +
                    $"WHERE PID = '{species}'  ORDER BY DBMS_RANDOM.VALUE) WHERE pet_id " +
                    $"NOT IN(SELECT adopter_id FROM adopt) AND  pet_id NOT IN "+
                    $"(SELECT pet_id FROM foster where censor_state = 'legitimate' or censor_state = " +
                    $"'to be censored') AND ROWNUM <= 1";
                try
                {
                    exist = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                    if (exist == null)
                        return -1;//宠物都没空
                    else
                        return Convert.ToInt32(exist);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                    return -2;//执行异常
                }

            }
        }
        public static void UpdateAdoptEntry(string UID,string PID,string censor_status)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"UPDATE adopt_apply SET censor_state='{censor_status}'" +
                    $" where adopter_id={UID} and pet_id={PID}";
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"用户:{UID}的领养申请通过状态为{censor_status}");
                }
                catch (OracleException ex)
                {
                    Console.WriteLine("错误码" + ex.ErrorCode.ToString());

                    throw;
                }
                connection.Close();
            }
        }
        public static void UpdateAdoptEntries(string UID, string PID, string censor_status)
        {
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"UPDATE adopt_apply SET censor_state='{censor_status}'" +
                    $" where adopter_id<>{UID} and pet_id={PID}";
                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"用户:{UID}的领养申请得以赦免");
                }
                catch (OracleException ex)
                {
                    Console.WriteLine("错误码" + ex.ErrorCode.ToString());

                    throw;
                }
                connection.Close();
            }
        }
        public static void InsertAdoptApply(string UID,string PID,bool gender,bool pet_exp,bool long_term_care,
            bool w_to_treat,decimal d_care_h,string P_Caregiver,decimal f_popul,bool be_children,bool accept_vis)
        {
            // 添加新行
            try
            {
                using (OracleConnection connection = new OracleConnection(conStr))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO adopt_apply (adopter_id, pet_id,adopter_gender,pet_experience" +
                       ",long_term_care,willing_to_treat,daily_care_hours,primary_caregiver,family_population,has_children,accept_visits) " +
                       $"VALUES ({UID},'{PID}',:gender,:p_exp,:lt_care,:w_t_treat,{d_care_h},'{P_Caregiver}',{f_popul},:h_child,:a_vis)";
                    command.Parameters.Add("gender", OracleDbType.Varchar2, gender?'M':'F', ParameterDirection.Input);
                    command.Parameters.Add("p_exp", OracleDbType.Varchar2, pet_exp?'Y':'N', ParameterDirection.Input);;
                    command.Parameters.Add("lt_care", OracleDbType.Varchar2, long_term_care?'Y':'N', ParameterDirection.Input);
                    command.Parameters.Add("w_t_treat", OracleDbType.Varchar2, w_to_treat?'Y':'N', ParameterDirection.Input);
                    command.Parameters.Add("h_child", OracleDbType.Varchar2, be_children?'Y':'N', ParameterDirection.Input);
                    command.Parameters.Add("a_vis", OracleDbType.Varchar2, accept_vis?'Y':'N', ParameterDirection.Input);
                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"{UID}请求申请抚养宠物");

                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine("不存在的用户或宠物");
                        throw new Exception("不存在的用户或宠物");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
        public static Pet2 SelectPet(string PID)
        {
            bool con = false;
            Pet2 petoverall = new Pet2();
            Pet pet = new Pet();
            using (OracleConnection connection = new OracleConnection(conStr))
            {
                // 连接对象将在 using 块结束时自动关闭和释放资源
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"select pet_profile.*,user_name from pet_profile "+
                    $"left join user2 on user_id=commenter where " +
                $"Pet_ID={PID}";
                try
                {
                    OracleDataReader reader = command.ExecuteReader();
                    reader.Read();
                    pet.Pet_ID = reader["Pet_ID"].ToString();
                    pet.Pet_Name = reader["Pet_Name"].ToString();
                    pet.Species = reader["Species"].ToString();
                    pet.birthdate = Convert.ToDateTime(reader["BIRTHDATE"]);
                    if (reader["Avatar"] != DBNull.Value)
                        pet.Avatar = reader["Avatar"].ToString();
                    else
                        pet.Avatar = null;
                    pet.Read_Num = Convert.ToDecimal(reader["Read_Num"]);
                    pet.Like_Num = Convert.ToDecimal(reader["Like_Num"]);
                    pet.Collect_Num = Convert.ToDecimal(reader["Collect_Num"]);
                    petoverall.original_pet = pet;
                    petoverall.original_pet.Vaccine = reader["Vaccine"].ToString();
                    petoverall.original_pet.Health_State = reader["Health_State"].ToString();
                    petoverall.Comment_Num = Convert.ToInt32(reader["comment_num"]);
                    petoverall.sex = Convert.ToChar(reader["sex"]) == 'M' ? true : false;
                    petoverall.Psize = reader["psize"].ToString();
                    petoverall.Popularity = Convert.ToInt32(reader["popularity"]);
                    petoverall.comments = new Pet2.Comment[petoverall.Comment_Num];
                    for (int k = 0; k < petoverall.Comment_Num; k++)
                    {
                        petoverall.comments[k] = new Pet2.Comment("", DateTime.Now,"","","");
                        petoverall.comments[k].comment_time = Convert.ToDateTime(reader["comment_time"]);
                        petoverall.comments[k].comment_contents = reader["comment_contents"].ToString();
                        petoverall.comments[k].commenter = reader["user_name"].ToString();
                        petoverall.comments[k].commenter_id = reader["commenter"].ToString();
                        petoverall.comments[k].commenter_avatar = UserServer.GetAvatar(reader["commenter"].ToString());
                        reader.Read();
                    }
                    if (pet.Pet_ID == "-1")
                        throw new Exception("不存在的宠物！");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                connection.Close();
            }

            return petoverall;
        }

        public static DataTable GetAdoptPets(string user_id)
        {
            string query = "select adopt_apply.pet_id,pet_name,avatar,TRUNC(MONTHS_BETWEEN(SYSDATE, birthdate) / 12) AS age" +
                $",sex,CASE WHEN CENSOR_STATE = 'invalid' THEN '无效' WHEN CENSOR_STATE = 'legitimate' " +
                $"THEN '通过' WHEN CENSOR_STATE = 'to be censored' THEN '待审核' WHEN CENSOR_STATE = 'aborted' " +
                $"then '未通过' end as censor_state from adopt_apply left join pet on pet.pet_id=adopt_apply.pet_id " +
                $"where adopter_id={user_id}";
            return DBHelper.ShowInfo(query);
        }
    }
}
