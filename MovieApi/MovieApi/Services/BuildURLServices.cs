using System.Collections.Generic;
using MovieApi.Services.Interfaces;
using MovieApi.Shared;

namespace MovieApi.Services
{
    public class BuildURLServices : IBuildURLServices
    {
        public string GetURL(string id, string provider, ExternalApiSettings externalApiSettings)
        {
            string hostname = externalApiSettings.BaseUrl;
            provider = "/"+ provider;
            id = "/" + id;
            string endpoint = Constants.MovieEndpoint;

            return GetBaseURL(hostname, provider, endpoint) + id;
        }

        public List<string> GetURL(ExternalApiSettings externalApiSettings)
        {
            List<string> urls = new List<string>();
            string hostname = externalApiSettings.BaseUrl;
            List<string> providers = APIProviders.Providers();
            string endpoint = Constants.MoviesEndpoint;

            foreach (string provider in providers)
            {
                urls.Add(GetBaseURL(hostname, provider, endpoint));
            }

            return urls;
        }

        string GetBaseURL(string hostname, string provider, string endpoint)
        {
            return hostname + provider + endpoint;
        }
    }
}
