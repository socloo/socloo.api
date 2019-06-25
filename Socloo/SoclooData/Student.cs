using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class Student : User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Students")]
        public string UserId { get; set; }
        public List<string> CoursesId { get; set; }
        public List<string> GroupsId { get; set; }
        public string PortfolioId { get; set; }


    }
}


