namespace MovieNet.Application.Commands
{
    public class CreateMovieCommand
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public int Runtime { get; set; }
    }
}
