using System.Text.Json;
using AsteroidsWebApi.DTO;
using AsteroidsWebApi.Service.Interface;

namespace AsteroidsWebApi.Service
{
    public class AsteroidsService : IAsteroidsService
    {
        private readonly ILogger<AsteroidsService> _logger;
        public AsteroidsService(ILogger<AsteroidsService> logger)
        {
            _logger = logger;
        }
        public async Task<string> Get(string planet, DateTime startDate, DateTime endDate, int regsPage, int page, IHttpClientFactory httpClientFactory)
        {
            try
            {
                string msgErr;
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

                var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate.ToString("yyyy-MM-dd")}&end_date={endDate.ToString("yyyy-MM-dd")}&api_key=DEMO_KEY");

                var httpClient = httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    msgErr = "Couldn't retrieve content.";
                    return null;
                }

                string content = await httpResponseMessage.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                NasaData? nasaDate = JsonSerializer.Deserialize<NasaData>(content, options);

                ConvertTo convert = new ConvertTo();
                List<Asteroids> AsteroidsList = convert.NasaDataToAsteroids(nasaDate);

                //if (string.IsNullOrEmpty(planet) || string.IsNullOrWhiteSpace(planet))
                //{
                //    msgErr = "The planet is required";
                //    return null;
                //}

                options = new JsonSerializerOptions { WriteIndented = true };
                string result = JsonSerializer.Serialize(AsteroidsList, options);
                Console.WriteLine(result);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
