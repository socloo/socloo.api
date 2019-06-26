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
            var collection = mongoDB.database.GetCollection<UserViewModel>("Users");
            return await collection.Find(new BsonDocument()).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<UserViewModel> GetById(string id)
        {
            var collection = mongoDB.database.GetCollection<UserViewModel>("Users");
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
            var collection = mongoDB.database.GetCollection<BsonDocument>("Users");
            await collection.InsertOneAsync(document);



        }
     

        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] UserViewModel user)
        {
        
            var document = new BsonDocument
            {
               // {"_id", id },
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
            var collection = mongoDB.database.GetCollection<UserViewModel>("Users");
            var list = collection.Find(new BsonDocument()).ToList();
            foreach (var col in list)
            {
                if (Convert.ToString(col.id).Equals(id))
                {
                    var filter = Builders<UserViewModel>.Filter.Eq("_id", col.id);
                   
                    await collection.DeleteOneAsync(filter);

                    return true;
                }
            }
            return false;


        }
    



        
        
    }
}