using System.Threading.Tasks;
using System.Net.Http;
using MovieApi.Shared;
using MovieApi.Models;
using Newtonsoft.Json;
using MovieApi.Services.Interfaces;
using System.Net.Http.Headers;

namespace MovieApi.Services
{
    public class GetMovieDetails
    {
        private readonly ExternalApiSettings _externalApiSettings;
        private IBuildURLServices _buildURLService;

        public GetMovieDetails(ExternalApiSettings externalApiSettings)
        {
            _externalApiSettings = externalApiSettings;
            _buildURLService = new BuildURLServices();
        }

        /// <summary>
        /// Gets the movie details async.
        /// </summary>
        /// <returns>The movie details</returns>
        /// <param name="provider">Provider.</param>
        /// <param name="id">Identifier.</param>
        public async Task<MovieDetails> GetMovieDetailsAsync(string provider, string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(_externalApiSettings.TokenName, _externalApiSettings.TokenValue);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = _buildURLService.GetURL(id, provider, _externalApiSettings);

                HttpResponseMessage response = await client.GetAsync(url);

                string stringResult = await response.Content.ReadAsStringAsync();
                MovieDetails json = JsonConvert.DeserializeObject<MovieDetails>(stringResult);
                return json;
            }
        }
    }
}
