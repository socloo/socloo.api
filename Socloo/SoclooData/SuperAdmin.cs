using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class SuperAdmin : User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("SuperAdmins")]
        public string UserId { get; set; }
        public List<string> SchoolsId { get; set; }
    }
}
