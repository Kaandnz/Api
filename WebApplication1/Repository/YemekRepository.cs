
using MongoDB.Driver;
using WebApplication8.Models;
using WebApplication8.Settings;

namespace WebApplication8.Repository
{
    public class YemekRepository : GenericRepository<Yemek>, IYemekRepository
    {
        public YemekRepository(IMongoClient mongoClient, MongoDbSettings settings)
            : base(mongoClient, settings) { }
    }
}
