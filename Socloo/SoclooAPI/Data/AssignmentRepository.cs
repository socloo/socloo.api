using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class AssignmentRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public AssignmentRepository(DataContext context) : base(context)
        {
        }
    }
}