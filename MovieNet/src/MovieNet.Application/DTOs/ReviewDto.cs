namespace MovieNet.Application.DTOs
{
    public class ReviewDto
    {
        public Guid Guid { get; set; }
        public Guid MovieGuid { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
    }

}
