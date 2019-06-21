using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socloo.Data
{
    class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Messages")]
        public string UserId { get; set; }
        public DateTime DataTime { get; set; }
           
        public string MessageText { get; set; }
        public string ChatId { get; set; }

    }
}
