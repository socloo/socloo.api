using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socloo.Data
{
    public class Portfolio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Portfolios")]
        public string Education { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }
        public string Interests { get; set; }
        public string References { get; set; }
        public string GeneralInfo { get; set; }
        public string Certification { get; set; }
    }
}
