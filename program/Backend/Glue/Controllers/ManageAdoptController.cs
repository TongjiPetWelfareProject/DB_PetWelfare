using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using System.Data;
using System.Text.Json;
using PetFoster.DAL;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class ManageAdoptController : ControllerBase
    {

        public class AdoptionRecord
        {
            public string date { get; set; }
            public string petId { get; set; }
            public string petName { get; set; }
            public string userId { get; set; }
            public string userName { get; set; }
            public string reason { get; set; }
            public string censor_status { get; set; }
            public AdoptionRecord()
            {
                date = "";
                petId = "";
                petName = "";
                userId = "";
                userName = "";
                reason = "";
                censor_status = "";
            }
        }

        // GET: api/<ManageAdoptController>
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("manage-adopt")]
        public IActionResult Get()
        {
            int censorstate = 0; //默认未审核

            string jsondata;
            try
            {
                DataTable dt = AdoptApplyManager.ShowCensorAdopt();
                List<AdoptionRecord> RecordList = new List<AdoptionRecord>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AdoptionRecord RecordItem = new AdoptionRecord();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.ToLower() == "apply_date")
                        {
                            RecordItem.date = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "pet_id")
                        {
                            //RecordItem.petId = PetServer.GetName(dt.Rows[i][j].ToString());
                            RecordItem.petId = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "adopter_id")
                        {
                            //RecordItem.userId = UserServer.GetName(dt.Rows[i][j].ToString());
                            RecordItem.userId = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "reason")
                        {
                            RecordItem.reason = dt.Rows[i][j].ToString().Replace("Y","是").Replace("N","否")
                                .Replace("M","男").Replace("F","女");
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "censor_status")
                        {
                            RecordItem.censor_status = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "user_name")
                        {
                            RecordItem.userName = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "pet_name")
                        {
                            RecordItem.petName = dt.Rows[i][j].ToString();
                        }
                    }
                    RecordList.Add(RecordItem);
                }

                jsondata = JsonSerializer.Serialize(RecordList);
                Console.WriteLine("申请领养数据完成JSON序列化");
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(jsondata);
        }

        // PATCH: api/<ManageAdoptController>
        [Authorize(Policy = "AdminOnly")]
        [HttpPatch("manage-adopt-update")]
        public IActionResult UpdateAdoptRecord([FromBody] AdoptionRecord record)
        {
            if (record == null)
            {
                return BadRequest("Invalid data.");
            }
            // Console.WriteLine(record.date);
            DateTime? rdate = ConvertTools.StringConvertToDate(record.date);
            if (rdate == null)
            {
                return BadRequest("Failed to parse the date.");
            }
            //Console.WriteLine(record.userId);
            //Console.WriteLine(record.petId);
            int pid;
            if (!int.TryParse(record.petId, out pid))
            {
                return BadRequest("Invalid petId");
            }
            if(record.userId == null)
            {
                return BadRequest("Invalid userId");
            }
            bool pass;
            if(record.censor_status.StartsWith("abor"))
            {
                pass = false;
            }
            else if(record.censor_status == "legitimate")
            {
                pass = true;
            }
            else
            {
                return BadRequest("Invalid censor_status");
            }
       
            try
            {
                AdoptApplyManager.CensorAdopt(record.userId, pid, (DateTime)rdate, pass, out int err_code);
                if(err_code == 1)
                {
                    return NotFound("宠物已经全被领养或寄养（正在申请寄养）走了!");
                }
                else if(err_code == 2)
                {
                    return StatusCode(500, "审核过程异常!");
                }
                else if(err_code == 3)
                {
                    return NotFound("不存在该申请人!");
                }
                else if(err_code != 0)
                {
                    return StatusCode(500, "未知异常！");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /*

        // GET api/<ManageAdoptController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ManageAdoptController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ManageAdoptController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ManageAdoptController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
