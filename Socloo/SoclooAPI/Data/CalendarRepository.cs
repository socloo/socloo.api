using MongoDB.Bson;
using SoclooAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Data
{
    public class CalendarRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public CalendarRepository(DataContext context) : base(context)
        {
        }
    }
}