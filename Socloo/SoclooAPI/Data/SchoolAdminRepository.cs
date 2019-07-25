using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class SchoolAdminRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public SchoolAdminRepository(DataContext context) : base(context)
        {
        }
    }
}
