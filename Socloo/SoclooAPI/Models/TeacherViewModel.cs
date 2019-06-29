using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace SoclooAPI.Models
{
    public class TeacherViewModel
    {
        public ObjectId id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]

        public string UserId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]

        public List<string> CoursesId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]

        public List<string> GroupsId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]

        public List<string> Subject { get; set; }
    }
}
