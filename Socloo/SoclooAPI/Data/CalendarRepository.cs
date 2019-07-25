using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class CalendarRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public CalendarRepository(DataContext context) : base(context)
        {
        }
    }
}