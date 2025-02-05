namespace MovieNet.Application.Commands
{
    public class UpdateWatchlistEntryCommand
    {
        public Guid EntryGuid { get; set; }
        public Guid UserGuid { get; set; }
        public int Score { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
