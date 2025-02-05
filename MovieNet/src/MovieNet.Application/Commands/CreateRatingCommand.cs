namespace MovieNet.Application.Commands
{
    public class CreateRatingCommand
    {
        public Guid MovieGuid { get; set; }
        public int Score { get; set; }
    }
}
