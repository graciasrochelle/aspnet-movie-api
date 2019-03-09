namespace MovieApi.Models
{
    public class Movie
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }

        public string Provider { get; set; }
    }
}
