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
        public List<UserViewModel> Get()
        {
            var JsonUser= mongoDB.database.GetCollection<UserViewModel>("Users");
            
            var AllUsers = mongoDB.database.GetCollection<UserViewModel>("Users").AsQueryable<UserViewModel>().ToList<UserViewModel>();


            return AllUsers;
                //.Find<UserViewModel>(_ => true).ToList;
                //ToList<UserViewModel>;
        
        }
        [HttpGet("id")]
        public UserViewModel Get(string id)
        {
            var JsonUser = mongoDB.database.GetCollection<UserViewModel>("Users");
            var AllUsers = mongoDB.database.GetCollection<UserViewModel>("Users").AsQueryable<UserViewModel>();
            foreach(var user in AllUsers)
            {
                if (user.id.Equals(id))
                {
                    return user;
                }
            }
            return null;

        }
        [HttpDelete("id")]
        public bool Delete(string id)
        {
            var JsonUser = mongoDB.database.GetCollection<UserViewModel>("Users");
            try
            {
                JsonUser.FindOneAndDelete(id);
                
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
          
        }

        [HttpPost]
        async public void Post([FromBody] UserViewModel user)
        {
            /*
            var JsonUser = mongoDB.database.GetCollection<UserViewModel>("Users");
            JsonUser.InsertOne(user);
            */

            var document = new BsonDocument
            {
                {"Id", ObjectId.GenerateNewId() },
                { "FullName", user.FullName},
                { "PhoneNumber", user.PhoneNumber},
                { "Email", user.Email},
                { "Bio", user.Bio},
                { "ProfilePictureId", user.ProfilePictureId}
            };
            var collection = mongoDB.database.GetCollection<BsonDocument>("Users");
            await collection.InsertOneAsync(document);
           
            //   await collection.UpdateOneAsync(document);
       
        }
        [HttpPut]
        public bool Put([FromBody] UserViewModel userMod)
        {
            var JsonUser = mongoDB.database.GetCollection<UserViewModel>("Users");
            var AllUsers = mongoDB.database.GetCollection<UserViewModel>("Users").AsQueryable<UserViewModel>();
            foreach (var user in AllUsers)
            {
                if (user.id.Equals(userMod.id))
                {
                    user.PhoneNumber = userMod.PhoneNumber;
                    user.Bio = user.Bio;
                    user.Email = user.Email;
                    user.FullName = user.FullName;
                    user.ProfilePictureId = user.ProfilePictureId;
                    return true;
                }
            }
            return false;
        }
    }
}