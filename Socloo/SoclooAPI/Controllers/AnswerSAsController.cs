using Microsoft.AspNetCore.Mvc;
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
                return new BadRequestResult();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<AnswerSA>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AnswerSA answerSA)
        {
            try
            {
                await UnitOfWork.Repository<AnswerSA>().InsertAsync(answerSA);
                ILogger<AnswersController> logger = new LoggerFactory().CreateLogger<AnswersController>();
                Answer answer = new Answer { Deleted = false, SubclassId = Convert.ToString(answerSA.Id), SubclassType = 2 };
                OkObjectResult answerResponse = (OkObjectResult)await new AnswersController(Config, logger, DataContext).Post(answer);
                return new OkObjectResult(answerSA.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }


        }

        [HttpPut("{id}")]
        async public Task<IActionResult> Put(string id, [FromBody] AnswerSA answerSA)
        {
            try
            {
                var document = new BsonDocument
                {
                    { "Text", answerSA.Text},
                    { "QuestionId", ObjectId.Parse(answerSA.QuestionId)},
                    {"Deleted",false}
                };
                UnitOfWork.Repository<AnswerSA>().UpdateAsync(document, ObjectId.Parse(id), "answersas");
                return new OkObjectResult(answerSA.Id);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            AnswerSA answerSA = (AnswerSA)this.GetById(id).Result;

            try
            {
                var document = new BsonDocument
                {
                     { "Text", answerSA.Text},
                       { "QuestionId", ObjectId.Parse(answerSA.QuestionId)},
                    { "Deleted", true}
                };
                UnitOfWork.Repository<AnswerSA>().DeleteAsync(document, ObjectId.Parse(id), "answersas", true);
                return new OkObjectResult(answerSA);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }

    }
}