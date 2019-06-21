using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Socloo.Data
{
    class Assignment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Assignments")]
        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Info { get; set; }
        public string FileId { get; set; }

    }
}
