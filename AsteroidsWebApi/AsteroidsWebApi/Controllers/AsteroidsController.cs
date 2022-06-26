using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web.Http.Description;
using AsteroidsWebApi.DTO;
using AsteroidsWebApi.Service.Interface;
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
        public async Task<ActionResult<string>> Get(string planet, DateTime startDate, DateTime endDate, int regsPage, int page)
        {
            try
            {
                string asteroids = await _asteroidsService.Get(planet, startDate, endDate, regsPage, page, _httpClientFactory);
                

                if (asteroids != null)
                    return Ok(asteroids);
                else
                    return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
