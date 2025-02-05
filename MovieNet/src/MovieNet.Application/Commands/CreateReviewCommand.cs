namespace MovieNet.Application.Commands
{
    public class CreateReviewCommand
    {
        public Guid MovieGuid { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
