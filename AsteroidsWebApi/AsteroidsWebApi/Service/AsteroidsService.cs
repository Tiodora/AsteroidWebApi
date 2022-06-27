using System.Text.Json;
using AsteroidsWebApi.DTO;
using AsteroidsWebApi.Service.Interface;

namespace AsteroidsWebApi.Service
{
    public class AsteroidsService : IAsteroidsService
    {
        private readonly ILogger<AsteroidsService> _logger;
        private readonly ConvertTo _convertTo;
        public AsteroidsService(ILogger<AsteroidsService> logger)
        {
            _logger = logger;
            _convertTo = new ConvertTo();
        }

        public List<Asteroids>? Get(string planet, DateTime startDate, DateTime endDate, IHttpClientFactory httpClientFactory)
        {
            try
            {
                if (!DateRange(ref startDate, ref endDate)) return null;

                Task<NasaData?> nasaDate = JsonOperations(startDate, endDate, httpClientFactory, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                List<Asteroids> AsteroidsList = _convertTo.NasaDataToAsteroids(nasaDate.Result, planet);
                return AsteroidsList;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured during execution. {e}");
                return null;
            }
        }

        private bool DateRange(ref DateTime startDate, ref DateTime endDate)
        {
            try
            {
                Dictionary<DateTime, string> asteroids = new Dictionary<DateTime, string>();
                if (startDate.Equals(default(DateTime)) && endDate.Equals(default(DateTime)))
                {
                    startDate = DateTime.UtcNow;
                    endDate = startDate.AddDays(7);
                }
                else if (!startDate.Equals(default(DateTime)) && endDate.Equals(default(DateTime)))
                    endDate = startDate.AddDays(7);
                else if (startDate.Equals(default(DateTime)) && !endDate.Equals(default(DateTime)))
                    startDate = endDate.AddDays(-7);
                else
                {
                    if (endDate.Subtract(startDate).TotalDays > 7)
                        endDate = startDate.AddDays(7);
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured checking the dates. {e}");
                return false;
            }
        }

        private async Task<NasaData?> JsonOperations(DateTime startDate, DateTime endDate, IHttpClientFactory httpClientFactory, JsonSerializerOptions options)
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate.ToString("yyyy-MM-dd")}&end_date={endDate.ToString("yyyy-MM-dd")}&api_key=DEMO_KEY");

                var httpClient = httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    _logger.LogError($"Couldn't retrieve content.");
                    return null;
                }

                string content = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<NasaData>(content, options);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured while recovering the data. {e}");
                return null;
            }
        }
    }
}
