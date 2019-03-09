using System.Threading.Tasks;
using System.Net.Http;
using MovieApi.Shared;
using MovieApi.Models;
using Newtonsoft.Json;
using MovieApi.Services.Interfaces;

namespace MovieApi.Services
{
    public class GetMovieDetails
    {
        private readonly ExternalApiSettings _externalApiSettings;
        private readonly IBuildURLServices _buildURLService;

        public GetMovieDetails(ExternalApiSettings externalApiSettings)
        {
            _externalApiSettings = externalApiSettings;
            _buildURLService = new BuildURLServices();
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(string provider, string id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(_externalApiSettings.TokenName, _externalApiSettings.TokenValue);
                string url = _buildURLService.GetURL(id,provider, _externalApiSettings);

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<MovieDetails>(stringResult);
                return json;
            }
        }
    }
}
