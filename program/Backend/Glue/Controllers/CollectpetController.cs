using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using System.Data;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectpetController : ControllerBase
    {
        public class CollectpetModel
        {
            public string? pet_id { get; set; }
            public string? collected_date { get; set; }
            public string? collected_time { get; set; }
        }
        /*
        // GET: api/<CollectpetController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CollectpetController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */

        // POST api/<CollectpetController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            DataTable dt = CollectPetInfoManager.ShowCollectPetInfo();
            List<CollectpetModel> collected_pets = new List<CollectpetModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CollectpetModel PetItem = new CollectpetModel();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName.ToLower() == "pet_id")
                    {
                        PetItem.pet_id = dt.Rows[i][j].ToString();
                    }
                    else if (dt.Columns[j].ColumnName.ToLower() == "collect_date")
                    {
                        PetItem.collected_date = dt.Rows[i][j].ToString();
                    }
                    else if (dt.Columns[j].ColumnName.ToLower() == "collected_time")
                    {
                        PetItem.collected_time = dt.Rows[i][j].ToString();
                    }
                }

                collected_pets.Add(PetItem);
            }

            string jsondata = JsonSerializer.Serialize(collected_pets);
            Console.WriteLine(jsondata);

            return Ok(jsondata);
        }

        /*
        // PUT api/<CollectpetController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CollectpetController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
