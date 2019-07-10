using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using SoclooAPI.Data;
using SoclooAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : BaseController
    {
        public PostsController(IConfiguration config, ILogger<PostsController> logger, DataContext context) :
            base(config, logger, context)
        { }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Post>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Post> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Post>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Post post)
        {

            await UnitOfWork.Repository<Post>().InsertAsync(post);

            return true;
        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Post post)
        {
            
            try
            {
                var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(post.UserId)},
                 { "Title", post.Title},
                 { "Content",post.Content},
                { "Type", post.Type},
                { "PostDate", Convert.ToDateTime(post.PostDate)},
            };

                UnitOfWork.Repository<Post>().Update(document, ObjectId.Parse(id), "posts");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id)
        {
            try
            {
                Post post = this.GetById(id).Result;
                var document = new BsonDocument
            {  { "UserId", ObjectId.Parse(post.UserId)},
                 { "Title", post.Title},
                 { "Content",post.Content},
                { "Type", post.Type},
                { "PostDate", Convert.ToDateTime(post.PostDate)},
                {"Deleted",true }
            };
                UnitOfWork.Repository<Post>().Delete(document, ObjectId.Parse(id), "posts", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}