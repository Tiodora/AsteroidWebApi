using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web.Http.Description;
using AsteroidsWebApi.DTO;
using AsteroidsWebApi.Service.Interface;
using AsteroidsWebApi.Wrappers;
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
        private readonly IAsteroidsService _asteroidsService;

        public AsteroidsController(ILogger<AsteroidsController> logger, IHttpClientFactory httpClientFactory, IAsteroidsService asteroidsService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _asteroidsService = asteroidsService;
        }

        [ResponseType(typeof(HttpResponseMessage))]
        [HttpGet("{planet}", Name = "AsteroidsList")]
        public IActionResult Get(string planet, DateTime startDate, DateTime endDate, int page, int regsPage)
        {
            try
            {
                PaginationFilter validFilter = new PaginationFilter(page, regsPage);
                List<Asteroids>? asteroids = _asteroidsService.Get(planet.ToUpper(), startDate, endDate, _httpClientFactory);
                if(asteroids == null)
                {
                    _logger.LogError("API call returned no data.");
                    return NoContent();
                }

                List<Asteroids> pagedData = asteroids.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                     .Take(validFilter.PageSize)
                                     .ToList();

                if (pagedData.Count == 0)
                {
                    _logger.LogError("No data for the selected page.");
                    return NoContent();
                }

                string response = JsonSerializer.Serialize(pagedData, new JsonSerializerOptions { WriteIndented = true });
                PagedResponse<string> paged = new PagedResponse<string>(response, page, regsPage);
                return Ok(paged);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured during execution. {e}");
                return NotFound(e.Message);
            }
        }
    }
}
