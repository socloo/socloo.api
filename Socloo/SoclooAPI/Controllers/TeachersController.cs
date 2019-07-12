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
                return new BadRequestResult();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var teacher = await UnitOfWork.Repository<Teacher>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(teacher[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TeacherViewModel model)
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

                var teacher = new Teacher
                {
                    CoursesId = model.CoursesId,
                    Deleted = model.Deleted,
                    GroupsId = model.GroupsId,
                    Subject = model.Subject,
                    UserId = Convert.ToString(user.Id),
                };

                await UnitOfWork.Repository<Teacher>().InsertAsync(teacher);

                return new OkObjectResult(teacher.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

            

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Teacher teacher)
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
                UnitOfWork.Repository<Teacher>().UpdateAsync(document, ObjectId.Parse(id), "teachers");

                return new OkObjectResult(teacher.Id);
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
                Teacher teacher = (Teacher)this.GetById(id).Result;
                var document = new BsonDocument
            {
                { "UserId", teacher.UserId},
                { "CoursesId", new BsonArray(teacher.CoursesId)},
                { "GroupsId", new BsonArray(teacher.GroupsId)},
                { "Subject", new BsonArray(teacher.Subject)}
            };
                UnitOfWork.Repository<Teacher>().DeleteAsync(document, ObjectId.Parse(id), "teacher", true);

                return new OkObjectResult(teacher);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }

    }
}