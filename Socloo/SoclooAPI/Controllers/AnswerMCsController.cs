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
                return new BadRequestResult();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<AnswerMC>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AnswerMC answerMC)
        {
            try
            {
                await UnitOfWork.Repository<AnswerMC>().InsertAsync(answerMC);
                ILogger<AnswersController> logger = new LoggerFactory().CreateLogger<AnswersController>();
                Answer answer = new Answer { Deleted= false, SubclassId=Convert.ToString(answerMC.Id), SubclassType=1 };
                OkObjectResult answerResponse = (OkObjectResult)await new AnswersController(Config, logger, DataContext).Post(answer);
                return new OkObjectResult(answerMC.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
            
        }

        [HttpPut("{id}")]
        async public Task<IActionResult> Put(string id, [FromBody] AnswerMC answerMC)
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
                UnitOfWork.Repository<AnswerMC>().UpdateAsync(document, ObjectId.Parse(id), "answermcs");
                return new OkObjectResult(answerMC.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            AnswerMC answerMC = (AnswerMC)this.GetById(id).Result;

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
                UnitOfWork.Repository<AnswerMC>().DeleteAsync(document, ObjectId.Parse(id), "answermcs", true);
                return new OkObjectResult(answerMC);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }

    }
}