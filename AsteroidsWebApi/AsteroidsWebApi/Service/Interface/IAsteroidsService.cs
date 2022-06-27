using AsteroidsWebApi.DTO;

namespace AsteroidsWebApi.Service.Interface
{
    public interface IAsteroidsService
    {
        List<Asteroids>? Get(string planet, DateTime startDate, DateTime endDate, IHttpClientFactory httpClientFactory);
    }
}
