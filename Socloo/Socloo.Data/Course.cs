using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Socloo.Data
{
    class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Courses")]
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Teacher> Coordinators { get; set; }
        public int Grade { get; set; }
        public String Section { get; set; }
        public String SubjectBranch { get; set; }


    }
}

