using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoclooAPI.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using Nancy.Json;
using MongoDB.Bson.IO;

namespace SoclooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {

        private MongoDBContext mongoDB;
        public ChatsController()
        {
            mongoDB = new MongoDBContext();
        }
        [HttpGet]
        public async Task<List<ChatViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<ChatViewModel>("Chats").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<ChatViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<ChatViewModel>("Chats");
                var filter = Builders<ChatViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] ChatViewModel chat)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);

            var document = new BsonDocument
            {
                 { "StudentsId", bsonarray},
                 { "TeachersId",bsonarray},
                { "MessagesId", bsonarray},
                {"ChatName",chat.ChatName },
                { "ChatType", chat.ChatType},
            };

            var collection = mongoDB.database.GetCollection<BsonDocument>("Chats");
            await collection.InsertOneAsync(document);

        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] ChatViewModel chat)
        {

            try
            {
                var document = new BsonDocument
            {
                 { "StudentsId", new BsonArray(chat.StudentsId)},
                 { "TeachersId", new BsonArray(chat.TeachersId)},
                 { "MessagesId",new BsonArray(chat.MessagesId)},
                 {"ChatName",chat.ChatName },
                { "ChatType", chat.ChatType},
            };

                var collection = mongoDB.database.GetCollection<BsonDocument>("Chats");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
                await collection.FindOneAndReplaceAsync(filter, document);
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
                var collection = mongoDB.database.GetCollection<ChatViewModel>("Chats");
                var filter = Builders<ChatViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}