﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Socloo.Data
{
    public class Occurrence
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Occurrences")]
        public int Type { get; set; }
        public ObjectId TeacherId { get; set; }
        public DateTime Date { get; set; }
        public string Info { get; set; }

    }
}