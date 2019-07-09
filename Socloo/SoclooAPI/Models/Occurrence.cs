using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;
using System;

namespace SoclooAPI.Models
{
    public class Occurrence : IEntity<ObjectId>
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        public int Type { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string TeacherId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }
        public string Info { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
