using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : Controller
    {

        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;

        public TestController(IMemoryCache cache, ILogger<TestController> logger)
        {
            _logger = logger;
            _cache = cache;
        }

        public JsonResult GetAllValues()
        {
            var movies = new List<MovieViewModel>();
            if(!_cache.TryGetValue("MoviesList", out movies))
            {
                if (movies == null)
                {
                    movies = AllMovies();
                }
                else
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
               .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

                    _cache.Set("MoviesList", movies, cacheEntryOptions);
                }
            }

            return Json(movies);
        }

        private List<MovieViewModel> AllMovies()
        {
            var movies = new List<MovieViewModel>
            {
                new MovieViewModel() { Id = "1", Provider = "test1", TimeStamp = DateTime.UtcNow},
                new MovieViewModel() { Id = "2", Provider = "test2", TimeStamp = DateTime.UtcNow }
            };
            return movies;
        }
    }
}
