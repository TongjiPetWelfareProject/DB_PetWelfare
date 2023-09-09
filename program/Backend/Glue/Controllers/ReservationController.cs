using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using PetFoster.DAL;
using System.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        public class AppointmentData
        {
            public string? name { get; set; }
            public string? kind { get; set; }
            public string? date1 { get; set; }
            public string? desc { get; set; }
            public string? selectedDoctorID { get; set; }
            public string? isOld { get; set; }
            public string? pet_kind { get; set; }
            public string? petId { get; set; }
            public string? userId { get; set; }
        }
        public class TreatedPetNames
        {
            public string id { get; set; }
            public string name { get; set; }
            public string kind { get; set; }
            public TreatedPetNames(string pet_id,string pet_name, string kind)
            {
                id = pet_id;
                name = pet_name;
                this.kind = kind;
            }
        }
        
        // GET: api/<ReservationController>
        [HttpGet("getPetInfo")]
        public IActionResult Get()
        {
            string jsonstring;
            try
            {
                DataTable dt = PetManager.ShowPetNames();
                List<TreatedPetNames> PetNamesList = new List<TreatedPetNames>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string pet_id = "", pet_name = "",kind="";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Columns[j].ColumnName.ToLower() == "pet_id")
                        {
                            pet_id = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "pet_name")
                        {
                            pet_name = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName.ToLower() == "species")
                        {
                            kind = dt.Rows[i][j].ToString();
                        }
                    }
                    pet_id = (pet_id == null ? "" : pet_id);
                    pet_name = (pet_name == null ? "" : pet_name);
                    kind = (kind == null ? "" : kind);
                    TreatedPetNames PetNameItem = new TreatedPetNames(pet_id, pet_name,kind);
                    PetNamesList.Add(PetNameItem);
                }
                jsonstring = "{\"data\":"+JsonSerializer.Serialize(PetNamesList)+"}";
                Console.WriteLine(jsonstring);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(jsonstring);
        }
        
        /*
        // GET api/<MedicalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */

        // POST api/<MedicalController>
        [Authorize]
        [HttpPost("submitAppointment")]
        public IActionResult Post([FromBody] AppointmentData appointment_data)
        {
            /* check data */
            if(appointment_data == null)
            {
                return BadRequest("Invalid Data");
            }
            if(appointment_data.userId == null)
            {
                return BadRequest("未登录");
            }
            if(appointment_data.isOld == null)
            {
                return BadRequest("Empty isOld");
            }
            if (appointment_data.selectedDoctorID == null)
            {
                return BadRequest("empty doctor");
            }
            DateTime? date = ConvertTools.StringConvertToDate(appointment_data.date1);
            if (date == null)
            {
                return BadRequest("Failed to parse the date.");
            }
            string desc;
            if (appointment_data.desc == null)
            {
                desc = "";
            }
            else
            {
                desc = appointment_data.desc;
            }

            if (appointment_data.isOld == "它未在此治疗过") // new pet
            {
                if (appointment_data.name == null)
                {
                    return BadRequest("Empty pet name for new pet");
                }
                if(appointment_data.pet_kind == null)
                {
                    return BadRequest("Empty pet species for new pet");
                }
                /* do the business */
                Console.WriteLine("name="+appointment_data.name);
                Console.WriteLine("pet_kind="+appointment_data.pet_kind);
                try
                {
                    AppointmentManager.Appointment(appointment_data.userId,
                        appointment_data.name,
                        appointment_data.pet_kind,
                        appointment_data.selectedDoctorID,
                        (DateTime)date,
                        desc);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
                return Ok();
            }
            else // existing pet
            {
                if(appointment_data.petId == null)
                {
                    return BadRequest("Empty petId for existing pet");
                }
                /* do the business */
                Console.WriteLine("petId=" + appointment_data.petId);
                try
                {
                    AppointmentManager.Appointment2(appointment_data.userId,
                        appointment_data.petId,
                        appointment_data.selectedDoctorID,
                        (DateTime)date,
                        desc);
                }
                catch(Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
                return Ok();
            }
        }

        /*
        // PUT api/<MedicalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MedicalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
