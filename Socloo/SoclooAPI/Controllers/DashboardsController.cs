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
using Newtonsoft.Json.Linq;

namespace SoclooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {

        private MongoDBContext mongoDB;
        public DashboardsController()
        {
            mongoDB = new MongoDBContext();

        }
        [HttpGet]
        public async Task<List<DashboardViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<DashboardViewModel>("Dashboards").Find(new BsonDocument()).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<DashboardViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<DashboardViewModel>("Dashboards");
                var filter = Builders<DashboardViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody]  DashboardViewModel dash)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                { "PostsId",bsonarray},

            };
            await mongoDB.database.GetCollection<BsonDocument>("Dashboards").InsertOneAsync(document);
        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] DashboardViewModel dash)
        {

            var document = new BsonDocument
            {
                 { "PostsId", new BsonArray(dash.PostsId)},
            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("Dashboards");
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
                var collection = mongoDB.database.GetCollection<DashboardViewModel>("Dashboards");
                var filter = Builders<DashboardViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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