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
        public string UserId { get; set; }
        public List<string> TeachersId { get; set; }
        public List<string> CoursesId { get; set; }
        public List<string> GroupsId { get; set; }
        public int Type { get; set; }

    }
}
