using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class Chat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Chats")]
        public List<string> StudentsId { get; set; }
        public List<string> TeachersId { get; set; }
        public List<string> MessagesId { get; set; }
        public string ChatName { get; set; }
        public int ChatType { get; set; }



    }
}
