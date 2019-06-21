using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socloo.Data
{
    class Answer
    {
        [BsonElement("Answers")]
        public string Text { get; set; }
    }
}
