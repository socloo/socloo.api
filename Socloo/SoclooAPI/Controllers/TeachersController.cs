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
    public class TeachersController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public TeachersController()
        {
            mongoDB = new MongoDBContext();

        }

        [HttpGet]
        public async Task<List<Teacher>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<Teacher>("Teachers").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Teacher> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<Teacher>("Teachers");
                var filter = Builders<Teacher>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] Teacher teacher)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "UserId", teacher.UserId},
                 { "CoursesId", bsonarray},
                 { "GroupsId",bsonarray},
                { "Subject", bsonarray},
            };
            await mongoDB.database.GetCollection<BsonDocument>("Teachers").InsertOneAsync(document);
        }

        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Teacher teacher)
        {

            var document = new BsonDocument
            {
                { "UserId", teacher.UserId},
                { "CoursesId", new BsonArray(teacher.CoursesId)},
                { "GroupsId", new BsonArray(teacher.GroupsId)},
                { "Subject", new BsonArray(teacher.Subject)}
            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("Teachers");
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
                var collection = mongoDB.database.GetCollection<Teacher>("Teachers");
                var filter = Builders<Teacher>.Filter.Eq("_id", ObjectId.Parse(id));
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