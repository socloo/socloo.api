using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class Test
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Tests")]
        public List<string> TeachersId { get; set; }
        public List<string> StudentsId { get; set; }
        public TimeSpan TimeMax { get; set; }
        public string PictureId { get; set; }
        public List<string> QuestionsId { get; set; }

        public int Type { get; set; }

    }


}
