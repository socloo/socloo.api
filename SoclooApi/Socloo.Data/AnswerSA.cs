using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socloo.Data
{
    class AnswerSA : Answer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("AnswerSAs")]
        public string QuestionId { get; set; }
    }
}
