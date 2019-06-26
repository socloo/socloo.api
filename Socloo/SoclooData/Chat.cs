using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class Chat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Chats")]
        public List<ObjectId> StudentsId { get; set; }
        public List<ObjectId> TeachersId { get; set; }
        public List<ObjectId> MessagesId { get; set; }
        public string ChatName { get; set; }
        public int ChatType { get; set; }



    }
}
