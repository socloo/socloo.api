using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using SoclooAPI.Data;
using SoclooAPI.Models;
using System;
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
                return new BadRequestResult();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Student>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentViewModel model)
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

                ILogger<PortfoliosController> loggerPortfolio = new LoggerFactory().CreateLogger<PortfoliosController>();
                OkObjectResult PortfolioResponse = (OkObjectResult)await new PortfoliosController(Config, loggerPortfolio, DataContext).Post(new Portfolio { UserId = Convert.ToString(userResponse.Value), Certification = "", Deleted = false, Education = "", Experience = "", GeneralInfo = "", Interests = "", References = "", Skills = "" });

                var student = new Student
                {
                    CoursesId = model.CoursesId,
                    GroupsId = model.GroupsId,
                    PortfolioId = Convert.ToString(PortfolioResponse.Value),
                    TeachersId = model.TeachersId,
                    Deleted = false,
                    UserId = Convert.ToString(userResponse.Value),
                    SchoolId = model.SchoolId
                };
                await UnitOfWork.Repository<Student>().InsertAsync(student);

                return new OkObjectResult(student.Id);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Student student)
        {

            var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(student.UserId)},
                 { "TeachersId", new BsonArray(student.TeachersId)},
                 { "CoursesId",new BsonArray(student.CoursesId)},
                { "GroupsId", new BsonArray(student.GroupsId)},
                { "PortfolioId", ObjectId.Parse(student.PortfolioId)},
                {"Deleted",false},
                {"SchoolId", ObjectId.Parse(student.SchoolId) }
            };
            try
            {
                UnitOfWork.Repository<Student>().UpdateAsync(document, ObjectId.Parse(id), "students");
                return new OkObjectResult(student.Id);
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
                Student student = (Student)this.GetById(id).Result;
                var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(student.UserId)},
                 { "TeachersId", new BsonArray(student.TeachersId)},
                 { "CoursesId",new BsonArray(student.CoursesId)},
                { "GroupsId", new BsonArray(student.GroupsId)},
                { "PortfolioId", ObjectId.Parse(student.PortfolioId)},
                  {"Deleted",true },
                  {"SchoolId", ObjectId.Parse(student.SchoolId) }
            };
                UnitOfWork.Repository<Student>().DeleteAsync(document, ObjectId.Parse(id), "students", true);
                return new OkObjectResult(student);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}