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
    public class TestsController : BaseController
    {

        public TestsController(IConfiguration config, ILogger<TestsController> logger, DataContext context) :
                base(config, logger, context)
        { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tests = await UnitOfWork.Repository<Test>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(tests);


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
                var tests = await UnitOfWork.Repository<Test>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(tests[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Test test)
        {
            try
            {
                UnitOfWork.Repository<Test>().InsertAsync(test);
                return new OkObjectResult(test.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpPut("{id}")]
        async public Task<IActionResult> Put(string id, [FromBody] Test test)
        {
            try
            {
                var document = new BsonDocument
            {
                 { "TeachersId", new BsonArray(test.TeachersId)},
                 { "StudentsId", new BsonArray(test.StudentsId)},
                 { "TimeMax",test.TimeMax},
                 { "PictureId", test.PictureId},
                 { "QuestionsId", new BsonArray(test.QuestionsId)},
                 { "Type", test.Type}
            };

                UnitOfWork.Repository<Test>().UpdateAsync(document, ObjectId.Parse(id), "tests");
                return new OkObjectResult(test.Id);
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
                Test test = (Test)GetById(id).Result;
                var document = new BsonDocument
            {
                 { "TeachersId", new BsonArray(test.TeachersId)},
                 { "StudentsId", new BsonArray(test.StudentsId)},
                 { "TimeMax",test.TimeMax},
                 { "PictureId", test.PictureId},
                 { "QuestionsId", new BsonArray(test.QuestionsId)},
                 { "Type", test.Type},
                    {"Deleted",true }

            };

                UnitOfWork.Repository<Test>().DeleteAsync(document, ObjectId.Parse(id), "tests", true);
                return new OkObjectResult(test.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }



    }
}