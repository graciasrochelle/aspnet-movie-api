using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MovieApi.Models;
using MovieApi.Services.Interfaces;
using MovieApi.Shared;
using Newtonsoft.Json;

namespace MovieApi.Services
{
    public class GetMovies
    {
        private readonly ExternalApiSettings _externalApiSettings;
        private readonly IBuildURLServices _buildURLService;

        public GetMovies(ExternalApiSettings externalApiSettings)
        {
            _externalApiSettings = externalApiSettings;
            _buildURLService = new BuildURLServices();
        }

        public async Task<IEnumerable<Movie>> GetListOfMovieAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(_externalApiSettings.TokenName, _externalApiSettings.TokenValue);
                List<string> urlList = _buildURLService.GetURL(_externalApiSettings);

                IEnumerable<Task<IEnumerable<Movie>>> downloadTasksQuery =
                from url in urlList select ProcessURLAsync(url, client);

                Task<IEnumerable<Movie>>[] downloadTasks = downloadTasksQuery.ToArray();

                Task<IEnumerable<Movie>> firstFinishedTask = await Task.WhenAny(downloadTasks);

                var data = await firstFinishedTask;

                return data;
            }
        }

        async Task<IEnumerable<Movie>> ProcessURLAsync(string url, HttpClient client)
        {
            var response = await client.GetAsync(url);
            var stringResult = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<RootObject>(stringResult);

            foreach (var movie in json.Movies)
            {
                string provider = GetProviderFromURL.GetProvider(url);
                movie.Provider = provider;
            }

            return json.Movies;
        }
    }
}
