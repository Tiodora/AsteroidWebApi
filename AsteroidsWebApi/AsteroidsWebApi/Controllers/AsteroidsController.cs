using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AsteroidsWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class AsteroidsController : Controller
    {
        private readonly ILogger<AsteroidsController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public AsteroidsController(ILogger<AsteroidsController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [ResponseType(typeof(HttpResponseMessage))]
        [HttpGet("{planet}", Name = "AsteroidsList")]
        public async Task<ActionResult<Asteroids>> Get(string planet, DateTime startDate, DateTime endDate, int regsPage, int page)
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

                var httpClient = _httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    msgErr = "Couldn't retrieve content.";
                    return NotFound(msgErr);
                }

                string content = await httpResponseMessage.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                NasaData? nasaDate = JsonSerializer.Deserialize<NasaData>(content, options);

                List<Asteroids> AsteroidsList = new List<Asteroids>();
                List<NasaData> NasaDataList = new List<NasaData>();

                if (string.IsNullOrEmpty(planet) || string.IsNullOrWhiteSpace(planet))
                {
                    msgErr = "The planet is required";
                    return NotFound(msgErr);
                }

                return Ok(AsteroidsList);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
