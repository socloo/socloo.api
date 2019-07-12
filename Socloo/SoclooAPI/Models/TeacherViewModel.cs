using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;
using System.Collections.Generic;

namespace SoclooAPI.Models
{
    public class TeacherViewModel
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]

        public string UserId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]

        public List<string> CoursesId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]

        public List<string> GroupsId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Subject { get; set; }
        public bool Deleted { get; set; } = false;
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
