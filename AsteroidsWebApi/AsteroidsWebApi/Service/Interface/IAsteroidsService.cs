using AsteroidsWebApi.DTO;

namespace AsteroidsWebApi.Service.Interface
{
    public interface IAsteroidsService
    {
        Task<string> Get(string planet, DateTime startDate, DateTime endDate, int regsPage, int page, IHttpClientFactory httpClientFactory);
    }
}
