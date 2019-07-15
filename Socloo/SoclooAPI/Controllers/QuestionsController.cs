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
    public class QuestionsController : BaseController
    {
        public QuestionsController(IConfiguration config, ILogger<QuestionsController> logger, DataContext context) :
            base(config, logger, context)
        { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Question>().GetListAsync(u => !u.Deleted);

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
                var result = await UnitOfWork.Repository<Question>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Question question)
        {
            try
            {
                await UnitOfWork.Repository<Question>().InsertAsync(question);

             
                return new OkObjectResult(question.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Question question)
        {

            
            try
            {
                var document = new BsonDocument
            {
                { "Text", question.Text}

            };
                UnitOfWork.Repository<Question>().UpdateAsync(document, ObjectId.Parse(id), "questions");
                return new OkObjectResult(question.Id);
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
                Question question = (Question)this.GetById(id).Result;
                var document = new BsonDocument
            {  { "Text", question.Text},{"Deleted",true }
            };
                UnitOfWork.Repository<Question>().DeleteAsync(document, ObjectId.Parse(id), "questions", true);
                return new OkObjectResult(question.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}