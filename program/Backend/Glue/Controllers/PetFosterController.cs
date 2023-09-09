using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using PetFoster.BLL;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class PetFosterController : ControllerBase
    {
        public class FosterData
        {
            public string? user { get; set; }
            public string? name { get; set; }
            public string? type { get; set; }
            public string? size { get; set; }
            public string? date { get; set; }
            public int num { get; set; }
            public string? remark { get; set; }
        }

        /*

        // GET: api/<PetFoster>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PetFoster>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        */

        // POST api/<PetFoster>
        [Authorize]
        [HttpPost("pet-foster")]
        public IActionResult FosterApply([FromBody] FosterData foster_table)
        {
            DateTime? rdate = ConvertTools.StringConvertToDate(foster_table.date);
            if (rdate == null)
            {
                return BadRequest("Failed to parse the date.");
            }
            DateTime date = rdate.Value;
            if (date.Subtract(DateTime.Now) <= TimeSpan.FromDays(0) || date.Subtract(DateTime.Now) >= TimeSpan.FromDays(7))
            {
                throw new Exception("请从今天开始的一周内申请领养！");
            }
            int status = FosterManager.ApplyFoster(foster_table.user, foster_table.name, foster_table.type, foster_table.size, (DateTime)date, foster_table.num, foster_table.remark);
            if(status == 3)
            {
                return Conflict("房间已满！");
            }
            else if(status == 4)
            {   
                return StatusCode(500, "系统故障");
            }
            else
            {
                return Ok();
            }
        }
    }
}
