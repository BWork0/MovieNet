namespace MovieNet.Application.DTOs
{
    public class UserProfileDto
    {
        public Guid UserGuid { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
        public int TotalMoviesWatched { get; set; }
        public int AverageScoreGiven { get; set; }
    }
}
