using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class DashboardRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public DashboardRepository(DataContext context) : base(context)
        {
        }
    }
}