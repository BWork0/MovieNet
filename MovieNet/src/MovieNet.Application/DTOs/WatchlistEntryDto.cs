namespace MovieNet.Application.DTOs
{
    public class WatchlistEntryDto
    {
        public Guid EntryGuid { get; set; }
        public Guid MovieGuid { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public int Status { get; set; }
        public int Score { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
