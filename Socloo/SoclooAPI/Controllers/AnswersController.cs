﻿using Microsoft.AspNetCore.Mvc;
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
    public class AnswersController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public AnswersController()
        {
            mongoDB = new MongoDBContext();

        }

        [HttpGet]
        public async Task<List<AnswerViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<AnswerViewModel>("Answers").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet("{id}")]
        public async Task<AnswerViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<AnswerViewModel>("Answers");
                var filter = Builders<AnswerViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] AnswerViewModel answer)
        {
            var document = new BsonDocument
                {
                     { "SubclassId", answer.SubclassId},
                     { "SubclassType", answer.SubclassType}
                };
            await mongoDB.database.GetCollection<BsonDocument>("Answers").InsertOneAsync(document);
        }

        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] AnswerViewModel answer)
        {

            var document = new BsonDocument
            {
                { "SubclassId", ObjectId.Parse(answer.SubclassId)},
                { "SubclassType", answer.SubclassType}
            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("Answers");
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
                var collection = mongoDB.database.GetCollection<AnswerViewModel>("Answers");
                var filter = Builders<AnswerViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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