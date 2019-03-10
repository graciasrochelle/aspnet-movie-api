using System.Text.RegularExpressions;

namespace MovieApi.Services
{
    public static class GetProviderFromURL
    {
        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <returns>The provider.</returns>
        /// <param name="url">URL.</param>
        public static string GetProvider(string url)
        {
            string pattern = "/";
            string[] substrings = Regex.Split(url, pattern);
            return substrings[4];
        }
    }
}
