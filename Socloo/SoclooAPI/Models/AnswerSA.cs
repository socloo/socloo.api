using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SoclooAPI.Models
{
    public class AnswerSA
    {
        public ObjectId Id { get; set; }
        public string Text { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]

        public string QuestionId { get; set; }
    }
}
