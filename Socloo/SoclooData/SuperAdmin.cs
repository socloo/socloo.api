using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class SuperAdmin : User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("SuperAdmins")]
        public ObjectId UserId { get; set; }
        public List<ObjectId> SchoolsId { get; set; }
    }
}
