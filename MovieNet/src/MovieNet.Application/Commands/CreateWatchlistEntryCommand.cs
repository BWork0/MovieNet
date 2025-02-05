using System.Text.Json.Serialization;

namespace MovieNet.Application.Commands
{

    public class CreateWatchlistEntryCommand
    {
        [JsonIgnore]
        public Guid UserGuid { get; set; }
        public Guid MovieGuid { get; set; }
        public int Score { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
