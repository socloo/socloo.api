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
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<SuperAdmin> GetById(string id)
        {
            try
            {
                var superAdmins = await UnitOfWork.Repository<SuperAdmin>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return superAdmins[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] SuperAdmin admin)
        {
            await UnitOfWork.Repository<SuperAdmin>().InsertAsync(admin);
        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] SuperAdmin admin)
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
                UnitOfWork.Repository<SuperAdmin>().Update(document, ObjectId.Parse(id), "superadmins");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id, [FromBody] SuperAdmin admin)
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
                UnitOfWork.Repository<SuperAdmin>().Delete(document, ObjectId.Parse(id), "superadmins", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}