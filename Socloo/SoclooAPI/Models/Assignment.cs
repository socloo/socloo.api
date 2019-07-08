using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;
using System;
using System.Collections.Generic;
namespace SoclooAPI.Models
{
    public class Assignment : IEntity<ObjectId>
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public bool Deleted { get; set; } = false;

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> TeachersId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> StudentsId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime ExpirationDate { get; set; }
        public string Info { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string FileId { get; set; }
    }
}
