using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SoclooAPI.Models
{
    public class StudentViewModel
    {
        public ObjectId Id { get; set; }

        
        public string UserId { get; set; }
        public List<ObjectId> CoursesId { get; set; }
        public List<ObjectId> GroupsId { get; set; }
        public string PortfolioId { get; set; }

    }
}
