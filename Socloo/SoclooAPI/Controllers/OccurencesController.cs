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
    public class OccurencesController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public OccurencesController()
        {
            mongoDB = new MongoDBContext();
        }
        [HttpGet]
        public async Task<List<OccurrenceViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<OccurrenceViewModel>("Occurrences").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<OccurrenceViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<OccurrenceViewModel>("Occurrences");
                var filter = Builders<OccurrenceViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] OccurrenceViewModel occurrence)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                {"Type",occurrence.Type },
                 { "TeacherId", ObjectId.Parse(occurrence.TeacherId)},
                 { "Date",Convert.ToDateTime(occurrence.Date)},
                { "Info", ""+occurrence.Info},
            };

            var collection = mongoDB.database.GetCollection<BsonDocument>("Occurrences");
            await collection.InsertOneAsync(document);

        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] OccurrenceViewModel occurrence)
        {

            try
            {
                var document = new BsonDocument
            {
                 {"Type",occurrence.Type },
                 { "TeacherId", ObjectId.Parse(occurrence.TeacherId)},
                 { "Date",Convert.ToDateTime(occurrence.Date)},
                { "Info", ""+occurrence.Info},
            };

                var collection = mongoDB.database.GetCollection<BsonDocument>("Occurrences");
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
                var collection = mongoDB.database.GetCollection<OccurrenceViewModel>("Occurrences");
                var filter = Builders<OccurrenceViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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