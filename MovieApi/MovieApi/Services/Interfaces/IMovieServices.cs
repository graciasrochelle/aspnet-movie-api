using System.Collections.Generic;
using System.Threading.Tasks;
using MovieApi.Models;

namespace MovieApi.Services
{
    public interface IMovieServices
    {
        Task<IEnumerable<Movie>> Get();
        Task<MovieDetails> Get(string provider, string id);
    }
}
