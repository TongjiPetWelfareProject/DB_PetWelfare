using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using PetFoster.DAL;
using PetFoster.Model;
using System.Data;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        public class Doctor
        {
            public string? id { get; set; }
            public string? name { get; set; }
            public string? phone { get; set; }
            public double workingHours { get; set; }
            public string? salary { get; set; }
        }
        private List<Doctor> ConvertDataTableToVetList(DataTable dataTable)
        {
            List<Doctor> vetList = new List<Doctor>();

            foreach (DataRow row in dataTable.Rows)
            {
                // 使用PadLeft方法确保始终有两位数

                var doctor = new Doctor
                {
                    id = row["vet_id"].ToString(),
                    name = row["vet_name"].ToString(),
                    phone = row["tel"].ToString(),
                    workingHours = Convert.ToDouble(row["working_hours"]),
                    salary = row["salary"].ToString() + "￥"
                };
                vetList.Add(doctor);
            }
            return vetList;
        }

        // GET: api/<DoctorController>
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("doctor")]
        public IActionResult Get()
        {
            try
            {
                DataTable vetProfiles = VetManager.ShowVetProfile();
                List<Doctor> vets = ConvertDataTableToVetList(vetProfiles);
                return Ok(vets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("add-doctor")]
        public IActionResult AddEmployee([FromBody] Doctor vet)
        {
            // 这个函数用来接受添加员工请求，由于前端输入的是工作时长，而后端需要的是工作起始时间，这个地方你们斟酌一下
            if (vet == null)
            {
                return BadRequest("Empty Data.");
            }
            if (vet.name == null)
            {
                return BadRequest("Empty Employee Name.");
            }
            if (!ConvertTools.ConvertCurrencyStringToDouble(vet.salary, out double salary))
            {
                return BadRequest("Invalid Salary Format");
            }
            /*
            if (!ConvertTools.ConvertHourStringToDouble(vet.workingHours, out double hours))
            {
                return BadRequest("Invalid Working Hours Format.");
            }*/
            try
            {
                VetManager.RecruitVet(vet.name, (decimal)salary, vet.phone, vet.workingHours);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /*
        // GET api/<DoctorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST api/<DoctorController>
        [HttpPost]
        public IEnumerable<Doctor> Post()
        {
            
        }
        */
        // PUT api/<DoctorController>/5
        [Authorize(Policy = "AdminOnly")]
        [HttpPut("edit-doctor/{doctorId}")]
        public IActionResult Put(int doctorId, [FromBody] Doctor vet)
        {
            if (vet == null)
            {
                return BadRequest("Empty Data.");
            }
            if (vet.id == null || !int.TryParse(vet.id, out int vid))
            {
                return BadRequest("Invalid Vet Id.");
            }
            if (vet.name == null)
            {
                return BadRequest("Empty Vet Name.");
            }
            if (!ConvertTools.ConvertCurrencyStringToDouble(vet.salary, out double salary))
            {
                return BadRequest("Invalid Salary Format");
            }/*
            if (!ConvertTools.ConvertHourStringToDouble(vet.workingHours, out double hours))
            {
                return BadRequest("Invalid Working Hours Format.");
            }*/
            try
            {
                VetManager.UpdateVet(vet.id, vet.name, salary,
                    vet.phone, vet.workingHours);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<DoctorController>/5
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("delete-doctor/{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid Employee Id.");
            }
            try
            {
                //1. 删除公告
                AppointmentServer.DeleteAppointment(id.ToString());
                TreatmentServer.DeleteTreatment(id.ToString());
                VetServer.DeleteVet(id.ToString());
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
    }
}
