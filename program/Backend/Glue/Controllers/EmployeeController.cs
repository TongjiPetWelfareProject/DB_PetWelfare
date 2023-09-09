using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using PetFoster.DAL;
using PetFoster.Model;
using System.Data;
using Microsoft.AspNetCore.Authorization;

using static WebApplicationTest1.UserInfoController;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public class EmployeeModel
        {
            public string? id { get; set; }
            public string? name { get; set; }
            public string? phone {get; set; }
            public string? responsibility { get; set; }
            public double workingHours { get; set; }
            public string? salary { get; set; }
        }
        // GET: api/<EmployeeController>
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("employee")]
        public IActionResult Get()
        {
            try
            {
                DataTable dt = EmployeeManager.ShowProfile();
                List<EmployeeModel> EmployeesList = new List<EmployeeModel>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EmployeeModel EmployeeItem = new EmployeeModel();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.ToLower() == "employee_id")
                        {
                            EmployeeItem.id = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "employee_name")
                        {
                            EmployeeItem.name = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "phone_number")
                        {
                            EmployeeItem.phone = dt.Rows[i][j].ToString();
                        }
                        else if(dt.Columns[j].ColumnName.ToLower() == "duty")
                        {
                            
                            EmployeeItem.responsibility = JsonHelper.TranslateToCn(dt.Rows[i][j].ToString(), "duties");
                        }
                        else if(dt.Columns[j].ColumnName.ToLower() == "salary")
                        {
                            EmployeeItem.salary = dt.Rows[i][j].ToString()+"￥";
                        }
                        else if(dt.Columns[j].ColumnName.ToLower() == "working_hours")
                        {
                            //EmployeeItem.workingHours = dt.Rows[i][j].ToString()+"小时";
                            EmployeeItem.workingHours = Convert.ToDouble(dt.Rows[i][j]);
                        }
                    }
                    EmployeesList.Add(EmployeeItem);
                }
                //jsonstring = "{\"data\":" + JsonSerializer.Serialize(PetNamesList) + "}";
                //Console.WriteLine(jsonstring);
                return Ok(EmployeesList);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("add-employee")]
        public IActionResult AddEmployee([FromBody] EmployeeModel employee)
        {
            // 这个函数用来接受添加员工请求，由于前端输入的是工作时长，而后端需要的是工作起始时间，这个地方你们斟酌一下
            if (employee == null)
            {
                return BadRequest("Empty Data.");
            }
            if (employee.name == null)
            {
                return BadRequest("Empty Employee Name.");
            }
            if (!ConvertTools.ConvertCurrencyStringToDouble(employee.salary, out double salary))
            {
                return BadRequest("Invalid Salary Format");
            }
            /*
            if (!ConvertTools.ConvertHourStringToDouble(employee.workingHours, out double hours))
            {
                return BadRequest("Invalid Working Hours Format.");
            }*/
            int exit = UserServer.CheckUserPhoneExistence(employee.phone);

            if (exit == 1)
            {
                return BadRequest("该手机号已被使用");
            }
            try
            {
                EmployeeManager.RecruitEmployee(employee.name, (decimal)salary, employee.phone,
                    employee.responsibility, employee.workingHours);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /*
        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        */
        // PUT api/<EmployeeController>/5
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("edit-employee/{employeeId}")]
        public IActionResult Put(int employeeId, [FromBody] EmployeeModel employee)
        {
            if(employee == null)
            {
                return BadRequest("Empty Data.");
            }
            if(employee.id == null || !int.TryParse(employee.id,out int eid))
            {
                return BadRequest("Invalid Employee Id.");
            }
            if(employee.name == null)
            {
                return BadRequest("Empty Employee Name.");
            }
            if(!ConvertTools.ConvertCurrencyStringToDouble(employee.salary,out double salary))
            {
                return BadRequest("Invalid Salary Format");
            }
            /*
            if(!ConvertTools.ConvertHourStringToDouble(employee.workingHours,out double hours))
            {
                return BadRequest("Invalid Working Hours Format.");
            }
            */
            int exit = UserServer.CheckUserPhoneExistence(employee.phone);
            User status = UserServer.GetUserByTel(employee.phone);

            if (exit == 1)
            {
                if (status.User_ID == employee.id)
                {
                    try
                    {
                        try
                        {
                            EmployeeManager.UpdateEmployee(employee.id, employee.name, salary,
                                employee.phone, employee.responsibility, employee.workingHours);
                            return Ok();
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(500, ex.Message);
                        }
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
                EmployeeManager.UpdateEmployee(employee.id, employee.name, salary,
                    employee.phone, employee.responsibility, employee.workingHours);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<EmployeeController>/5
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("delete-employee/{employeeId}")]
        public IActionResult Delete(int employeeId)
        {
            if (employeeId<0)
            {
                return BadRequest("Invalid Employee Id.");
            }
            try
            {
                //BulletinServer.DeleteBulletins(employeeId.ToString());
                Console.WriteLine("已删除雇员发布的公告");
                EmployeeServer.DeleteEmployee(employeeId.ToString());
                Console.WriteLine("已删除雇员的记录");
                LikePetServer.DeleteLikePets(employeeId.ToString());
                Console.WriteLine("已删除点赞宠物的记录");
                CommentPetServer.DeleteCommentPets(employeeId.ToString());
                Console.WriteLine("已删除评论宠物的记录");
                CollectPetInfoServer.DeleteCollectPetInfos(employeeId.ToString());
                Console.WriteLine("已删除收藏宠物的记录");
                LikePostServer.DeleteLikePosts(employeeId.ToString());
                Console.WriteLine("已转移点赞帖子的记录");
                CommentPostServer.DeleteCommentPosts(employeeId.ToString());
                Console.WriteLine("已转移评论帖子的记录");
                AppointmentServer.DeleteAppointments(employeeId.ToString());
                Console.WriteLine("已清除该雇员的预约记录");
                AccommodateServer.DeleteAccommodates(employeeId.ToString());
                Console.WriteLine("已清除该雇员的宠物的居住记录");
                AdoptApplyServer.DeleteAdopts(employeeId.ToString());
                Console.WriteLine("已清除该雇员全部寄养申请记录");
                Console.WriteLine("已清除该雇员全部领养记录");
                FosterServer.DeleteFosters(employeeId.ToString());
                Console.WriteLine("已清除该雇员全部领养申请记录");
                DonationServer.DeleteDonations(employeeId.ToString());
                Console.WriteLine("已转移捐款记录");
                ForumPostServer.DeletePosts(employeeId.ToString());
                Console.WriteLine("已转移发布帖子的记录");
                UserServer.DeleteUser(employeeId.ToString());
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
