using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using SoclooAPI.Data;
using SoclooAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SoclooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : BaseController
    {
        public TeachersController(IConfiguration config, ILogger<TeachersController> logger, DataContext context) :
                base(config, logger, context)
        { }

    [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var teachers = await UnitOfWork.Repository<Teacher>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(teachers);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Teacher> GetById(string id)
        {
            try
            {
                var teacher = await UnitOfWork.Repository<Teacher>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return teacher[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
         public bool Post([FromBody] Teacher teacher)
        {
            UnitOfWork.Repository<Teacher>().InsertAsync(teacher);

            return true;
        }

        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Teacher teacher)
        {

            var document = new BsonDocument
            {
                { "UserId", teacher.UserId},
                { "CoursesId", new BsonArray(teacher.CoursesId)},
                { "GroupsId", new BsonArray(teacher.GroupsId)},
                { "Subject", new BsonArray(teacher.Subject)}
            };
            try
            {
                UnitOfWork.Repository<Teacher>().Update(document, ObjectId.Parse(id), "teachers");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id, [FromBody] Teacher teacher)
        {
            try
            {
                var document = new BsonDocument
            {
                { "UserId", teacher.UserId},
                { "CoursesId", new BsonArray(teacher.CoursesId)},
                { "GroupsId", new BsonArray(teacher.GroupsId)},
                { "Subject", new BsonArray(teacher.Subject)}
            };
                UnitOfWork.Repository<Teacher>().Delete(document, ObjectId.Parse(id), "teacher", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}