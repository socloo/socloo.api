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
    public class CoursesController : BaseController
    {

        public CoursesController(IConfiguration config, ILogger<CoursesController> logger, DataContext context) :
            base(config, logger, context)
        { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Course>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Course> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Course>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Course course)
        {

            await UnitOfWork.Repository<Course>().InsertAsync(course);

            return true;
        }

        [HttpPut("{_id}")]
        async public Task<bool> Put(string _id, [FromBody] Course course)
        {

            
            try
            {
                var document = new BsonDocument
            {
                 { "StudentsId", new BsonArray(course.StudentsId)},
                 { "TeachersId", new BsonArray(course.TeachersId)},
                 { "CoordinatorsId",new BsonArray(course.CoordinatorsId)},
                 { "Grade", course.Grade},
                 { "Section", course.Section},
                 { "SubjectBranch", course.SubjectBranch}
            };
                UnitOfWork.Repository<Chat>().UpdateAsync(document, ObjectId.Parse(_id), "courses");
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
            Course course = this.GetById(id).Result;
            try
            {
                var document = new BsonDocument
            {
                 { "StudentsId", new BsonArray(course.StudentsId)},
                 { "TeachersId", new BsonArray(course.TeachersId)},
                 { "CoordinatorsId",new BsonArray(course.CoordinatorsId)},
                 { "Grade", course.Grade},
                 { "Section", course.Section},
                 { "SubjectBranch", course.SubjectBranch},
                 { "Deleted", true}
            };
                UnitOfWork.Repository<Course>().DeleteAsync(document, ObjectId.Parse(id), "courses", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}