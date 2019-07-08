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
    public class CoursesController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public CoursesController()
        {
            mongoDB = new MongoDBContext();

        }

        [HttpGet]
        public async Task<List<Course>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<Course>("Courses").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Course> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<Course>("Courses");
                var filter = Builders<Course>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] Course course)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "StudentsId", bsonarray},
                 { "TeachersId", bsonarray},
                 { "CoordinatorsId",bsonarray},
                 { "Grade", course.Grade},
                 { "Section", course.Section},
                 { "SubjectBranch", course.SubjectBranch}
            };
            await mongoDB.database.GetCollection<BsonDocument>("Courses").InsertOneAsync(document);
        }

        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Course course)
        {

            var document = new BsonDocument
            {
                 { "StudentsId", new BsonArray(course.StudentsId)},
                 { "TeachersId", new BsonArray(course.TeachersId)},
                 { "CoordinatorsId",new BsonArray(course.CoordinatorsId)},
                 { "Grade", course.Grade},
                 { "Section", course.Section},
                 { "SubjectBranch", course.SubjectBranch}
            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("Courses");
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
                var collection = mongoDB.database.GetCollection<Course>("Courses");
                var filter = Builders<Course>.Filter.Eq("_id", ObjectId.Parse(id));
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