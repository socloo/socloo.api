using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Socloo.Data
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Posts")]
        public string Title { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        public DateTime PostDate { get; set; }
    }
}
