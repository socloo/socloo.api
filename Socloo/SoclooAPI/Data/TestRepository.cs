using MongoDB.Bson;
using SoclooAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Data
{
    public class TestRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    { 
        public TestRepository(DataContext context) : base(context)
        {
        }
    }
}
