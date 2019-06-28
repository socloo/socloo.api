using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Models
{
    public class DocumentViewModel
    {
        public ObjectId Id { get; set; }
        public string FileId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> UsersId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string TeacherId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public string DateTime { get; set; }
    }
}
