using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SoclooAPI.Models
{
    public class AnswerMCViewModel
    {
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string AnswerId { get; set; }
        public string Text { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string QuestionId { get; set; }
        public bool Correct { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Image { get; set; }
    }
}
