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
        public ObjectId id { get; set; }
        public int length { get; set; }
        public int chunkSize { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime uploadDate { get; set; }
        public string md5 { get; set; }
        public string filename { get; set; }
    }
}
