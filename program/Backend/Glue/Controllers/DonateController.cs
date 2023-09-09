using Microsoft.AspNetCore.Mvc;
using PetFoster.BLL;
using static Glue.Controllers.RegisterController;
using Microsoft.AspNetCore.Authorization;

namespace Glue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonateController : Controller
    {
        public class DonateModel
        {
            public string UserId { get; set; }
            public decimal Amount { get; set; }

            public DonateModel()
            {
                UserId = string.Empty;
                Amount = 0;
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult Donate([FromBody] DonateModel donateModel)
        {
            string userId = donateModel.UserId;
            decimal amount = donateModel.Amount;
            int status = DonationManager.Donate(userId, amount);
            if(status == 0) { return Ok("捐款成功"); }
            else if(status == -1) { return Unauthorized("金额信息不正确"); }
            else if(status == -2) { return Unauthorized("金额过大，请慎重考虑"); }
            else { return BadRequest(); }
        }
    }
}
