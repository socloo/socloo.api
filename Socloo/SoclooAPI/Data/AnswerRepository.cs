using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class AnswerRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public AnswerRepository(DataContext context) : base(context)
        {
        }
    }
}