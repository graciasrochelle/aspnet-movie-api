using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMovieServices _movieServices;
        private readonly ILogger _logger;

        public MovieController(ExternalApiSettings externalApiSettings, ILogger<TestController> logger)
        {
            _movieServices = new MovieServices(externalApiSettings);
            _externalApiSettings = externalApiSettings;
            _logger = logger;
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
                var response = await _movieServices.Get();
                _logger.LogInformation("MovieController: GetMoviesAsync", " response received..");
                return Ok(response);
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
                var response = await _movieServices.Get(provider, id);
                _logger.LogInformation("MovieController: GetMovie", " response received for ID::" + id + " and Provider::" + provider);
                return Ok(response);
            }
        }
    }
}
