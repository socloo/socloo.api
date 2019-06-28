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
    public class AnswerTFsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public AnswerTFsController()
        {
            mongoDB = new MongoDBContext();

        }

        [HttpGet]
        public async Task<List<AnswerTFViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<AnswerTFViewModel>("AnswerTFs").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<AnswerTFViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<AnswerTFViewModel>("AnswerTFs");
                var filter = Builders<AnswerTFViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        async public void Post([FromBody] AnswerTFViewModel answerTF)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "AnswerId", ObjectId.Parse(answerTF.AnswerId)},
                 { "QuestionId",ObjectId.Parse(answerTF.QuestionId)},
                 { "Correct",answerTF.Correct}
            };
            await mongoDB.database.GetCollection<BsonDocument>("AnswerTFs").InsertOneAsync(document);
        }
        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody]  AnswerTFViewModel answerTF)
        {

            var document = new BsonDocument
            {
                 { "AnswerId", ObjectId.Parse(answerTF.AnswerId)},
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
                var collection = mongoDB.database.GetCollection<AnswerTFViewModel>("AnswerTFs");
                var filter = Builders<AnswerTFViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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