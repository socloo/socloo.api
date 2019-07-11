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
    public class SuperAdminsController : BaseController
    {
        public SuperAdminsController(IConfiguration config, ILogger logger, DataContext context) :
            base(config, logger, context)
        { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var superAdmins = await UnitOfWork.Repository<SuperAdmin>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(superAdmins);

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
                var superAdmins = await UnitOfWork.Repository<SuperAdmin>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(superAdmins[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SuperAdmin admin, User user)
        {
            try
            {
                new UsersController(Config, Logger, DataContext).Post(user);
                admin.UserId = Convert.ToString(user.Id);
                await UnitOfWork.Repository<SuperAdmin>().InsertAsync(admin);
                return new OkObjectResult(admin.Id);
            }
            catch(Exception ex)
            {
                return new BadRequestResult();
            }
        }


        [HttpPut("{id}")]
         public async Task<IActionResult> Put(string id, [FromBody] SuperAdmin admin)
        {

            try
            {
                var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(admin.UserId)},
                 { "TeachersId", new BsonArray(admin.TeachersId)},
                 { "CoursesId",new BsonArray(admin.CoursesId)},
                { "GroupsId",new BsonArray(admin.GroupsId)},
            };
                UnitOfWork.Repository<SuperAdmin>().UpdateAsync(document, ObjectId.Parse(id), "superadmins");
                return new OkObjectResult(admin.Id);
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
                SuperAdmin admin = (SuperAdmin)this.GetById(id).Result;
                var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(admin.UserId)},
                 { "TeachersId", new BsonArray(admin.TeachersId)},
                 { "CoursesId",new BsonArray(admin.CoursesId)},
                { "GroupsId",new BsonArray(admin.GroupsId)},
                {"Deleted",true }
            };
                UnitOfWork.Repository<SuperAdmin>().DeleteAsync(document, ObjectId.Parse(id), "superadmins", true);
                return new OkObjectResult(admin);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }

    }
}