using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class PostRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public PostRepository(DataContext context) : base(context)
        {
        }
    }
}
