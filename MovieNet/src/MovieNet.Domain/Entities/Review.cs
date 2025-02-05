using System.Diagnostics.CodeAnalysis;

namespace MovieNet.Domain.Entities
{
    public class Review
    {
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public required string Text { get; set; }
        public DateOnly CreatedAt { get; private set; }
        public required User User { get; set; }
        public required Movie Movie { get; set; }

        protected Review()
        { }

        [SetsRequiredMembers]
        public Review(string text, DateOnly createdAt, User user, Movie movie)
        {
            Guid = Guid.NewGuid();
            Text = text;
            CreatedAt = createdAt;
            User = user;
            Movie = movie;
        }
    }
}
