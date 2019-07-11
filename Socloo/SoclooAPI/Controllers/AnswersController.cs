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
        public AnswersController(IConfiguration config, ILogger logger, DataContext context) :
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
                return new BadRequestResult();
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Answer>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Answer answer)
        {
            try
            {
                await UnitOfWork.Repository<Answer>().InsertAsync(answer);

                return new OkObjectResult(answer.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
            
        }

        [HttpPut("{id}")]
        async public Task<IActionResult> Put(string id, [FromBody] Answer answer)
        {

            
            try
            {
                var document = new BsonDocument
            {
                { "SubclassId", ObjectId.Parse(answer.SubclassId)},
                { "SubclassType", answer.SubclassType}
            };
                UnitOfWork.Repository<Answer>().UpdateAsync(document, ObjectId.Parse(id), "answers");
                return new OkObjectResult(answer.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            Answer answer = (Answer)this.GetById(id).Result;

            try
            {
                var document = new BsonDocument
                {
                     { "SubclassId", ObjectId.Parse(answer.SubclassId)},
                 { "SubclassType", answer.SubclassType},
                 { "Deleted", true}
                };
                UnitOfWork.Repository<Answer>().DeleteAsync(document, ObjectId.Parse(id), "answers", true);
                return new OkObjectResult(answer);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}