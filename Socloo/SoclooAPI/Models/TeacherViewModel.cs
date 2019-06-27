using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

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
