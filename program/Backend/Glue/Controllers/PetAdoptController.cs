using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using PetFoster.Model;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using PetFoster.DAL;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class PetAdoptController : ControllerBase
    {
        public class AdoptData
        {
            public string? user { get; set; }
            public string? pet { get; set; }
            public bool gender { get; set; }
            public decimal pet_exp { get; set; }
            public bool long_term_care { get; set; }
            public bool w_to_treat { get; set; }
            public decimal d_care_h { get; set; }
            public string? P_caregiver { get; set; }
            public decimal f_popul { get; set; }
            public bool be_children { get; set; }
            public bool accept_vis { get; set; }
        }

        // GET: api/<PetAdoptController>
        [HttpGet("pet-adopt-list")]
        public IActionResult Get()
        {
            DataTable dt = AdoptManager.ShowPetProfile();
            string json = DataTableToJson(dt);
            json=json.Replace("\\", "//").Replace("\\", "/");
            return Content(json, "application/json");
        }

        public class PetWithoutAvartar
        {
            public string Pet_ID { get; set; }
            public string Pet_Name { get; set; }
            public string Species { get; set; }
            public DateTime birthdate { get; set; }
            public string Health_State { get; set; }
            public string Vaccine { get; set; }
            public decimal Read_Num { get; set; }
            public decimal Like_Num { get; set; }
            public decimal Collect_Num { get; set; }
            public string Avatar { get; set; }
            public PetWithoutAvartar()
            {
                Pet_ID = "-1";
                Pet_Name = "宠物已注销";
                Avatar = "http://localhost:6001/uploads/2023/09/02/f36fa6ba-78fe-4179-be4a-6ba0a6e0d38e.png";
            }
            public PetWithoutAvartar(PetData.PETRow prow)
            {
                Pet_ID = prow.PET_ID;
                Pet_Name = prow.PET_NAME;
                Species = prow.BREED;
                birthdate = prow.BIRTHDATE;
                Health_State = prow.HEALTH_STATE;
                Vaccine = prow.VACCINE;
                Read_Num = prow.READ_NUM;
                Like_Num = prow.LIKE_NUM;
                Collect_Num = prow.COLLECT_NUM;
                Avatar = "http://localhost:6001/uploads/2023/09/02/f36fa6ba-78fe-4179-be4a-6ba0a6e0d38e.png";
            }
        }
        public class Pet2WithoutAvartar
        {
            public Pet2WithoutAvartar()
            {

            }
            public PetWithoutAvartar original_pet;
            public int Popularity;
            public bool sex;
            public string Psize;//宠物大小
            public int Comment_Num;
            public Pet2.Comment[] comments;
        }

        public class Pet2WithHaveLiked: Pet2WithoutAvartar
        {
            public bool have_liked;
            public bool have_collected;
        }
        public class DetailsRequest
        {
            public int userId;
        }
        // GET api/<PetAdoptController>
        [HttpGet("pet-details/{petId}")]
        public IActionResult Get(int petId)
        {
            try
            {
                Pet2 pet = AdoptApplyManager.RetrievePet(petId);
                try
                {
                    string jsondata = Newtonsoft.Json.JsonConvert.SerializeObject(pet);
                    PetServer.ReadPet(pet.original_pet.Pet_ID);
                    Console.WriteLine($"{pet.original_pet.Pet_Name}的阅读量+1!");
                    return Ok(jsondata);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return BadRequest("无法转换为Json");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<PetAdoptController>
        [Authorize]
        [HttpPost("pet-adopt")]
        public IActionResult Post([FromBody] AdoptData adopt_table)
        {
            if (adopt_table == null)
            {
                return BadRequest("Invalid data.");
            }
            if(adopt_table.user == null)
            {
                return BadRequest("Invalid userId.");
            }
            if(adopt_table.pet == null)
            {
                return BadRequest("Invalid petId.");
            }

            bool pet_exp = (adopt_table.pet_exp > 0) ? true : false;

            try
            {
                if (AdoptApplyManager.ApplyAbuseOrNot(adopt_table.user, adopt_table.pet))
                    return BadRequest("重复申请领养该重复/该宠物领养热度过高");
                AdoptApplyManager.ApplyAdopt(adopt_table.user, adopt_table.pet,
                    adopt_table.gender, pet_exp, adopt_table.long_term_care,
                    adopt_table.w_to_treat, adopt_table.d_care_h, adopt_table.P_caregiver,
                    adopt_table.f_popul, adopt_table.be_children, adopt_table.accept_vis);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        private string DataTableToJson(DataTable table)
        {
            var jsonString = new StringBuilder();

            if (table.Rows.Count > 0)
            {
                jsonString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jsonString.Append("{");

                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        jsonString.AppendFormat("\"{0}\":\"{1}\"",
                            table.Columns[j].ColumnName,
                            table.Rows[i][j]);

                        if (j < table.Columns.Count - 1)
                        {
                            jsonString.Append(",");
                        }
                    }

                    jsonString.Append("}");
                    if (i < table.Rows.Count - 1)
                    {
                        jsonString.Append(",");
                    }
                }
                jsonString.Append("]");
            }

            return jsonString.ToString();
        }

    }
}
