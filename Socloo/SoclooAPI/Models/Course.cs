using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace SoclooAPI.Models
{
    public class Course
    {
        public ObjectId Id { get; set; }

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
