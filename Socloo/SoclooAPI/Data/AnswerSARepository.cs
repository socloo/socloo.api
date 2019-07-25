using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class AnswerSARepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public AnswerSARepository(DataContext context) : base(context)
        {
        }
    }
}