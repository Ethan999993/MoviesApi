namespace MoviesApi.DTOs
{
    public class Movie
    {


        public int id { get; set; }


        public string Title { get; set; } = string.Empty;

        public string Synopsis { get; set; } = string.Empty;

        public int RuntimeMinutes { get; set; } // gets the movies runtime in minutes 

        public string ImageUrl { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; } // gets movie relase date 

      

        public Genre Genre { get; set; } = null!;

        public AudienceRating AudienceRating { get; set; } = null!;








    }
}
