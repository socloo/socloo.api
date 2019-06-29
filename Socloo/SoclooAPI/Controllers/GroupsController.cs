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
    public class GroupsController : ControllerBase
    {

        private MongoDBContext mongoDB;
        public GroupsController()
        {
            mongoDB = new MongoDBContext();

        }
        [HttpGet]
        public async Task<List<GroupViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<GroupViewModel>("Groups").Find(new BsonDocument()).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<GroupViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<GroupViewModel>("Groups");
                var filter = Builders<GroupViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody]  GroupViewModel group)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                { "StudentsId",bsonarray},
                { "TeachersId",bsonarray},
                { "Name", ""},
                { "Info", ""},
                { "PictureId", ""},
              
            };
            await mongoDB.database.GetCollection<BsonDocument>("Groups").InsertOneAsync(document);
        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] GroupViewModel group)
        {
           
            var document = new BsonDocument
            {
                 { "StudentsId", new BsonArray(group.StudentsId)},
                { "TeachersId", new BsonArray(group.TeachersId)},
                { "Name", "" + group.Name},
                { "Info", "" + group.Info},
                { "PictureId", ObjectId.Parse(group.PictureId)},
            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("Groups");
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
                var collection = mongoDB.database.GetCollection<GroupViewModel>("Groups");
                var filter = Builders<GroupViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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