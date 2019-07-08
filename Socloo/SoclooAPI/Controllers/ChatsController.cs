using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SoclooAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<List<Chat>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<Chat>("Chats").Find(new BsonDocument()).ToListAsync();


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
                var collection = mongoDB.database.GetCollection<Chat>("Chats");
                var filter = Builders<Chat>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] Chat chat)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);

            var document = new BsonDocument
            {
                 { "UsersId",bsonarray},
                { "MessagesId", bsonarray},
                {"ChatName",chat.ChatName },
                { "ChatType", chat.ChatType},
            };

            var collection = mongoDB.database.GetCollection<BsonDocument>("Chats");
            await collection.InsertOneAsync(document);

        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Chat chat)
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
                var collection = mongoDB.database.GetCollection<Chat>("Chats");
                var filter = Builders<Chat>.Filter.Eq("_id", ObjectId.Parse(id));
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