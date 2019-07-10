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
    public class AnswerMCsController : BaseController
    {
        public AnswerMCsController(IConfiguration config, ILogger<AnswerMCsController> logger, DataContext context) :
            base(config, logger, context)
        { }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<AnswerMC>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<AnswerMC> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<AnswerMC>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        public async Task<bool> Post([FromBody] AnswerMC answerMC)
        {

            await UnitOfWork.Repository<AnswerMC>().InsertAsync(answerMC);

            return true;
        }

        [HttpPut("{_id}")]
        async public Task<bool> Put(string _id, [FromBody] AnswerMC answerMC)
        {
            try
            {
                var document = new BsonDocument
                {
                     { "Text", answerMC.Text},
                     { "QuestionId",ObjectId.Parse(answerMC.QuestionId)},
                     { "Correct", answerMC.Correct},
                     { "Image", ObjectId.Parse(answerMC.Image)}
                };
                UnitOfWork.Repository<AnswerMC>().Update(document, ObjectId.Parse(_id), "answermcs");
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
            AnswerMC answerMC = this.GetById(id).Result;

            try
            {
                var document = new BsonDocument
                {
                     { "Text", answerMC.Text},
                     { "QuestionId",ObjectId.Parse(answerMC.QuestionId)},
                     { "Correct", answerMC.Correct},
                     { "Image", ObjectId.Parse(answerMC.Image)},
                    { "Deleted", true}
                };
                UnitOfWork.Repository<AnswerMC>().Delete(document, ObjectId.Parse(id), "answermcs", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}