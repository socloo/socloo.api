using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class OccurrenceRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public OccurrenceRepository(DataContext context) : base(context)
        {
        }
    }
}
