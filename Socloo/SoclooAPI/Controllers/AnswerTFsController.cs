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
    public class AnswerTFsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public AnswerTFsController()
        {
            mongoDB = new MongoDBContext();

        }

        [HttpGet]
        public async Task<List<AnswerTF>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<AnswerTF>("AnswerTFs").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<AnswerTF> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<AnswerTF>("AnswerTFs");
                var filter = Builders<AnswerTF>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        async public void Post([FromBody] AnswerTF answerTF)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "QuestionId",ObjectId.Parse(answerTF.QuestionId)},
                 { "Correct",answerTF.Correct}
            };
            await mongoDB.database.GetCollection<BsonDocument>("AnswerTFs").InsertOneAsync(document);
        }
        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody]  AnswerTF answerTF)
        {

            var document = new BsonDocument
            {
                 { "QuestionId",ObjectId.Parse(answerTF.QuestionId)},
                 { "Correct",answerTF.Correct}
            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("AnswerTFs");
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
                var collection = mongoDB.database.GetCollection<AnswerTF>("AnswerTFs");
                var filter = Builders<AnswerTF>.Filter.Eq("_id", ObjectId.Parse(id));
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