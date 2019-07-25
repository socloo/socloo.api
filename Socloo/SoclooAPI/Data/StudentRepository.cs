using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class StudentRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public StudentRepository(DataContext context) : base(context)
        {
        }
    }
}
