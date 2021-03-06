﻿using Microsoft.AspNetCore.Mvc;
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
                return new BadRequestResult();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<AnswerTF>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AnswerTF answerTF)
        {
            try
            {
                await UnitOfWork.Repository<AnswerTF>().InsertAsync(answerTF);
                ILogger<AnswersController> logger = new LoggerFactory().CreateLogger<AnswersController>();
                Answer answer = new Answer { Deleted = false, SubclassId = Convert.ToString(answerTF.Id), SubclassType = 3 };
                OkObjectResult answerResponse = (OkObjectResult)await new AnswersController(Config, logger, DataContext).Post(answer);
                return new OkObjectResult(answerTF.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();

            }


        }
        [HttpPut("{id}")]
        async public Task<IActionResult> Put(string id, [FromBody]  AnswerTF answerTF)
        {


            try
            {
                var document = new BsonDocument
            {
                 { "QuestionId",ObjectId.Parse(answerTF.QuestionId)},
                 { "Correct",answerTF.Correct},
                 {"Deleted",false}
            };
                UnitOfWork.Repository<AnswerTF>().UpdateAsync(document, ObjectId.Parse(id), "answertfs");
                return new OkObjectResult(answerTF.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            AnswerTF answerTF = (AnswerTF)this.GetById(id).Result;

            try
            {
                var document = new BsonDocument
            {
                 { "QuestionId",ObjectId.Parse(answerTF.QuestionId)},
                 { "Correct",answerTF.Correct},
                     { "Deleted", true}
            };
                UnitOfWork.Repository<AnswerTF>().DeleteAsync(document, ObjectId.Parse(id), "answertfs", true);
                return new OkObjectResult(answerTF);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}