using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class CourseRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public CourseRepository(DataContext context) : base(context)
        {
        }
    }
}