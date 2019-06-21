using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class SchoolAdmin : User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("SchoolAdmins")]
        public List<Teacher> Teachers { get; set; }
        public List<Course> Courses { get; set; }
        public List<Group> Groups { get; set; }
        public int Type { get; set; }

    }
}
