using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class SchoolAdmin : User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("SchoolAdmins")]
        public ObjectId UserId { get; set; }
        public List<ObjectId> TeachersId { get; set; }
        public List<ObjectId> CoursesId { get; set; }
        public List<ObjectId> GroupsId { get; set; }
        public int Type { get; set; }

    }
}
