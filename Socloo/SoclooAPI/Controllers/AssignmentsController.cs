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
    public class AssignmentsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public AssignmentsController()
        {
            mongoDB = new MongoDBContext();
        }
        [HttpGet]
        public async Task<List<AssignmentViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<AssignmentViewModel>("Assignments").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<AssignmentViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<AssignmentViewModel>("Assignments");
                var filter = Builders<AssignmentViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] AssignmentViewModel assignment)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "TeachersId",bsonarray},
                 { "StudentsId", bsonarray},
                 { "ExpirationDate",Convert.ToDateTime(assignment.ExpirationDate)},
                { "Info", assignment.Info},
                { "FileId", assignment.FileId},
            };

            var collection = mongoDB.database.GetCollection<BsonDocument>("Assignments");
            await collection.InsertOneAsync(document);

        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] AssignmentViewModel assignment)
        {

            try
            {
                var document = new BsonDocument
            {

                 { "TeachersId", new BsonArray(assignment.TeachersId)},
                 { "StudentsId", new BsonArray(assignment.StudentsId)},
                { "ExpirationDate",Convert.ToDateTime(assignment.ExpirationDate)},
                { "Info", assignment.Info},
                { "FileId", assignment.FileId}
            };

                var collection = mongoDB.database.GetCollection<BsonDocument>("Assignments");
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
                var collection = mongoDB.database.GetCollection<AssignmentViewModel>("Assignments");
                var filter = Builders<AssignmentViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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