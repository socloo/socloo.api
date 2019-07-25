using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class TestRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public TestRepository(DataContext context) : base(context)
        {
        }
    }
}
