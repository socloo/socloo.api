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
    public class UsersController : BaseController
    {
        public UsersController(IConfiguration config, ILogger<UsersController> logger, DataContext context) :
            base(config, logger, context)
        { }

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
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<User> GetById(string id)
        {
            try
            {
                var users = await UnitOfWork.Repository<User>().GetListAsync(u => !u.Deleted&&u.Id== ObjectId.Parse(id));
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


        [HttpPut("{_id}")]
        async public Task<bool> Put(string _id, [FromBody] User user)
        {


            try
            {
                var document = new BsonDocument

                {

                { "FullName", user.FullName},

                { "PhoneNumber", user.PhoneNumber},

                { "Email", user.Email},

                { "Bio", user.Bio},

                { "ProfilePictureId", ObjectId.Parse(user.ProfilePictureId)}

                  };
                UnitOfWork.Repository<User>().Update(document, ObjectId.Parse(_id),"users");
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
            User user = this.GetById(id).Result;

            try
            {
                var document = new BsonDocument

                {

                { "FullName", user.FullName},

                { "PhoneNumber", user.PhoneNumber},

                { "Email", user.Email},

                { "Bio", user.Bio},

                { "ProfilePictureId", ObjectId.Parse(user.ProfilePictureId)},
                    {"Deleted",true }

                  };
                UnitOfWork.Repository<User>().Delete(document, ObjectId.Parse(id), "users", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}