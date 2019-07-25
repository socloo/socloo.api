using MongoDB.Bson;

namespace SoclooAPI.Data
{
    public class PortfolioRepository<T> : Repository<T> where T : ILocalizableEntity<ObjectId>

    {
        public PortfolioRepository(DataContext context) : base(context)
        {
        }
    }
}
