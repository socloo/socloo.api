using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Socloo.Data
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Messages")]
        public ObjectId UserId { get; set; }
        public DateTime DataTime { get; set; }

        public string MessageText { get; set; }
        public ObjectId ChatId { get; set; }

    }
}
