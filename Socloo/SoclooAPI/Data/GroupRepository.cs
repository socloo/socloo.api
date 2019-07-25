using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class GroupRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public GroupRepository(DataContext context) : base(context)
        {
        }
    }
}
