using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class SuperAdminRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public SuperAdminRepository(DataContext context) : base(context)
        {
        }
    }
}
