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
using System.Collections.Generic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//8.24头文件再问一下
namespace Glue.Controllers
{
    [Route("api")]
    [ApiController]
    public class ManageHotController : Controller
    {
        public class TopPet
        {
            public string id { get; set; }
            public string name { get; set; }
            public int views { get; set; }
            public int likes { get; set; }
            public string image { get; set; }
            public TopPet()
            {
                id = "";
                name = "";
                views = 0;
                likes = 0;
                image = "";
            }
        }

        //added by rqx 8.24
        // 获取点赞量加阅读量最高的10个宠物信息，包括id、名字、阅读量、点赞量
        [HttpGet("top-pets")]
        public IActionResult GetTopPets()
        {
            try
            {
                // 在这里编写获取点赞量和阅读量最高的10个宠物信息的逻辑
                List<TopPet> topPets = _GetTopNPets(10);
                return Ok(topPets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("topAdoptPets")]
        public IActionResult GetTopAdoptPets()
        {
            try
            {
                // 在这里编写获取点赞量和阅读量最高的10个宠物信息的逻辑
                List<TopPet> topPets = _GetTopNPets(3);
                return Ok(topPets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /*
        // 发布三个人气榜
        [HttpPost("publish-popularity-chart")]
        public IActionResult PublishPopularityChart([FromBody] List<int> selectedPetIds)
        {
            try
            {
                // 在这里编写发布人气榜的逻辑，使用selectedPetIds参数
                _PublishPopularity(selectedPetIds);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        *//*

        // 获取用户发布的人气榜宠物信息
        [HttpGet("user-popularity-pets/{userId}")]
        public IActionResult GetUserPopularityPets(int userId)
        {
            try
            {
                // 在这里编写获取用户发布的人气榜宠物信息的逻辑，使用userId参数
                //List<Pet> userPopularityPets = GetUserPopularityPets(userId);
                return Ok(_GetUserPopularityPets(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }*/

        // 模拟获取点赞量和阅读量最高的10个宠物信息的方法
        private List<TopPet> _GetTopNPets(int n)
        {
            // 在这里编写获取点赞量和阅读量最高的10个宠物信息的逻辑
            // 返回一个包含宠物信息的List<Pet>对象

            DataTable dt = PetManager.ShowBoards();
            List<TopPet> TopPetsList = new List<TopPet>();
            int cnt = 0; // 记录已经加进列表的个数
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TopPet TopPetItem = new TopPet();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName.ToLower() == "pet_id")
                    {
                        TopPetItem.id = dt.Rows[i][j].ToString();
                    }
                    else if (dt.Columns[j].ColumnName.ToLower() == "pet_name")
                    {
                        TopPetItem.name = dt.Rows[i][j].ToString();
                    }
                    else if (dt.Columns[j].ColumnName.ToLower() == "read_num")
                    {
                        TopPetItem.views = Convert.ToInt32(dt.Rows[i][j]);
                    }
                    else if (dt.Columns[j].ColumnName.ToLower() == "like_num")
                    {
                        TopPetItem.likes = Convert.ToInt32(dt.Rows[i][j]);
                    }
                    else if (dt.Columns[j].ColumnName.ToLower() == "avatar")
                    {
                        TopPetItem.image = dt.Rows[i][j].ToString();
                    }
                }
                TopPetsList.Add(TopPetItem);
                cnt++;
                if(cnt >= n)
                {
                    break;
                }
            }
            return TopPetsList;
        }

        // 模拟发布人气榜的方法
        private void _PublishPopularity(List<int> selectedPetIds)
        {
            // 在这里编写发布人气榜的逻辑，使用selectedPetIds参数
        }

        // 模拟获取用户发布的人气榜宠物信息的方法
        private List<Pet> _GetUserPopularityPets(int userId)
        {
            // 在这里编写获取用户发布的人气榜宠物信息的逻辑，使用userId参数
            // 返回一个包含宠物信息的List<Pet>对象
            return new List<Pet>();
        }

    }
}
