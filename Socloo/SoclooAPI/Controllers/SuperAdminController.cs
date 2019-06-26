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
    public class SuperAdminController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public SuperAdminController()
        {
            mongoDB = new MongoDBContext();
        }
        [HttpGet]
        public async Task<List<SuperAdminModel>> Get()
        {
            var collection = mongoDB.database.GetCollection<SuperAdminModel>("SuperAdmins");
            return await collection.Find(new BsonDocument()).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<SuperAdminModel> GetById(string id)
        {
            var collection = mongoDB.database.GetCollection<SuperAdminModel>("SuperAdmins");
            var list = collection.Find(new BsonDocument()).ToList();
         

            foreach (var col in list)
            {
                if (Convert.ToString(col.id).Equals(id))
                {
                    return col;
                }
            }
            return null;
        }

        [HttpPost]
        async public void Post([FromBody] SuperAdminModel admin)
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

    }
}