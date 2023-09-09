using Microsoft.AspNetCore.Mvc;

using PetFoster.BLL;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.OracleClient;
using PetFoster.DAL;
using PetFoster.Model;
using System.Text.Json;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Numerics;

namespace Glue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulletinController : Controller
    {
        [HttpGet]
        public IActionResult Bulletin()
        {
            List<Bulletin> bulletinlist = BulletinManager.GetAllBulletins();
            Console.WriteLine("收到公告请求");
            return Ok(bulletinlist);
        }
    }
}
