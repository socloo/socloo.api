using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SoclooAPI.Models
{
    public class Post
    {

        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime PostDate { get; set; }
    }
}
