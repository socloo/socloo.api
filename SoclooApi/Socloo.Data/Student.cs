using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    class Student : User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Students")]
        public List<Course> Courses { get; set; }
        public List<Group> Groups { get; set; }
        public Portfolio Portfolio { get; set; }


    }
}


