using System.Collections.Generic;

namespace MovieApi.Models
{
    public class RootObject
    {
        public IEnumerable<Movie> Movies { get; set; }
    }
}
