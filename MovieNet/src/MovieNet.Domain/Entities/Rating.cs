using System.Diagnostics.CodeAnalysis;

namespace MovieNet.Domain.Entities
{
    public class Rating
    {
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public required User User { get; set; }
        public required Movie Movie { get; set; }
        public int Score { get; set; }
        public DateOnly CreatedAt { get; set; }

        protected Rating()
        { }

        [SetsRequiredMembers]
        public Rating(int score, DateOnly createdAt, User user, Movie movie)
        {
            Guid = Guid.NewGuid();
            Score = score;
            CreatedAt = createdAt;
            User = user;
            Movie = movie;
        }
    }
}
