using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class AnswerMCRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public AnswerMCRepository(DataContext context) : base(context)
        {
        }
    }
}