using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class ChatRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public ChatRepository(DataContext context) : base(context)
        {
        }
    }
}