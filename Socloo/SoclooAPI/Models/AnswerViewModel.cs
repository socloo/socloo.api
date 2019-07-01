using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SoclooAPI.Models
{
    public class AnswerViewModel
    {
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string SubclassId { get; set; }
        public int SubclassType { get; set; } 

        
    }
}
