using MongoDB.Driver;
using WebApplication8.Models;
using WebApplication8.Settings;

namespace WebApplication8.Repository
{
    public class GarsonRepository : GenericRepository<Garson>, IGarsonRepository
    {
        private readonly IMongoCollection<Garson> _garsonCollection;

        public GarsonRepository(IMongoClient mongoClient, MongoDbSettings settings)
            : base(mongoClient, settings)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _garsonCollection = database.GetCollection<Garson>("garsons");
        }

        public IEnumerable<Garson> GetComplatedGarsons()
        {
            return _garsonCollection.Find(g => g.siparisTamamlandi == true).ToList();
        }

        public IEnumerable<Garson> GetUnfinishedGarsons()
        {
            return _garsonCollection.Find(g => g.siparisTamamlandi == false).ToList();
        }
    }
}
