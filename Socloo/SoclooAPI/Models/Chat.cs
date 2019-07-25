using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;
using System.Collections.Generic;
namespace SoclooAPI.Models
{
    public class Chat : IEntity<ObjectId>
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public bool Deleted { get; set; } = false;
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> UsersId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> MessagesId { get; set; }
        public string ChatName { get; set; }
        public int ChatType { get; set; }
    }
}
