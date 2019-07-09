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

        public ChatsController(IConfiguration config, ILogger<ChatsController> logger, DataContext context) :
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
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Chat> GetById(string id)
        {
            try
            {
                var result = await UnitOfWork.Repository<Chat>().GetListAsync(u => !u.Deleted && u.Id == ObjectId.Parse(id));
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Post([FromBody] Chat chat)
        {

            await UnitOfWork.Repository<Chat>().InsertAsync(chat);

            return true;
        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string _id, [FromBody] Chat chat)
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

                UnitOfWork.Repository<Chat>().Update(document, ObjectId.Parse(_id), "chats");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id, [FromBody] Chat chat)
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
                UnitOfWork.Repository<Chat>().Delete(document, ObjectId.Parse(id), "chats", true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}