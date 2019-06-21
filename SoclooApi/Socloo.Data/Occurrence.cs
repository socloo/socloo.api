using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Socloo.Data
{
    class Occurrence
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Occurrences")]
        public int Type { get; set; }
        public string TeacherId { get; set; }
        public DateTime Date { get; set; }
        public string Info { get; set; }

    }
}
