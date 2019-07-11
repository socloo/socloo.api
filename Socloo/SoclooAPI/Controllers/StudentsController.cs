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
    public class StudentsController : BaseController
    {
        private MongoDBContext mongoDB;
        public StudentsController(IConfiguration config, ILogger<StudentsController> logger, DataContext context) :
            base(config, logger, context)
        { }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Student>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Student> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Student>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Student student)
        {

            await UnitOfWork.Repository<Student>().InsertAsync(student);

            return true;
        }

        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Student student)
        {

            var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(student.UserId)},
                 { "TeachersId", new BsonArray(student.TeachersId)},
                 { "CoursesId",new BsonArray(student.CoursesId)},
                { "GroupsId", new BsonArray(student.GroupsId)},
                { "PortfolioId", ObjectId.Parse(student.PortfolioId)},
            };
            try
            {
                UnitOfWork.Repository<Student>().UpdateAsync(document, ObjectId.Parse(id), "students");
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
            try
            {
                Student student = this.GetById(id).Result;
                var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(student.UserId)},
                 { "TeachersId", new BsonArray(student.TeachersId)},
                 { "CoursesId",new BsonArray(student.CoursesId)},
                { "GroupsId", new BsonArray(student.GroupsId)},
                { "PortfolioId", ObjectId.Parse(student.PortfolioId)},
                  {"Deleted",true }
            };
                UnitOfWork.Repository<Student>().DeleteAsync(document, ObjectId.Parse(id), "students", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}