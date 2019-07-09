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
    public class AnswersController : BaseController
    {
        public AnswersController(IConfiguration config, ILogger<UsersController> logger, DataContext context) :
            base(config, logger, context)
        { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Answer>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet("{id}")]
        public async Task<Answer> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Answer>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        public async Task<bool> Post([FromBody] Answer answer)
        {

            await UnitOfWork.Repository<Answer>().InsertAsync(answer);

            return true;
        }

        [HttpPut("{id}")]
        async public Task<bool> Put(string _id, [FromBody] Answer answer)
        {

            
            try
            {
                var document = new BsonDocument
            {
                { "SubclassId", ObjectId.Parse(answer.SubclassId)},
                { "SubclassType", answer.SubclassType}
            };
                UnitOfWork.Repository<Answer>().Update(document, ObjectId.Parse(_id), "answers");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id, [FromBody] Answer answer)
        {
            try
            {
                var document = new BsonDocument
                {
                     { "SubclassId", ObjectId.Parse(answer.SubclassId)},
                 { "SubclassType", answer.SubclassType}
                };
                UnitOfWork.Repository<Answer>().Delete(document, ObjectId.Parse(id), "answers", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}