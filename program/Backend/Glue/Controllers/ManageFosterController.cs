using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using PetFoster.DAL;
using System.Data;
using System.Text.Json;
//using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class ManageFosterController : ControllerBase
    {
        public class FosterRecord
        {
            public string date { get; set; }
            public string petId { get; set; }
            public string petName { get; set; }
            public string userId { get; set; }
            public string userName { get; set; }
            public int days { get; set; }
            public string censor_status { get; set; }
            public FosterRecord()
            {
                date = "";
                petId = "";
                petName = "";
                userId = "";
                userName = "";
                days = 0;
                censor_status = "";
            }
            public void print()
            {
                Console.WriteLine("date:" + date + "\n");
                Console.WriteLine("petId:" + petId + "\n");
                Console.WriteLine("petName:" + petName + "\n");
                Console.WriteLine("userId:" + userId + "\n");
                Console.WriteLine("userName:" + userName + "\n");
                Console.WriteLine("days:" + days + "\n");
                Console.WriteLine("censor_status:" + censor_status + "\n");
            }
        }

        // GET: api/<ManageFosterController>
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("manage-foster")]
        public IActionResult Get()
        {
            string censorStr;

            string jsondata;
            try
            {
                DataTable dt = FosterManager.CensorFoster(out censorStr);
                List<FosterRecord> RecordList = new List<FosterRecord>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    FosterRecord RecordItem = new FosterRecord();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.ToLower() == "duration")
                        {
                            RecordItem.days = Convert.ToInt32(dt.Rows[i][j]);
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "fosterer")
                        {
                            RecordItem.userId = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "pet_id")
                        {
                            RecordItem.petId = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "startdate")
                        {
                            RecordItem.date = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "censor_state")
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
                /*
                foreach (FosterRecord Record in RecordList)
                {
                    Console.WriteLine(Record.date+Record.petId+Record.userId+Record.days.ToString()+Record.censor_status);
                }
                */
                jsondata = JsonSerializer.Serialize(RecordList);
                Console.WriteLine("已成功Json序列化");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(jsondata);
        }

        // PATCH: api/<ManageFosterController>
        [Authorize(Policy = "AdminOnly")]
        [HttpPatch("manage-foster-update")]
        public IActionResult UpdateFosterRecord([FromBody] FosterRecord record)
        {
            if (record == null)
            {
                return BadRequest("Invalid data.");
            }
            //record.print();
            DateTime? date = ConvertTools.StringConvertToDate(record.date);
            if (date == null)
            {
                return BadRequest("Failed to parse the date.");
            }
            if (!int.TryParse(record.petId, out int pid))
            {
                return BadRequest("Invalid petId");
            }
            if (!int.TryParse(record.userId, out int uid))
            {
                return BadRequest("Invalid userId");
            }
            try
            {
                FosterManager.Censorship(record.userId, record.petId, (DateTime)date, record.censor_status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        /*

        // GET api/<ManageFosterController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ManageFosterController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ManageFosterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ManageFosterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        */
    }
}
