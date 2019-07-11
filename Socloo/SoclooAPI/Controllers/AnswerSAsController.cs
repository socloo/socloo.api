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
    public class AnswerSAsController : BaseController
    {
        public AnswerSAsController(IConfiguration config, ILogger<AnswerSAsController> logger, DataContext context) :
            base(config, logger, context)
        { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<AnswerSA>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<AnswerSA> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<AnswerSA>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] AnswerSA answerSA)
        {

            await UnitOfWork.Repository<AnswerSA>().InsertAsync(answerSA);

            return true;
        }

        [HttpPut("{_id}")]
        async public Task<bool> Put(string _id, [FromBody] AnswerSA answerSA)
        {
            try
            {
                var document = new BsonDocument
                {
                    { "Text", answerSA.Text},
                    { "QuestionId", ObjectId.Parse(answerSA.QuestionId)}
                };
                UnitOfWork.Repository<AnswerSA>().Update(document, ObjectId.Parse(_id), "answersas");
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
            AnswerSA answerSA = this.GetById(id).Result;

            try
            {
                var document = new BsonDocument
                {
                     { "Text", answerSA.Text},
                       { "QuestionId", ObjectId.Parse(answerSA.QuestionId)},
                    { "Deleted", true}
                };
                UnitOfWork.Repository<AnswerSA>().DeleteAsync(document, ObjectId.Parse(id), "answersas", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}