﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;
using System.Collections.Generic;
namespace SoclooAPI.Models
{
    public class Student : IEntity<ObjectId>
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
        [BsonRepresentation(BsonType.ObjectId)]
        public string PortfolioId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string SchoolId { get; set; }
        public bool Deleted { get; set; } = false;

    }
}
