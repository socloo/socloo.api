using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;
using System.Collections.Generic;


namespace SoclooAPI.Models
{
    public class Dashboard : IEntity<ObjectId>
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public bool Deleted { get; set; } = false;

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> UsersId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> PostsId { get; set; }
    }
}
