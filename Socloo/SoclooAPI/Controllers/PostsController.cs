using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using SoclooAPI.Data;
using SoclooAPI.Models;
using System;
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
                return new BadRequestResult();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Post>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            try
            {
                await UnitOfWork.Repository<Post>().InsertAsync(post);

                return new OkObjectResult(post.Id);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Post post)
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
                {"Deleted",false}
            };

                UnitOfWork.Repository<Post>().UpdateAsync(document, ObjectId.Parse(id), "posts");
                return new OkObjectResult(post.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            try
            {
                Post post = (Post)this.GetById(id).Result;
                var document = new BsonDocument
            {  { "UserId", ObjectId.Parse(post.UserId)},
                 { "Title", post.Title},
                 { "Content",post.Content},
                { "Type", post.Type},
                { "PostDate", Convert.ToDateTime(post.PostDate)},
                {"Deleted",true }
            };
                UnitOfWork.Repository<Post>().DeleteAsync(document, ObjectId.Parse(id), "posts", true);
                return new OkObjectResult(post);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}