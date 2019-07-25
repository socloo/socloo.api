using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class DocumentRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public DocumentRepository(DataContext context) : base(context)
        {
        }
    }
}