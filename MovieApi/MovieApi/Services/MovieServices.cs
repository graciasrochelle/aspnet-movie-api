using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MovieApi.Models;
using MovieApi.Shared;

namespace MovieApi.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly ExternalApiSettings _externalApiSettings;

        public MovieServices(ExternalApiSettings externalApiSettings)
        {
            _externalApiSettings = externalApiSettings;
        }

        public async Task<IEnumerable<Movie>> Get()
        {
            GetMovies getListOfMovies = new GetMovies(_externalApiSettings);
            var response = await getListOfMovies.GetListOfMovieAsync();
            return response;
        }

        public async Task<MovieDetails> Get(string provider, string id)
        {
            GetMovieDetails getMovieDetailsAsync = new GetMovieDetails(_externalApiSettings);
            var response = await getMovieDetailsAsync.GetMovieDetailsAsync(provider, id);
            return response;
        }
    }
}
