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
        public AnswerRepository<T> AnswerRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new AnswerRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (AnswerRepository<T>)_geoRepository[typeof(T)];
        }
        public AnswerMCRepository<T> AnswerMCRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new AnswerMCRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (AnswerMCRepository<T>)_geoRepository[typeof(T)];
        }
        public AnswerSARepository<T> AnswerSARepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new AnswerSARepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (AnswerSARepository<T>)_geoRepository[typeof(T)];
        }
        public AnswerTFRepository<T> AnswerTFRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new AnswerTFRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (AnswerTFRepository<T>)_geoRepository[typeof(T)];
        }
        public AssignmentRepository<T> AssignmentRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new AssignmentRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (AssignmentRepository<T>)_geoRepository[typeof(T)];
        }
        public CalendarRepository<T> CalendarRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new CalendarRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (CalendarRepository<T>)_geoRepository[typeof(T)];
        }
        public ChatRepository<T> ChatRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new ChatRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (ChatRepository<T>)_geoRepository[typeof(T)];
        }
        public CourseRepository<T> CourseRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new CourseRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (CourseRepository<T>)_geoRepository[typeof(T)];
        }
        public DashboardRepository<T> DashboardRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new DashboardRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (DashboardRepository<T>)_geoRepository[typeof(T)];
        }
        public DocumentRepository<T> DocumentRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new DocumentRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (DocumentRepository<T>)_geoRepository[typeof(T)];
        }
        public GroupRepository<T> GroupRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new GroupRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (GroupRepository<T>)_geoRepository[typeof(T)];
        }
        public MessageRepository<T> MessageRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new MessageRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (MessageRepository<T>)_geoRepository[typeof(T)];
        }
        public OccurrenceRepository<T> OccurrenceRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new OccurrenceRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (OccurrenceRepository<T>)_geoRepository[typeof(T)];
        }
        public PortfolioRepository<T> PorfolioRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new PortfolioRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (PortfolioRepository<T>)_geoRepository[typeof(T)];
        }
        public  PostRepository<T> PostRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new PostRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (PostRepository<T>)_geoRepository[typeof(T)];
        }


        public QuestionRepository<T> QuestionRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new QuestionRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (QuestionRepository<T>)_geoRepository[typeof(T)];
        }

        public SchoolAdminRepository<T> SchoolAdminRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new SchoolAdminRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (SchoolAdminRepository<T>)_geoRepository[typeof(T)];
        }



        public StudentRepository<T> StudentRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new StudentRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (StudentRepository<T>)_geoRepository[typeof(T)];
        }


        public SuperAdminRepository<T> SuperAdminRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new SuperAdminRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (SuperAdminRepository<T>)_geoRepository[typeof(T)];
        }

        public TeacherRepository<T> TeacherRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new TeacherRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (TeacherRepository<T>)_geoRepository[typeof(T)];
        }

        public TestRepository<T> TestRepository<T>() where T : ILocalizableEntity<ObjectId>
        {
            if (!_geoRepository.Keys.Contains(typeof(T)))
            {
                var obj = new TestRepository<T>(_context);

                _geoRepository.Add(typeof(T), obj);
            }

            return (TestRepository<T>)_geoRepository[typeof(T)];
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
