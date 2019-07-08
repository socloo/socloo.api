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
    public class PorfoliosController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public PorfoliosController()
        {
            mongoDB = new MongoDBContext();

        }
        [HttpGet]
        public async Task<List<Porfolio>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<Porfolio>("Porfolios").Find(new BsonDocument()).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Porfolio> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<Porfolio>("Porfolios");
                var filter = Builders<Porfolio>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] Porfolio portfolio)
        {

            var document = new BsonDocument
            {
                { "UserId",ObjectId.Parse(portfolio.UserId)},
                { "Education", ""},
                { "Skills", ""},
                { "Experience", ""},
                { "Interests", ""},
                { "References", ""},
                { "GeneralInfo", ""},
                { "Certification", ""}
            };
            await mongoDB.database.GetCollection<BsonDocument>("Porfolios").InsertOneAsync(document);
        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] Porfolio portfolio)
        {

            var document = new BsonDocument
            {  { "UserId",ObjectId.Parse(portfolio.UserId)},
                { "Education", ""+portfolio.Education},
                { "Skills", ""+portfolio.Skills},
                { "Experience",""+ portfolio.Experience},
                { "Interests", ""+portfolio.Interests},
                { "References", ""+portfolio.References},
                { "GeneralInfo", ""+portfolio.GeneralInfo},
                { "Certification",""+portfolio.Certification}
            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("Porfolios");
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
                var collection = mongoDB.database.GetCollection<Porfolio>("Porfolios");
                var filter = Builders<Porfolio>.Filter.Eq("_id", ObjectId.Parse(id));
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