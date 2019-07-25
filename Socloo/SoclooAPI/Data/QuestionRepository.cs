using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class QuestionRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public QuestionRepository(DataContext context) : base(context)
        {
        }
    }
}
