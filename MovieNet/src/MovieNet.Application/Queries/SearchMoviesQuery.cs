namespace MovieNet.Application.Queries
{
    public class SearchMoviesQuery
    {
        public string? TitleContains { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public string? GenreName { get; set; }
    }
}
