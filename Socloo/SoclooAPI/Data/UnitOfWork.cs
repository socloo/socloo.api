using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Data
{
    public class UnitOfWork
    {
        DataContext _context;
        IDictionary<Type, object> _repository;
        IDictionary<Type, object> _geoRepository;

        public Repository<T> Repository<T>() where T : IEntity<ObjectId>
        {
            if (!_repository.Keys.Contains(typeof(T)))
            {
                var obj = new Repository<T>(_context);

                _repository.Add(typeof(T), obj);
            }

            return (Repository<T>)_repository[typeof(T)];
        }

        public UserRepository<T> UserRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new UserRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (UserRepository<T>)_geoRepository[typeof(T)];
        }

        public UnitOfWork(DataContext context)
        {
            _repository = new Dictionary<Type, object>();
            _geoRepository = new Dictionary<Type, object>();

            _context = context;

            context.UnitOfWork = this;
        }
    }
}
