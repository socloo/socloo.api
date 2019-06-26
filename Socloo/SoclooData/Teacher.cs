using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class Teacher : User
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Teachers")]
        public ObjectId UserId { get; set; }
        public List<ObjectId> CoursesId { get; set; }
        public List<ObjectId> GroupsId { get; set; }
        public List<ObjectId> Subject { get; set; }
    }
}
