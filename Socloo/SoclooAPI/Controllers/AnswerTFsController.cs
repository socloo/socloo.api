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
    public class AnswerTFsController : BaseController
    {
        public AnswerTFsController(IConfiguration config, ILogger<AnswerTFsController> logger, DataContext context) :
            base(config, logger, context)
        { }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<AnswerTF>().GetListAsync(u => !u.Deleted);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<AnswerTF> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<AnswerTF>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        public async Task<bool> Post([FromBody] AnswerTF answerTF)
        {

            await UnitOfWork.Repository<AnswerTF>().InsertAsync(answerTF);

            return true;
        }
        [HttpPut("{_id}")]
        async public Task<bool> Put(string _id, [FromBody]  AnswerTF answerTF)
        {

            
            try
            {
                var document = new BsonDocument
            {
                 { "QuestionId",ObjectId.Parse(answerTF.QuestionId)},
                 { "Correct",answerTF.Correct}
            };
                UnitOfWork.Repository<AnswerTF>().Update(document, ObjectId.Parse(_id), "answertfs");
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
            AnswerTF answerTF = this.GetById(id).Result;

            try
            {
                var document = new BsonDocument
            {
                 { "QuestionId",ObjectId.Parse(answerTF.QuestionId)},
                 { "Correct",answerTF.Correct},
                     { "Deleted", true}
            };
                UnitOfWork.Repository<AnswerTF>().Delete(document, ObjectId.Parse(id), "answertfs", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}