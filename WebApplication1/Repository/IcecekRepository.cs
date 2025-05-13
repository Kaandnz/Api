using MongoDB.Driver;
using WebApplication8.Models;
using WebApplication8.Settings;

namespace WebApplication8.Repository
{
    public class IcecekRepository : GenericRepository<Icecek>, IIcecekRepository
    {
        public IcecekRepository(IMongoClient mongoClient, MongoDbSettings settings)
            : base(mongoClient, settings) { }
    }
}
