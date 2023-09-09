using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using PetFoster.Model;
using System.Data;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikepetController : ControllerBase
    {
        public class LikepetModel
        {
            public string? pet_id { get; set; }
            public string? liked_date { get; set; }
            public string? liked_time { get; set; }
        }
        /*
        // GET: api/<LikepetController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LikepetController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */

        // POST api/<LikepetController>
        [HttpPost]
        public IActionResult Post([FromBody] string user_id)
        {
            DataTable dt = LikePetManager.ShowLikePet();
            List<LikepetModel> liked_pets = new List<LikepetModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LikepetModel PetItem = new LikepetModel();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName.ToLower() == "pet_id")
                    {
                        PetItem.pet_id = dt.Rows[i][j].ToString();
                    }
                    else if (dt.Columns[j].ColumnName.ToLower() == "liked_date")
                    {
                        PetItem.liked_date = dt.Rows[i][j].ToString();
                    }
                    else if (dt.Columns[j].ColumnName.ToLower() == "liked_time")
                    {
                        PetItem.liked_time = dt.Rows[i][j].ToString();
                    }
                }

                liked_pets.Add(PetItem);
            }

            string jsondata = JsonSerializer.Serialize(liked_pets);
            Console.WriteLine(jsondata);

            return Ok(jsondata);
        }

        /*
        // PUT api/<LikepetController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LikepetController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
