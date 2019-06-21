using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Socloo.Data
{
    class Calendar
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Calendars")]
        public string UserId { get; set; }
        public List<Occurrence> Occurrences { get; set; }
    }
}
