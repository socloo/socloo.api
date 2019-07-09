using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;
using System;

namespace SoclooAPI.Models
{
    public class Message : IEntity<ObjectId>
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DataTime { get; set; }

        public string MessageText { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ChatId { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
