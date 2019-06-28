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
    public class QuestionsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public QuestionsController()
        {
            mongoDB = new MongoDBContext();

        }

        [HttpGet]
        public async Task<List<QuestionViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<QuestionViewModel>("Questions").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<QuestionViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<QuestionViewModel>("Questions");
                var filter = Builders<QuestionViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        async public void Post([FromBody] QuestionViewModel question)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "Text", question.Text}
            };
            await mongoDB.database.GetCollection<BsonDocument>("Questions").InsertOneAsync(document);
        }
        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] QuestionViewModel question)
        {

            var document = new BsonDocument
            {
                { "Text", question.Text}

            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("Questions");
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
                var collection = mongoDB.database.GetCollection<QuestionViewModel>("Questions");
                var filter = Builders<QuestionViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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