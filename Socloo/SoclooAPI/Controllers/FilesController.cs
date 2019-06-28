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
        public bool DownLoadFiles(String id)
        {
            
            try
            {
                var option = new GridFSUploadOptions
                {


                };
                var fs = new GridFSBucket(mongoDB.database);
                var fileDate = mongoDB.database.GetCollection<FileViewModel>("fs.files").Find(new BsonDocument()).ToList();
                var f = fs.DownloadAsBytes(ObjectId.Parse(id));
                var 
                System.IO.File.WriteAllBytes(@"C:\Users\nicco\Downloads\prova7.pdf", f);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}