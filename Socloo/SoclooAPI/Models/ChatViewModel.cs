using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SoclooAPI.Models
{
    public class ChatViewModel
    {
       
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> StudentsId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> TeachersId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> MessagesId { get; set; }
        public string ChatName { get; set; }
        public int ChatType { get; set; }
    }
}
