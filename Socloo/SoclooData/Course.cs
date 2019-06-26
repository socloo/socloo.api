using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Socloo.Data
{
    public  class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Courses")]
        public List<ObjectId> StudentsId { get; set; }
        public List<ObjectId> TeachersId { get; set; }
        public List<ObjectId> CoordinatorsId { get; set; }
        public int Grade { get; set; }
        public String Section { get; set; }
        public String SubjectBranch { get; set; }


    }
}

