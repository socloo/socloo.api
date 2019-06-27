using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Models
{
    public class PorfolioViewModel
    {
        public ObjectId id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string Education { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }
        public string Interests { get; set; }
        public string References { get; set; }
        public string GeneralInfo { get; set; }
        public string Certification { get; set; }
    }
}
