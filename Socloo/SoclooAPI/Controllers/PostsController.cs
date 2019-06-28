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
    public class PostsController : ControllerBase
    {
        private MongoDBContext mongoDB;
        public PostsController()
        {
            mongoDB = new MongoDBContext();
        }
        [HttpGet]
        public async Task<List<PostViewModel>> Get()
        {
            try
            {
                return await mongoDB.database.GetCollection<PostViewModel>("Posts").Find(new BsonDocument()).ToListAsync();


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("{id}")]
        public async Task<PostViewModel> GetById(string id)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<PostViewModel>("Posts");
                var filter = Builders<PostViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = await collection.Find(filter).ToListAsync();
                return result[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        async public void Post([FromBody] PostViewModel post)
        {

            var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(post.UserId)},
                 { "Title", post.Title},
                 { "Content",post.Content},
                { "Type", post.Type},
                { "PostDate", Convert.ToDateTime(post.PostDate) },
            };

            var collection = mongoDB.database.GetCollection<BsonDocument>("Posts");
            await collection.InsertOneAsync(document);

        }


        [HttpPut("{id}")]
        async public Task<bool> Put(string id, [FromBody] PostViewModel post)
        {

            try
            {
                var document = new BsonDocument
            {
                 { "UserId", ObjectId.Parse(post.UserId)},
                 { "Title", post.Title},
                 { "Content",post.Content},
                { "Type", post.Type},
                { "PostDate", Convert.ToDateTime(post.PostDate)},
            };

                var collection = mongoDB.database.GetCollection<BsonDocument>("Posts");
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
                var collection = mongoDB.database.GetCollection<PostViewModel>("Posts");
                var filter = Builders<PostViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
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