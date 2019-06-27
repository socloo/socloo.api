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
    public class SuperAdminsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public SuperAdminsController()
        {
            mongoDB = new MongoDBContext();
        }
        [HttpGet]
        public async Task<List<SuperAdminViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<SuperAdminViewModel>("SuperAdmins").Find(new BsonDocument()).ToListAsync();
                
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<SuperAdminViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<SuperAdminViewModel>("SuperAdmins");
                var filter = Builders<SuperAdminViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] SuperAdminViewModel admin)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(admin.UserId)},
                 { "TeachersId", bsonarray},
                 { "CoursesId",bsonarray},
                { "GroupsId", bsonarray},
                { "Type", admin.Type},
            };

            var collection = mongoDB.database.GetCollection<BsonDocument>("SuperAdmins");
            await collection.InsertOneAsync(document);

        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] SuperAdminViewModel admin)
        {

            try
            {
                var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(admin.UserId)},
                 { "TeachersId", new BsonArray(admin.TeachersId)},
                 { "CoursesId",new BsonArray(admin.CoursesId)},
                { "GroupsId",new BsonArray(admin.GroupsId)},
                { "Type", admin.Type},
            };
           
                var collection = mongoDB.database.GetCollection<BsonDocument>("SuperAdmins");
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
                var collection = mongoDB.database.GetCollection<SuperAdminViewModel>("SuperAdmins");
                var filter = Builders<SuperAdminViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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