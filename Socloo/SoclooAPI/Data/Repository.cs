using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SoclooAPI.Data
{
    public class Repository<T> where T : IEntity<ObjectId>
    {
        public MongoDBContext mongoDB = new MongoDBContext();

        public Repository(DataContext context)
        {
            Context = context;

            CollectionName = typeof(T).Name.ToLower() + "s";

            Collection = Context.Database.GetCollection<T>(CollectionName);
        }

        protected int Limit { get; set; } = 1000; // max number of items returned from query

        protected DataContext Context { get; }

        protected IMongoCollection<T> Collection { get; }

        protected string CollectionName { get; }

        #region WRITE METHODS

        public async Task<T> InsertAsync(T instance)
        {
            try
            {
                instance.Id = ObjectId.GenerateNewId();

                await Collection.InsertOneAsync(instance);

                return instance;
            }
            catch (Exception ex)
            {
                Context.Logger.LogError(ex, "Insert");

                throw ex;
            }
        }

        public async void UpdateAsync(BsonDocument document, ObjectId id, string CollectionName)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>(CollectionName);

                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

                await collection.FindOneAndReplaceAsync(filter, document);
            }
            catch (Exception ex)
            {
                Context.Logger.LogError(ex, "Update");

            }
        }

        public async Task DeleteAsync(BsonDocument document, ObjectId id, string CollectionName, bool logical = true)
        {
            try
            {
                var collection = mongoDB.database.GetCollection<BsonDocument>(CollectionName);

                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);


                if (logical)
                {
                    await collection.FindOneAndReplaceAsync(filter, document);
                }
                else
                {
                    await collection.DeleteOneAsync(filter);
                }
            }
            catch (Exception ex)
            {
                Context.Logger.LogError(ex, "DeleteAsync");

                throw ex;
            }
        }

        public void InitColletion()
        {
            Context.Database.DropCollection(CollectionName);
        }

        #endregion WRITE METHODS

        #region QUERY METHODS

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> condition)
        {
            Context.Logger.LogDebug("REPOSITORY - GetSingle");

            try
            {
                var query = await Collection.FindAsync<T>(condition);

                var result = query.SingleOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                Context.Logger.LogError(ex, "GetSingle");

                throw ex;
            }
        }

        public async Task<T> GetByIdAsync(ObjectId id)
        {
            return await GetSingleAsync(o => o.Id == id);
        }

        public async Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> condition = null)
        {
            Context.Logger.LogDebug("REPOSITORY - GetList");

            try
            {
                IAsyncCursor<T> query;

                if (condition == null)
                {
                    condition = _ => true;
                }

                query = await Collection.Find(condition)
                    .Limit(Limit)
                    .ToCursorAsync();

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Context.Logger.LogError(ex, "GetList");

                throw ex;
            }
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> condition = null)
        {
            Context.Logger.LogDebug("REPOSITORY - Count");

            try
            {
                if (condition == null)
                {
                    condition = _ => true;
                }

                return await Collection.CountDocumentsAsync(condition);
                // return await Collection.CountAsync( condition );
            }
            catch (Exception ex)
            {
                Context.Logger.LogError(ex, "Count");

                throw ex;
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> condition)
        {
            Context.Logger.LogDebug("REPOSITORY - Exists");

            try
            {
                var result = await CountAsync(condition);

                return result > 0;
            }
            catch (Exception ex)
            {
                Context.Logger.LogError(ex, "Exists");

                throw ex;
            }
        }

        #endregion QUERY METHODS
    }
}