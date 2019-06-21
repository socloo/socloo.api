
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socloo.Data
{
    class AnswerTF
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("AnswerTFs")]
        public string QuestionId { get; set; }
        public bool Correct { get; set; }
    }
}
