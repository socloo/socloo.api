using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;
using System.Collections.Generic;

namespace SoclooAPI.Models
{
    public class Document : IEntity<ObjectId>
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public bool Deleted { get; set; } = false;
        [BsonRepresentation(BsonType.ObjectId)]
        public string FileId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> UsersId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string TeacherId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public string DateTime { get; set; }
    }
}
