using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using SoclooAPI.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using Nancy.Json;
using MongoDB.Bson.IO;
using MongoDB.Driver.GridFS;
using Microsoft.Extensions.Caching.Memory;

namespace SoclooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        private MongoDBContext mongoDB;
        public FilesController()
        {
            mongoDB = new MongoDBContext();

        }


        [HttpPost]
        public bool UploadFiles(string filename, string path)
        {
            try
            {
                
                var fs = new GridFSBucket(mongoDB.database);
                var f = System.IO.File.OpenRead(path);
                byte[] bytes = System.IO.File.ReadAllBytes(path);
                var option = new GridFSUploadOptions
                {


                };
                fs.UploadFromBytes(filename, bytes, option);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet("{id}")]
        public bool DownLoadFiles(String id,string url)
        {
            
            try
            {
                var option = new GridFSUploadOptions
                {


                };
                var fs = new GridFSBucket(mongoDB.database);
                var collection = mongoDB.database.GetCollection<FileViewModel>("fs.files");
                var filter = Builders<FileViewModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var result = collection.Find(filter).ToList();
                var fileDate = result[0];
                var f = fs.DownloadAsBytes(ObjectId.Parse(id));
                var path = Path.Combine(url, fileDate.filename);
                System.IO.File.WriteAllBytes(@path, f);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}