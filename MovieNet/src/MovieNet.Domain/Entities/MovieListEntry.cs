using System.Diagnostics.CodeAnalysis;

namespace MovieNet.Domain.Entities
{
    public enum WatchlistType
    {
        Watching = 1, Dropped = 2, Plan2Watch = 3
    }

    public class MovieListEntry
    {
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public required User User { get; set; }
        public required Movie Movie { get; set; }
        public WatchlistType Status { get; set; }
        public int Score { get; set; }
        public required string Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateCompleted { get; set; }

        protected MovieListEntry()
        { }

        [SetsRequiredMembers]
        public MovieListEntry(User user, Movie movie, WatchlistType status, int score, string notes, DateTime dateAdded, DateTime? dateCompleted = null)
        {
            Guid = Guid.NewGuid();
            User = user;
            Movie = movie;
            Status = status;
            Score = score;
            Notes = notes;
            DateAdded = dateAdded;
            DateCompleted = dateCompleted;
        }
    }
}
