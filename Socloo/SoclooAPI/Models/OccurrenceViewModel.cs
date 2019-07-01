using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SoclooAPI.Models
{
    public class OccurrenceViewModel
    {
        public ObjectId Id { get; set; }

        public int Type { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string TeacherId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }
        public string Info { get; set; }
    }
}
