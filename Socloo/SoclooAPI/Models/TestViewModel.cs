﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace SoclooAPI.Models
{
    public class TestViewModel
    {
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]

        public List<ObjectId> TeachersId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]

        public List<ObjectId> StudentsId { get; set; }
        public double TimeMax { get; set; }
        public string PictureId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]

        public List<ObjectId> QuestionsId { get; set; }

        public int Type { get; set; }
    }
}
