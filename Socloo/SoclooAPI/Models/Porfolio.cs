using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;

namespace SoclooAPI.Models
{
    public class Porfolio : IEntity<ObjectId>
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string Education { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }
        public string Interests { get; set; }
        public string References { get; set; }
        public string GeneralInfo { get; set; }
        public string Certification { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
