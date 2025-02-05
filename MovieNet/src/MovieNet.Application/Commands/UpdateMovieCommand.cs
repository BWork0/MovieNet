namespace MovieNet.Application.Commands
{
    public class UpdateMovieCommand
    {
        public Guid Guid { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public int Runtime { get; set; }
    }
}
