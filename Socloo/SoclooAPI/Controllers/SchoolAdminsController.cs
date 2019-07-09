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
    public class SchoolAdminsController : BaseController
    {
        public SchoolAdminsController(IConfiguration config, ILogger<SchoolAdminsController> logger, DataContext context) :
            base(config, logger, context)
        { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<SchoolAdmin>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<SchoolAdmin> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<SchoolAdmin>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] SchoolAdmin schooladmin)
        {

            await UnitOfWork.Repository<SchoolAdmin>().InsertAsync(schooladmin);

            return true;
        }
        [HttpPut("{id}")]
        async public Task<bool> Put(string _id, [FromBody] SchoolAdmin schooladmin)
        {

            var document = new BsonDocument
            {
                 { "UserId", schooladmin.UserId},
                 { "TeachersId", new BsonArray(schooladmin.TeachersId)},
                 { "CoursesId",new BsonArray(schooladmin.CoursesId)},
                 { "GroupsId", new BsonArray(schooladmin.GroupsId)},
                 {"Type",schooladmin.Type }

            };
            try
            {
                UnitOfWork.Repository<SchoolAdmin>().Update(document, ObjectId.Parse(_id), "schooladmins");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id, [FromBody] SchoolAdmin schooladmin)
        {
            try
            {
                var document = new BsonDocument
            {
                 { "UserId", schooladmin.UserId},
                 { "TeachersId", new BsonArray(schooladmin.TeachersId)},
                 { "CoursesId",new BsonArray(schooladmin.CoursesId)},
                 { "GroupsId", new BsonArray(schooladmin.GroupsId)},
                 {"Type",schooladmin.Type }

            };
                UnitOfWork.Repository<SchoolAdmin>().Delete(document, ObjectId.Parse(id), "schooladmins", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}