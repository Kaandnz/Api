using WebApplication8.Models;

namespace WebApplication8.Repository
{
    public interface IGarsonRepository
    {
        IEnumerable<Garson> GetComplatedGarsons();
        IEnumerable<Garson> GetUnfinishedGarsons();


    }
}
