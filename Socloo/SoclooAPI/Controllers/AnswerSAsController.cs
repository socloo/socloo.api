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
    public class AnswerSAsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public AnswerSAsController()
        {
            mongoDB = new MongoDBContext();

        }

        [HttpGet]
        public async Task<List<AnswerSAViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<AnswerSAViewModel>("AnswerSAs").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<AnswerSAViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<AnswerSAViewModel>("AnswerSAs");
                var filter = Builders<AnswerSAViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] AnswerSAViewModel answerSA)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "Text", answerSA.Text},
                 { "QuestionId",ObjectId.Parse(answerSA.QuestionId)}
            };
            await mongoDB.database.GetCollection<BsonDocument>("AnswerSAs").InsertOneAsync(document);
        }

        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] AnswerSAViewModel answerSA)
        {

            var document = new BsonDocument
            {
                { "Text", answerSA.Text},
                { "QuestionId", ObjectId.Parse(answerSA.QuestionId)}
            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("AnswerSAs");
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
                var collection = mongoDB.database.GetCollection<AnswerSAViewModel>("AnswerSAs");
                var filter = Builders<AnswerSAViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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