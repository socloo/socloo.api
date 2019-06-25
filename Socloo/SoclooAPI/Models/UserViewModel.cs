using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace SoclooAPI.Models
{
    public class UserViewModel
    {
       
        public ObjectId id { get; set; }

      
        public string FullName { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
     
        public string Bio { get; set; }
       
        public string ProfilePictureId { get; set; }
    }
}
