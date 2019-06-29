using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace SoclooAPI.Models
{
    public class PostViewModel
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public string PostDate { get; set; }
    }
}
