﻿using System;
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
    public class SchoolAdminsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public SchoolAdminsController()
        {
            mongoDB = new MongoDBContext();

        }

        [HttpGet]
        public async Task<List<SchoolAdminViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<SchoolAdminViewModel>("SchoolAdmins").Find(new BsonDocument()).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<SchoolAdminViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<SchoolAdminViewModel>("SchoolAdmins");
                var filter = Builders<SchoolAdminViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] SchoolAdminViewModel schooladmin)
        {
            List<ObjectId> list = new List<ObjectId>();
            var bsonarray = new BsonArray(list);
            var document = new BsonDocument
            {
                 { "UserId", schooladmin.UserId},
                 { "TeachersId", bsonarray},
                 { "CoursesId",bsonarray},
                 { "GroupsId", bsonarray},
                 {"Type",schooladmin.Type }
            };
            await mongoDB.database.GetCollection<BsonDocument>("SchoolAdmins").InsertOneAsync(document);
        }
        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] SchoolAdminViewModel schooladmin)
        {

            var document = new BsonDocument
            {
                 { "UserId", schooladmin.UserId},
                 { "TeachersId", new BsonArray(schooladmin.TeachersId)},
                 { "CoursesId",new BsonArray(schooladmin.CoursesId)},
                 { "GroupsId", new BsonArray(schooladmin.GroupsId)},
                 {"Type",schooladmin.Type }

            };
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>("SchoolAdmins");
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
                var collection = mongoDB.database.GetCollection<SchoolAdminViewModel>("SchoolAdmins");
                var filter = Builders<SchoolAdminViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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