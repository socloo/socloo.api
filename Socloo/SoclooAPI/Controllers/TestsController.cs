using System;
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
    public class TestsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public TestsController()
        {
            mongoDB = new MongoDBContext();

        }

        [HttpGet]
        public async Task<List<TestViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<TestViewModel>("Tests").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<TestViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<TestViewModel>("Tests");
                var filter = Builders<TestViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        async public void Post([FromBody] TestViewModel test)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "TeachersId", bsonarray},
                 { "StudentsId", bsonarray},
                 { "TimeMax",test.TimeMax},
                 { "PictureId", test.PictureId},
                 { "QuestionsId", bsonarray},
                 { "Type", test.Type}
            };
            await mongoDB.database.GetCollection<BsonDocument>("Tests").InsertOneAsync(document);
        }
        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] TestViewModel test)
        {

            var document = new BsonDocument
            {
                 { "TeachersId", new BsonArray(test.TeachersId)},
                 { "StudentsId", new BsonArray(test.StudentsId)},
                 { "TimeMax",test.TimeMax},
                 { "PictureId", test.PictureId},
                 { "QuestionsId", new BsonArray(test.QuestionsId)},
                 { "Type", test.Type}
            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("Tests");
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
                var collection = mongoDB.database.GetCollection<TestViewModel>("Tests");
                var filter = Builders<TestViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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