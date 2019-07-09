using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;

namespace SoclooAPI.Models
{
    public class Question : IEntity<ObjectId>
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public string Text { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
