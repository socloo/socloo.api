﻿using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socloo.Data
{
    class AnswerMC
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("AnswerMCs")]
        public string Text { get; set; }
        public string QuestionId { get; set; }
        public bool Correct { get; set; }
        public string Image { get; set; }

    }
}
