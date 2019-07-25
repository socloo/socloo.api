using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class AnswerTFRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public AnswerTFRepository(DataContext context) : base(context)
        {
        }
    }
}