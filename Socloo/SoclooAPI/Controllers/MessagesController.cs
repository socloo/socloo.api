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
    public class MessagesController : BaseController
    {
        public MessagesController(IConfiguration config, ILogger<MessagesController> logger, DataContext context) :
            base(config, logger, context)
        { }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Message>().GetListAsync(u => !u.Deleted);

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
                var result = await UnitOfWork.Repository<Message>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Message message)
        {
            try
            {
                await UnitOfWork.Repository<Message>().InsertAsync(message);

                return new OkObjectResult(message.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();

            }
            
        }


        [HttpPut("{id}")]
        async public Task<IActionResult> Put(string id, [FromBody] Message message)
        {

            try
            {
                var document = new BsonDocument
            {
                  { "UserId", ObjectId.Parse(message.UserId)},
                 { "DataTime", Convert.ToDateTime(message.DataTime)},
                 { "MessageText",message.MessageText},
                { "ChatId",  ObjectId.Parse(message.ChatId)},
                {"Deleted",false}
            };

                UnitOfWork.Repository<Group>().UpdateAsync(document, ObjectId.Parse(id), "messages");
                return new OkObjectResult(message.Id);
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
                Message message = (Message)this.GetById(id).Result;
                var document = new BsonDocument
            {
                  { "UserId", ObjectId.Parse(message.UserId)},
                 { "DataTime", Convert.ToDateTime(message.DataTime)},
                 { "MessageText",message.MessageText},
                { "ChatId",  ObjectId.Parse(message.ChatId)},
                 {"Deleted",true }
            };
                UnitOfWork.Repository<Message>().DeleteAsync(document, ObjectId.Parse(id), "messages", true);
                return new OkObjectResult(message);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}