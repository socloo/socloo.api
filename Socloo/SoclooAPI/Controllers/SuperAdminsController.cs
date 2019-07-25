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
        public SuperAdminsController(IConfiguration config, ILogger<SuperAdminsController> logger, DataContext context) :
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
        public async Task<IActionResult> Post([FromBody] SuperAdminViewModel model)
        {
            try
            {
                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                ILogger<UsersController> logger = new LoggerFactory().CreateLogger<UsersController>();
                OkObjectResult userResponse = (OkObjectResult)await new UsersController(Config, logger, DataContext).Post(user);
                var superAdmin = new SuperAdmin
                {

                    CoursesId = model.CoursesId,
                    Deleted = model.Deleted,
                    GroupsId = model.GroupsId,
                   TeachersId=model.TeachersId,
                    UserId = Convert.ToString(user.Id),
                };
                await UnitOfWork.Repository<SuperAdmin>().InsertAsync(superAdmin);
                return new OkObjectResult(superAdmin.Id);
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
                {"Deleted",false}
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