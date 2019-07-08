using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SoclooAPI.Models
{
    public class AnswerTF
    {
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]

        public string QuestionId { get; set; }
        public bool Correct { get; set; }
    }
}
