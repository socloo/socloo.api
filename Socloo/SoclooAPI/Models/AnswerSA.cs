using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;

namespace SoclooAPI.Models
{
    public class AnswerSA : IEntity<ObjectId>
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public bool Deleted { get; set; } = false;
        public string Text { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]

        public string QuestionId { get; set; }
    }
}
