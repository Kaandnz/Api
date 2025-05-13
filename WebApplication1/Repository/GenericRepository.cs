using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using WebApplication8.Settings;


namespace WebApplication8.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;   
        public GenericRepository(IMongoClient mongoClient , MongoDbSettings settings)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(typeof(T).Name.ToLower());
        }

        public void Add(T entity)
        {
            _collection.InsertOne(entity);
        }

        public void Delete(int id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            _collection.DeleteOne(filter);

        }

        public IEnumerable<T> GetAll() => _collection.Find(_ => true).ToList();

        public T GetById(int id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return _collection.Find(filter).FirstOrDefault();
        }

        public void Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", typeof(T).GetProperty("Id").GetValue(entity, null));
            _collection.ReplaceOne(filter, entity);
        }
    }
}
