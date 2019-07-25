using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class UserRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
