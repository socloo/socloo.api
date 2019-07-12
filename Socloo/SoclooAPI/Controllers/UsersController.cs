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
              
                await UnitOfWork.Repository<User>().InsertAsync(user);
                ILogger<CalendarsController> logger = new LoggerFactory().CreateLogger<CalendarsController>();
                Calendar calendar = new Calendar { UserId = Convert.ToString(user.Id), Deleted=false};
                OkObjectResult calendarResponse = (OkObjectResult) await new CalendarsController(Config, logger, DataContext).Post(calendar);
                user.CalendarId = Convert.ToString(calendar.Id);
                user.ProfilePictureId = "5d286be132b90e35642c96db";
                await this.Put(Convert.ToString(user.Id), user);
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
                    {"FullName", user.FullName+""},

                    {"PhoneNumber", user.PhoneNumber+""},

                    {"Email", user.Email+""},

                    {"Bio", user.Bio+""},

                    {"ProfilePictureId", ObjectId.Parse(user.ProfilePictureId)},
                    {"CalendarId", ObjectId.Parse(user.CalendarId) },
                    { "Deleted",user.Deleted}

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