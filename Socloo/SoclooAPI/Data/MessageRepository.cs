using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class MessageRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public MessageRepository(DataContext context) : base(context)
        {
        }
    }
}
