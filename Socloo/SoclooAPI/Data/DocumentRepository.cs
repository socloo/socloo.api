using MongoDB.Bson;
using SoclooAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Data
{
    public class DocumentRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public DocumentRepository(DataContext context) : base(context)
        {
        }
    }
}