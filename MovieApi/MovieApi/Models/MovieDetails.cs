using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models
{
    public class MovieDetails
    {
        [Required]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Genre { get; set; }
        public string Plot { get; set; }
        public string Type { get; set; }
        public string Price { get; set; }
    }
}
