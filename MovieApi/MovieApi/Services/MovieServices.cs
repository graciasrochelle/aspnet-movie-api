using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MovieApi.Models;
using MovieApi.Shared;

namespace MovieApi.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly ExternalApiSettings _externalApiSettings;
        private readonly string Key = "MovieList";
        private IMemoryCache _cache;

        public MovieServices(ExternalApiSettings externalApiSettings, IMemoryCache memoryCache)
        {
            _externalApiSettings = externalApiSettings;
            _cache = memoryCache;
        }

        /// <summary>
        /// This methods checks if the list of movies are present in cache,
        /// if its present then it returns the cached copy
        /// else it fetches it from the external API, stores in cache for 10secs
        /// and returns the response
        /// </summary>
        /// <returns>List of movies.</returns>
        public async Task<IEnumerable<Movie>> Get()
        {
            List<Movie> response = new List<Movie>();

            if (!_cache.TryGetValue(Key, out response))
            {
                if (response == null)
                {
                    GetMovies getListOfMovies = new GetMovies(_externalApiSettings);
                    response = (List<Movie>)await getListOfMovies.GetListOfMovieAsync();
                    MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

                    _cache.Set(Key, response, cacheEntryOptions);
                }
            }
            return response;
        }

        /// <summary>
        /// This methods checks if the movie details are present in cache,
        /// if its present then it returns the cached copy
        /// else it fetches it from the external API, stores in cache for 10secs
        /// and returns the response
        /// </summary>
        /// <returns>Movie details for an ID and provider</returns>
        /// <param name="provider">Provider.</param>
        /// <param name="id">Identifier.</param>
        public async Task<MovieDetails> Get(string provider, string id)
        {
            MovieDetails response = new MovieDetails();
            string cacheKey = id + "-" + provider;
            if (!_cache.TryGetValue(cacheKey, out response))
            {
                if (response == null)
                {
                    GetMovieDetails getMovieDetailsAsync = new GetMovieDetails(_externalApiSettings);
                    response = await getMovieDetailsAsync.GetMovieDetailsAsync(provider, id);

                    MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

                    _cache.Set(cacheKey, response, cacheEntryOptions);
                }
            }
            return response;
        }
    }
}
