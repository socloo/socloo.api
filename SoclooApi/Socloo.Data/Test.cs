using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socloo.Data
{
    class Test
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Tests")]
        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
        public TimeSpan TimeMax { get; set; }
        public string PictureId { get; set; }
        public List<Question> Questions{ get; set; }

        public int Type { get; set; }

    }

    
}
