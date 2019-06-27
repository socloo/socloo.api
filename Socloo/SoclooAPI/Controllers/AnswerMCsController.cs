﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Socloo.Data;
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
    public class AnswerMCsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public AnswerMCsController()
        {
            mongoDB = new MongoDBContext();

        }
        [HttpGet]
        public async Task<List<AnswerMCViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<AnswerMCViewModel>("AnswerMCs").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<AnswerMCViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<AnswerMCViewModel>("AnswerMCs");
                var filter = Builders<AnswerMCViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] AnswerMCViewModel answerMC)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "AnswerId", ObjectId.Parse(answerMC.AnswerId)},
                 { "Text", answerMC.Text},
                 { "QuestionId",ObjectId.Parse(answerMC.QuestionId)},
                 { "Correct", answerMC.Correct},
                 { "Image", answerMC.Image}
            };
            await mongoDB.database.GetCollection<BsonDocument>("AnswerMCs").InsertOneAsync(document);
        }

        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] AnswerMCViewModel answerMC)
        {

            var document = new BsonDocument
            {
                 { "AnswerId", ObjectId.Parse(answerMC.AnswerId)},
                 { "Text", answerMC.Text},
                 { "QuestionId",ObjectId.Parse(answerMC.QuestionId)},
                 { "Correct", answerMC.Correct},
                 { "Image", answerMC.Image}
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
                var collection = mongoDB.database.GetCollection<AnswerMCViewModel>("AnswerMCs");
                var filter = Builders<AnswerMCViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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