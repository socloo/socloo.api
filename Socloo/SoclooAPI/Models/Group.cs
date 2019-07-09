using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SoclooAPI.Data;
using System.Collections.Generic;

namespace SoclooAPI.Models
{
    public class Group : IEntity<ObjectId>
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> StudentsId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> TeachersId { get; set; }

        public string Name { get; set; }
        public string Info { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string PictureId { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
