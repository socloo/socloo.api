using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using SoclooAPI.Data;
using SoclooAPI.Models;
using System.Collections;
using System.Collections.Generic;

namespace SoclooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(IConfiguration config, ILogger logger, DataContext context) :
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
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var users = await UnitOfWork.Repository<User>()
                    .GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                 return new OkObjectResult(users[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try{
                Calendar calendar = new Calendar {Deleted = false, OccurrencesId = new List<string>() };

                new CalendarsController(Config, Logger, DataContext).Post(calendar);
                await UnitOfWork.Repository<User>().InsertAsync(user);
                calendar.UserId = Convert.ToString(user.Id);
                new CalendarsController(Config, Logger, DataContext).Put(Convert.ToString(calendar.Id),calendar);
                return new OkObjectResult(user.Id);

            }catch(Exception ex){
                return new BadRequestResult();
                }

           
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] User user)
        {
            try
            {
                var document = new BsonDocument

                {
                    {"FullName", user.FullName},

                    {"PhoneNumber", user.PhoneNumber},

                    {"Email", user.Email},

                    {"Bio", user.Bio},

                    {"ProfilePictureId", ObjectId.Parse(user.ProfilePictureId)},
                   
                };
                UnitOfWork.Repository<User>().UpdateAsync(document, ObjectId.Parse(id), "users");
                 return new OkObjectResult(user.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            User user = (User)GetById(id).Result;

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
                return new OkObjectResult(user);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
    }
}