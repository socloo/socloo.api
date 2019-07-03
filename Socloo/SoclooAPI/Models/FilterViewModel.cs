using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Models
{
    public class FilterViewModel
    {
        public ObjectId id { get; set; }
        public string Text { get; set; }
    }
}
