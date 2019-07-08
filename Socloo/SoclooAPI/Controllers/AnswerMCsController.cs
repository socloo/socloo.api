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
    public class AnswerMCsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public AnswerMCsController()
        {
            mongoDB = new MongoDBContext();

        }
        [HttpGet]
        public async Task<List<AnswerMC>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<AnswerMC>("AnswerMCs").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<AnswerMC> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<AnswerMC>("AnswerMCs");
                var filter = Builders<AnswerMC>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] AnswerMC answerMC)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "Text", answerMC.Text},
                 { "QuestionId",ObjectId.Parse(answerMC.QuestionId)},
                 { "Correct", answerMC.Correct},
                 { "Image",  ObjectId.Parse(answerMC.Image)}
            };
            await mongoDB.database.GetCollection<BsonDocument>("AnswerMCs").InsertOneAsync(document);
        }

        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] AnswerMC answerMC)
        {

            var document = new BsonDocument
            {
                 { "Text", answerMC.Text},
                 { "QuestionId",ObjectId.Parse(answerMC.QuestionId)},
                 { "Correct", answerMC.Correct},
                 { "Image", ObjectId.Parse(answerMC.Image)}
            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("AnswerMCs");
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
                var collection = mongoDB.database.GetCollection<AnswerMC>("AnswerMCs");
                var filter = Builders<AnswerMC>.Filter.Eq("_id", ObjectId.Parse(id));
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