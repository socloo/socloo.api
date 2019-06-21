using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socloo.Data
{
    class Chat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Chats")]
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Message> Messages { get; set; }
        public string ChatName { get; set; }
        public int ChatType { get; set; }



    }
}
