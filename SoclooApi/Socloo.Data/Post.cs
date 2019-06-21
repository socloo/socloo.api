using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socloo.Data
{
    class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Posts")]
        public string Title { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        public DateTime PostDate{ get; set; }
    }
}
