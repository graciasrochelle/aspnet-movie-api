using System.Collections.Generic;
using MovieApi.Shared;

namespace MovieApi.Services.Interfaces
{
    public interface IBuildURLServices
    {
         string GetURL(string id, string provider, ExternalApiSettings externalApiSettings);
         List<string> GetURL(ExternalApiSettings externalApiSettings);
    }
}
