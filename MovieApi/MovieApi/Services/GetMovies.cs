using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        /// <summary>
        /// Gets the list of movie async.
        /// </summary>
        /// <returns>The list of movie async.</returns>
        public async Task<IEnumerable<Movie>> GetListOfMovieAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(_externalApiSettings.TokenName, _externalApiSettings.TokenValue);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                List<string> urlList = _buildURLService.GetURL(_externalApiSettings);

                IEnumerable<Task<IEnumerable<Movie>>> downloadTasksQuery =
                from url in urlList select ProcessURLAsync(url, client);

                Task<IEnumerable<Movie>>[] downloadTasks = downloadTasksQuery.ToArray();

                Task<IEnumerable<Movie>> firstFinishedTask = await Task.WhenAny(downloadTasks);

                IEnumerable<Movie> data = await firstFinishedTask;

                return data;
            }
        }

        async Task<IEnumerable<Movie>> ProcessURLAsync(string url, HttpClient client)
        {
            HttpResponseMessage response =  client.GetAsync(url).Result;
            string stringResult = await response.Content.ReadAsStringAsync();
            RootObject json = JsonConvert.DeserializeObject<RootObject>(stringResult);

            foreach (var movie in json.Movies)
            {
                string provider = GetProviderFromURL.GetProvider(url);
                movie.Provider = provider;
            }

            return json.Movies;
        }
    }
}
