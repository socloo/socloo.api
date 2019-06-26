using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class Group
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Groups")]
        public List<ObjectId> StudentsId { get; set; }
        public List<ObjectId> TeachersId { get; set; }

        public string Name { get; set; }
        public string Info { get; set; }
        public string PictureId { get; set; }
    }
}
