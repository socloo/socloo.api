using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using SoclooAPI.Data;
using SoclooAPI.Models;

namespace SoclooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(IConfiguration config, ILogger<UsersController> logger, DataContext context) :
            base(config, logger, context)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await UnitOfWork.Repository<User>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(users);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("{id}")]
        public async Task<User> GetById(string id)
        {
            try
            {
                var users = await UnitOfWork.Repository<User>()
                    .GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return users[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] User user)
        {
            await UnitOfWork.Repository<User>().InsertAsync(user);

            return true;
        }


        [HttpPut("{id}")]
        public async Task<bool> Put(string id, [FromBody] User user)
        {
            try
            {
                var document = new BsonDocument

                {
                    {"FullName", user.FullName},

                    {"PhoneNumber", user.PhoneNumber},

                    {"Email", user.Email},

                    {"Bio", user.Bio},

                    {"ProfilePictureId", ObjectId.Parse(user.ProfilePictureId)}
                };
                UnitOfWork.Repository<User>().UpdateAsync(document, ObjectId.Parse(id), "users");
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
            var user = GetById(id).Result;

            try
            {
                var document = new BsonDocument

                {
                    {"FullName", user.FullName},

                    {"PhoneNumber", user.PhoneNumber},

                    {"Email", user.Email},

                    {"Bio", user.Bio},

                    {"ProfilePictureId", ObjectId.Parse(user.ProfilePictureId)},
                    {"Deleted", true}
                };
                await UnitOfWork.Repository<User>().DeleteAsync(document, ObjectId.Parse(id), "users");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}