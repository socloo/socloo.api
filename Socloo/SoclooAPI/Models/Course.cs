using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;
using System;
using System.Collections.Generic;

namespace SoclooAPI.Models
{
    public class Course : IEntity<ObjectId>
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public bool Deleted { get; set; } = false;

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> StudentsId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> TeachersId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> CoordinatorsId { get; set; }

        public int Grade { get; set; }
        public String Section { get; set; }
        public String SubjectBranch { get; set; }
    }
}
