using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoclooAPI.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {

        private MongoDBContext mongoDB;
        public MessagesController()
        {
            mongoDB = new MongoDBContext();
        }
        [HttpGet]
        public async Task<List<Message>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<Message>("Messages").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Message> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<Message>("Messages");
                var filter = Builders<Message>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] Message message)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            message.MessageText = new Filter().RemoveBadWord(message.MessageText);
            var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(message.UserId)},
                 { "DataTime", Convert.ToDateTime(message.DataTime)},
                 { "MessageText",message.MessageText},
                { "ChatId",  ObjectId.Parse(message.ChatId)},
            };

            var collection = mongoDB.database.GetCollection<BsonDocument>("Messages");
            await collection.InsertOneAsync(document);

        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Message message)
        {

            try
            {
                var document = new BsonDocument
            {
                  { "UserId", ObjectId.Parse(message.UserId)},
                 { "DataTime", Convert.ToDateTime(message.DataTime)},
                 { "MessageText",message.MessageText},
                { "ChatId",  ObjectId.Parse(message.ChatId)},
            };

                var collection = mongoDB.database.GetCollection<BsonDocument>("Messages");
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
                var collection = mongoDB.database.GetCollection<Message>("Messages");
                var filter = Builders<Message>.Filter.Eq("_id", ObjectId.Parse(id));
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