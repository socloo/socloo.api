using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
namespace SoclooAPI.Models
{
    public class Chat
    {

        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> UsersId { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> MessagesId { get; set; }
        public string ChatName { get; set; }
        public int ChatType { get; set; }
    }
}
