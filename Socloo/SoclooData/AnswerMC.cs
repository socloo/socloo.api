
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socloo.Data
{
    public class AnswerMC : Answer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("AnswerMCs")]
        public string Text { get; set; }
        public ObjectId QuestionId { get; set; }
        public bool Correct { get; set; }
        public string Image { get; set; }

    }
}
