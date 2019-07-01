using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SoclooAPI.Models
{
    public class MessageViewModel
    {
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DataTime { get; set; }

        public string MessageText { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ChatId { get; set; }
    }
}
