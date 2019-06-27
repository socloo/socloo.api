using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace SoclooAPI.Models
{
    public class AnswerSAViewModel
    {
        public ObjectId Id { get; set; }
        public string Text { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]

        public string AnswerId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]

        public string QuestionId { get; set; }
    }
}
