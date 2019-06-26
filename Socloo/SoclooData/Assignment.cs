using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class Assignment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Assignments")]
        public List<ObjectId> TeachersId { get; set; }
        public List<ObjectId> StudentsId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Info { get; set; }
        public string FileId { get; set; }

    }
}
