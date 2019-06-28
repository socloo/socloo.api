﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace SoclooAPI.Models
{
    public class AnswerViewModel
    {
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string SubclassId { get; set; }
        public int SubclassType { get; set; } //type 1: MC, type 2: SA, type 3: TF
    }
}