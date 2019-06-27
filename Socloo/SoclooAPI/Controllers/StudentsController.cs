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
    public class StudentsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public StudentsController()
        {
            mongoDB = new MongoDBContext();
        }
        [HttpGet]
        public async Task<List<StudentViewModel>> Get()
        {
            try
            {
               return await mongoDB.database.GetCollection<StudentViewModel>("Students").Find(new BsonDocument()).ToListAsync();
                

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
       /* public async Task<StudentViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("Students");
                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                var student = new StudentViewModel
                {
                    CoursesId =,

                };

                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        */
        [HttpPost]
        async public void Post([FromBody] StudentViewModel student)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(student.UserId)},
                 { "TeachersId", bsonarray},
                 { "CoursesId",bsonarray},
                { "GroupsId", bsonarray},
                { "PortfolioId", ObjectId.Parse(student.PortfolioId)},
            };

            var collection = mongoDB.database.GetCollection<BsonDocument>("Students");
            await collection.InsertOneAsync(document);

        }

    }
}