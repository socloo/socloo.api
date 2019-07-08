using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace SoclooAPI.Models
{
    public class SchoolAdmin
    {
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> TeachersId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> CoursesId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> GroupsId { get; set; }
        public int Type { get; set; }
    }
}
