using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MovieApi.Models;
using MovieApi.Services;
using MovieApi.Shared;

namespace MovieApi.Controllers
{ 
    [Route("api/movies")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly ExternalApiSettings _externalApiSettings;
        private IMovieServices _movieServices;
        private ILogger _logger;
        private IMemoryCache _cache;

        public MovieController(ExternalApiSettings externalApiSettings, ILogger<MovieController> logger, IMemoryCache memoryCache)
        {
            _movieServices = new MovieServices(externalApiSettings, memoryCache);
            _externalApiSettings = externalApiSettings;
            _logger = logger;
            _cache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<Task<IEnumerable<Movie>>>> GetMoviesAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                IEnumerable<Movie> response = await _movieServices.Get();
                _logger.LogInformation("MovieController: GetMoviesAsync", " response received..");
                return Ok(Json(response));
            }
        }

        [HttpGet("{provider}/{id}")]
        public async Task<ActionResult<MovieDetails>> GetMovie(String provider, String id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                MovieDetails response = await _movieServices.Get(provider, id);
                _logger.LogInformation("MovieController: GetMovie", " response received for ID::" + id + " and Provider::" + provider);
                if (response == null)
                {
                    return NoContent();
                }
                return Ok(Json(response));
            }
        }
    }
}
