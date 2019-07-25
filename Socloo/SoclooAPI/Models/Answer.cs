using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;

namespace SoclooAPI.Models
{
    public class Answer : IEntity<ObjectId>
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public bool Deleted { get; set; } = false;
        [BsonRepresentation(BsonType.ObjectId)]
        public string SubclassId { get; set; }
        public int SubclassType { get; set; }


    }
}
