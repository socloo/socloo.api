using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Documents")]
        public string FileId { get; set; }
        public List<User> users { get; set; }
        public string TeacherId { get; set; }
        public DateTime DateTime { get; set; }

    }
}
