using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using System.Data;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Glue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentpetController : ControllerBase
    {
        public class CommentpetModel
        {
            public string? pet_id { get; set; }
            public string? comment_date { get; set; }
            public string? comment_time { get; set; }
            public string? comment_contents { get; set; }
        }
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            DataTable dt = CommentPetManager.ShowCommentPet();
            List<CommentpetModel> comment_pets = new List<CommentpetModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CommentpetModel PetItem = new CommentpetModel();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName.ToLower() == "pet_id")
                    {
                        PetItem.pet_id = dt.Rows[i][j].ToString();
                    }
                    else if (dt.Columns[j].ColumnName.ToLower() == "comment_date")
                    {
                        PetItem.comment_date = dt.Rows[i][j].ToString();
                    }
                    else if (dt.Columns[j].ColumnName.ToLower() == "commented_time")
                    {
                        PetItem.comment_time = dt.Rows[i][j].ToString();
                    }
                    else if (dt.Columns[j].ColumnName.ToLower() == "comment_contents")
                    {
                        PetItem.comment_contents = dt.Rows[i][j].ToString();
                    }
                }

                comment_pets.Add(PetItem);
            }

            string jsondata = JsonSerializer.Serialize(comment_pets);
            Console.WriteLine(jsondata);

            return Ok(jsondata);
        }
    }
}
