﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SoclooAPI.Models
{
    public class UserViewModel
    {
        public ObjectId id { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProfilePictureId { get; set; }
    }
}
