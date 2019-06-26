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
    public class TeachersController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public TeachersController()
        {
            mongoDB = new MongoDBContext();

        }

        [HttpGet]

        public async Task<List<TeacherViewModel>> Get()
        {
            var collection = mongoDB.database.GetCollection<TeacherViewModel>("Teachers");
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<TeacherViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<TeacherViewModel>("Teachers");
                var filter = Builders<TeacherViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] TeacherViewModel teacher)
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

        [HttpDelete("{id}")]
        public async Task<bool> DeleteById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<TeacherViewModel>("Teachers");
                var filter = Builders<TeacherViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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