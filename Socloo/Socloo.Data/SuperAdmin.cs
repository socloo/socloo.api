using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    class SuperAdmin : User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("SuperAdmins")]

        public List<School> Schools { get; set; }
    }
}
