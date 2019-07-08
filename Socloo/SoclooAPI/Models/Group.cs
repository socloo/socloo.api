using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace SoclooAPI.Models
{
    public class Group
    {
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> StudentsId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> TeachersId { get; set; }

        public string Name { get; set; }
        public string Info { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string PictureId { get; set; }
    }
}
