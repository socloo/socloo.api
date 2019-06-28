using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SoclooAPI.Models
{
    public class QuestionViewModel
    {
        public ObjectId Id { get; set; }
        public string Text { get; set; }
    }
}
