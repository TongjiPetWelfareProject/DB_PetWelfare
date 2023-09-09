using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using PetFoster.DAL;
using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class ManagePetController : ControllerBase
    {
        private readonly FileHelper _fileHelper;
        public ManagePetController(FileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }
        // GET: api/<ManagePetController>
        [Authorize(Policy = "AdminOnly")]
        [HttpGet("petlist")]
        public IActionResult Get()
        {
            DataTable dt = PetManager.ShowPetProfile();
            //List<Dictionary<string, object>> dataRows = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {

                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ColumnName == "VACCINE")
                    {
                        row[column.ColumnName] = row[column.ColumnName].ToString() == "Y" ? "已接种" : "未接种";
                    }
                    else if (column.ColumnName == "HEALTH_STATE")
                    {
                        row[column.ColumnName] = JsonHelper.TranslateToCn(row[column.ColumnName].ToString(), "health_state");
                    }
                    else if (column.ColumnName == "SEX")
                    {
                        row[column.ColumnName] = row[column.ColumnName].ToString() == "F" ? "女" : "男";
                    }
                    else if (column.ColumnName == "SPECIES")
                    {
                        row[column.ColumnName] = row[column.ColumnName].ToString() == "dog" ? "狗" : "猫";
                    }
                    else if (column.ColumnName == "STATUS"||column.ColumnName=="SOURCE")
                    {
                        row[column.ColumnName] = JsonHelper.TranslateToCn(row[column.ColumnName].ToString(), "source");
                        if(column.ColumnName == "STATUS")
                        column.ColumnName = "SOURCE"; 
                    }
                }
                if (row["SPECIES"].ToString() == "狗")
                    row["PSIZE"] = JsonHelper.TranslateToCn(row["PSIZE"].ToString(), "size");
                else
                    row["PSIZE"] = "-";
            }
            //foreach (DataRow row in dt.Rows)
            //{
            //    Dictionary<string, object> rowData = new Dictionary<string, object>();
            //    foreach (DataColumn column in dt.Columns)
            //    {
            //        if (column.ColumnName != "AVARTAR")
            //        {
            //            rowData[column.ColumnName] = row[column];
            //        }
            //    }
            //    byte[] avatarBytes = (byte[])row["AVATAR"];
            //    string base64Avatar = Convert.ToBase64String(avatarBytes);
            //    rowData["AVATAR"] = base64Avatar;
            //    dataRows.Add(rowData);
            //}
            string json = ConvertTools.DataTableToJson(dt);
            json = json.Replace("\\", "//").Replace("\\", "/"); //防止逃逸字符
            return Content(json, "application/json");
        }
        /*
        // GET api/<ManagePetController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */
        public class PetModel
        {
            public string? id { get; set; }
            public string? petname { get; set; }
            public string? breed { get; set; }
            public string? size { get; set; }
            public int age { get; set; }
            public string? sex { get; set; }
            public string? popularity { get; set; }
            public string? health { get; set; }
            public string? vaccine { get; set; }
            public string? from { get; set; }
        }
        public class PetModelWithImgs
        {
            public string? id { get; set; }
            public string? petname { get; set; }
            public string? breed { get; set; }
            public string? size { get; set; }
            public int age { get; set; }
            public string? sex { get; set; }
            public string? popularity { get; set; }
            public string? health { get; set; }
            public string? vaccine { get; set; }
            public string? from { get; set; }
            public List<IFormFile>? filename { get; set; }
        }

        // POST api/<ManagePetController>
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("add-pet")]
        public IActionResult AddPet([FromBody] PetModel pet)
        {
            // 这个函数用来接受添加员工请求，由于前端输入的是工作时长，而后端需要的是工作起始时间，这个地方你们斟酌一下
            if (pet == null)
            {
                return BadRequest("Empty Data.");
            }
            if (pet.petname == null)
            {
                return BadRequest("Empty Pet Name.");
            }
            if(pet.breed == null)
            {
                return BadRequest("Empty Pet Specis.");
            }
            if(pet.size == null)
            {
                return BadRequest("Empty Pet Size.");
            }
            else
            {
                pet.size = pet.size.ToLower();
                if(pet.size != "small" && pet.size != "medium" && pet.size != "large")
                {
                    return BadRequest("Invalid Pet Size.");
                }
            }
            string health = JsonHelper.TranslateToEn(pet.health,"health_state");
            if(!string.IsNullOrEmpty(pet.health))
            {
                if((health = PetManager.GetHealth(pet.health)) == null)
                {
                    return BadRequest("Invalid Health State.");
                }
            }
            bool vaccine;
            if(pet.vaccine == "已接种")
            {
                vaccine = true;
            }
            else if(pet.vaccine =="未接种")
            {
                vaccine = false;
            }
            else
            {
                return BadRequest("Invalid Vaccine Status.");
            }
            string sex;
            if (pet.sex == "公" || pet.sex == "男")
            {
                sex = "男";
            }
            else if (pet.sex == "母" || pet.sex == "女")
            {
                sex = "女";
            }
            else
            {
                return BadRequest("Invalid Sex.");
            }
            /*
            if (!ConvertTools.ConvertHourStringToDouble(employee.workingHours, out double hours))
            {
                return BadRequest("Invalid Working Hours Format.");
            }*/
            try
            {
                PetManager.RegisterPet(pet.petname, pet.breed, pet.size, pet.age, null, health, vaccine, sex);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<ManagePetController>/5
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("edited-pet")]
        public async Task<IActionResult> Post([FromForm] PetModelWithImgs pet)
        {
            if (pet == null)
            {
                return BadRequest("Empty Data.");
            }
            if (pet.id == null || !int.TryParse(pet.id, out int pid))
            {
                return BadRequest("Invalid Pet Id.");
            }
            string? health;
            if(!string.IsNullOrEmpty(pet.health))
            {
                if ((health = PetManager.GetHealth(pet.health)) == null)
                {
                    return BadRequest("Invalid Health State.");
                }
            }
            else
            {
                health = null;
            }
            bool? vaccine;
            if (pet.vaccine == "已接种")
            {
                vaccine = true;
            }
            else if (pet.vaccine == "未接种")
            {
                vaccine = false;
            }
            else
            {
                return BadRequest("Invalid Vaccine Status.");
            }
            try
            {
                // FileNames为上传的图片URL
                List<string> FileNames = new List<string>();
                if (pet.filename != null)
                {
                    FileNames = await _fileHelper.SaveImagesAsync(pet.filename);
                    PetManager.UpdatePet(pet.id, pet.petname, pet.health, vaccine.Value, FileNames[0]);
                }
                else
                {
                    PetManager.UpdatePet(pet.id, pet.petname, pet.health, vaccine.Value);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public class DeletePetRequest
        {
            public string? pid { get; set; }
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("delete-pet")]
        public IActionResult Delete([FromBody] DeletePetRequest request)
        {
            if (request.pid == null || !int.TryParse(request.pid, out int uid))
            {
                return BadRequest("Invalid Pet Id.");
            }
            
            try
            {
                string source = DBHelper.GetScalar($"select status from pet_source where pet_id={uid}");
                if (source == "Wander")
                {
                    DBHelper.ExecuteNonScalar($"delete from appointment where pet_id={uid}");
                    DBHelper.ExecuteNonScalar($"delete from adopt_apply where pet_id={uid}");
                    DBHelper.ExecuteNonScalar($"delete from adopt where pet_id={uid}");
                    DBHelper.ExecuteNonScalar($"delete from accommodate where pet_id={uid}");
                    DBHelper.ExecuteNonScalar($"delete from foster where pet_id={uid}");
                    DBHelper.ExecuteNonScalar($"delete from collect_pet_info where pet_id={uid}");
                    DBHelper.ExecuteNonScalar($"delete from like_pet where pet_id={uid}");
                    DBHelper.ExecuteNonScalar($"delete from comment_pet where pet_id={uid}");
                    DBHelper.ExecuteNonScalar($"delete from pet where pet_id={uid}");
                    return Ok();
                }
                else
                    return BadRequest("与客户有关的宠物禁止删除！");
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
