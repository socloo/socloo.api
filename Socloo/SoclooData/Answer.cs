
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socloo.Data
{
    public class Answer
    {
        public ObjectId Id { get; set; }
        [BsonElement("Answers")]
        public string Text { get; set; }
    }
}
