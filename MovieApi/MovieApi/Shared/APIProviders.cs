using System;
using System.Collections.Generic;

namespace MovieApi.Shared
{
    public static class APIProviders
    {
        private const string Cinemaworld = "/cinemaworld";
        private const string Filmworld = "/filmworld";

        public static List<string> Providers()
        {
            List<string> list = new List<string>();
            list.Add(Cinemaworld);
            list.Add(Filmworld);

            return list;
        }
    }
}
