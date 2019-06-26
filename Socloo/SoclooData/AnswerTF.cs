
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socloo.Data
{
    public class AnswerTF
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("AnswerTFs")]
        public ObjectId QuestionId { get; set; }
        public bool Correct { get; set; }
    }
}
