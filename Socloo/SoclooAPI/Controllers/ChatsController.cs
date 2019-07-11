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
    public class ChatsController : BaseController
    {

        public ChatsController(IConfiguration config, ILogger logger, DataContext context) :
            base(config, logger, context)
        { }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<Chat>().GetListAsync(u => !u.Deleted);

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
                var result = await UnitOfWork.Repository<Chat>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return new OkObjectResult(result[0]);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Chat chat)
        {
            try
            {
                await UnitOfWork.Repository<Chat>().InsertAsync(chat);

                return new OkObjectResult(chat.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();

            }
            
        }


        [HttpPut("{id}")]
        async public Task<IActionResult> Put(string id, [FromBody] Chat chat)
        {

            try
            {
                var document = new BsonDocument
            {
                 { "UsersId", new BsonArray(chat.UsersId)},
                 { "MessagesId",new BsonArray(chat.MessagesId)},
                 {"ChatName",chat.ChatName },
                { "ChatType", chat.ChatType},
            };

                UnitOfWork.Repository<Chat>().UpdateAsync(document, ObjectId.Parse(id), "chats");
                return new OkObjectResult(chat.Id);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            Chat chat = (Chat)this.GetById(id).Result;
            try
            {
                var document = new BsonDocument
            {
                 { "UsersId", new BsonArray(chat.UsersId)},
                 { "MessagesId",new BsonArray(chat.MessagesId)},
                 {"ChatName",chat.ChatName },
                { "ChatType", chat.ChatType},
                 { "Deleted", true}
            };
                UnitOfWork.Repository<Chat>().DeleteAsync(document, ObjectId.Parse(id), "chats", true);
                return new OkObjectResult(chat);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }

        }
    }
}