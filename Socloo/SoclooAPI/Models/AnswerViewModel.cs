using System;
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
        public string Text { get; set; }
    }
}
