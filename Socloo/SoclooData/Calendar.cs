using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    public class Calendar
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("Calendars")]
        public ObjectId UserId { get; set; }
        public List<ObjectId> OccurrencesId { get; set; }
    }
}
