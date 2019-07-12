using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Models
{
    public class StudentViewModel
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

        public List<string> TeachersId { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
