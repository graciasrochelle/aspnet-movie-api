using System.Text.RegularExpressions;

namespace MovieApi.Services
{
    public static class GetProviderFromURL
    {
        public static string GetProvider(string url)
        {
            string pattern = "/";
            string[] substrings = Regex.Split(url, pattern);
            return substrings[4];
        }
    }
}
