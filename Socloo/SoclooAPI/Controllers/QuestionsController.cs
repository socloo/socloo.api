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
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Question> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Question>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        public async Task<bool> Post([FromBody] Question question)
        {

            await UnitOfWork.Repository<Question>().InsertAsync(question);

            return true;
        }
        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Question question)
        {

            
            try
            {
                var document = new BsonDocument
            {
                { "Text", question.Text}

            };
                UnitOfWork.Repository<Question>().Update(document, ObjectId.Parse(id), "questions");
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
                Question question = this.GetById(id).Result;
                var document = new BsonDocument
            {  { "Text", question.Text},{"Deleted",true }
            };
                UnitOfWork.Repository<Question>().Delete(document, ObjectId.Parse(id), "questions", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}