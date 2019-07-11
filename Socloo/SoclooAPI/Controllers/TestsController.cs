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
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Test> GetById(string id)
        {
            try
            {
                var tests = await UnitOfWork.Repository<Test>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return tests[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
         public bool Post([FromBody] Test test)
        {
             UnitOfWork.Repository<Test>().InsertAsync(test);

            return true;
        }
        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Test test)
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
            try
            {
                UnitOfWork.Repository<Test>().UpdateAsync(document, ObjectId.Parse(id), "tests");
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
                Test test = this.GetById(id).Result;
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



    }
}