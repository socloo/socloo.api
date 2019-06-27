using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Models
{
    public class GroupViewModel
    {
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> StudentsId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> TeachersId { get; set; }

        public string Name { get; set; }
        public string Info { get; set; }
        public string PictureId { get; set; }
    }
}
