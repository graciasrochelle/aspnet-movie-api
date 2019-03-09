using System;
using Xunit;
using MovieApi.Shared;
using System.Collections.Generic;
using MovieApi.Services;
using MovieApi.Services.Interfaces;

namespace MovieApiTests
{
    public class Testing_BuildURLServices
    {
        private readonly ExternalApiSettings _externalApiSettings;

        public Testing_BuildURLServices()
        {
            _externalApiSettings = new ExternalApiSettings
            {
                BaseUrl = "https://webjetapitest.azurewebsites.net/api",
                TokenValue = "x-access-token",
                TokenName = "sjd1HfkjU83ksdsm3802k"
            };
        }

        [Fact]
        public void TestingHostname()
        {
            string actual = "https://webjetapitest.azurewebsites.net/api";
            string expected = _externalApiSettings.BaseUrl;
           
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestingEndpointToGetAllMovies()
        {
            string actual = "/movies";
            string expected = Constants.MoviesEndpoint;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestingEndpointToGetMovieDetails()
        {
            string actual = "/movie";
            string expected = Constants.MovieEndpoint;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestingAPIProviders()
        {
            string[] actual = { "/cinemaworld", 
                                "/filmworld" };

            List<string> expected = APIProviders.Providers();

            for (int i = 0; i < actual.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        [Fact]
        public void TestingBuildURLService1()
        {
            string[] actualURLS = {
            "https://webjetapitest.azurewebsites.net/api/cinemaworld/movies",
            "https://webjetapitest.azurewebsites.net/api/filmworld/movies"
            };

            IBuildURLServices buildURLService = new BuildURLServices();

            List<string> urlList = buildURLService.GetURL(_externalApiSettings);

            for (int i=0;i< actualURLS.Length;i++)
            {
                Assert.Equal(urlList[i], actualURLS[i]);
            }
        }

        [Fact]
        public void TestingBuildURLService2()
        {
            string actualURL = "https://webjetapitest.azurewebsites.net/api/cinemaworld/movie/1";

            IBuildURLServices buildURLService = new BuildURLServices();

            string id = "1";
            string provider = "cinemaworld";

            string url = buildURLService.GetURL(id, provider, _externalApiSettings);

            Assert.Equal(url, actualURL);
        }
    }
}
