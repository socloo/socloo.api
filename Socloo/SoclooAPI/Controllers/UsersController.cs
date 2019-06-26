﻿using System;
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
    public class UsersController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public UsersController()
        {
            mongoDB = new MongoDBContext();
            
        }

        [HttpGet]
        public async Task<List<UserViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<UserViewModel>("Users").Find(new BsonDocument()).ToListAsync();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<UserViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<UserViewModel>("Users");
                var filter = Builders<UserViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] UserViewModel user)
        {
            var document = new BsonDocument
            {

                { "FullName", user.FullName},
                { "PhoneNumber", user.PhoneNumber},
                { "Email", user.Email},
                { "Bio", user.Bio},
                { "ProfilePictureId", user.ProfilePictureId}
            };
            await mongoDB.database.GetCollection<BsonDocument>("Users").InsertOneAsync(document);
        }
     

        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] UserViewModel user)
        {
        
            var document = new BsonDocument
            {
                { "FullName", user.FullName},
                { "PhoneNumber", user.PhoneNumber},
                { "Email", user.Email},
                { "Bio", user.Bio},
                { "ProfilePictureId", user.ProfilePictureId}
            };
            try
            {
                 var collection = mongoDB.database.GetCollection<BsonDocument>("Users");
                 var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
                 await collection.FindOneAndReplaceAsync(filter, document);
                 return true;
            }
            catch(Exception ex){
                return false;
            }   
        }


        [HttpDelete("{id}")]
        public async Task<bool> DeleteUserById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<UserViewModel>("Users");
                var filter = Builders<UserViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                await collection.DeleteOneAsync(filter);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}