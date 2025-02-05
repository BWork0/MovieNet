namespace MovieNet.Application.Commands
{
    public class RemoveWatchlistEntryCommand
    {
        public Guid UserGuid { get; set; }
        public Guid EntryGuid { get; set; }
    }
}
