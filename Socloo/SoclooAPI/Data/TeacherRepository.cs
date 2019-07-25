using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class TeacherRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public TeacherRepository(DataContext context) : base(context)
        {
        }
    }
}
