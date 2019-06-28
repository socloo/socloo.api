using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Models
{
    public class FileViewModel
    {
        public ObjectId Id { get; set; }
        public int Length { get; set; }
        public int ChunkSize { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UploadDate { get; set; }
        public string Md5 { get; set; }
        public string Filename { get; set; }
    }
}
