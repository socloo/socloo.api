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
    public class StudentsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public StudentsController()
        {
            mongoDB = new MongoDBContext();
        }
        [HttpGet]
        public async Task<List<Student>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<Student>("Students").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<Student> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<Student>("Users");
                var filter = Builders<Student>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] Student student)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                { "UserId", ObjectId.Parse(student.UserId)},
                { "TeachersId", bsonarray},
                { "CoursesId",bsonarray},
                { "GroupsId", bsonarray},
              { "PortfolioId", ObjectId.Empty},
            };

            var collection = mongoDB.database.GetCollection<BsonDocument>("Students");
            await collection.InsertOneAsync(document);

        }

        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Student student)
        {

            var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(student.UserId)},
                 { "TeachersId", new BsonArray(student.TeachersId)},
                 { "CoursesId",new BsonArray(student.CoursesId)},
                { "GroupsId", new BsonArray(student.GroupsId)},
                { "PortfolioId", ObjectId.Parse(student.PortfolioId)},
            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("Students");
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
                var collection = mongoDB.database.GetCollection<Student>("Students");
                var filter = Builders<Student>.Filter.Eq("_id", ObjectId.Parse(id));
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