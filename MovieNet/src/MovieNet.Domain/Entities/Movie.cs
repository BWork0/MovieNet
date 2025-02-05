using System.Diagnostics.CodeAnalysis;

namespace MovieNet.Domain.Entities
{
    public class Movie
    {
        public int Id { get; private set; }
        public Guid Guid { get; init; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int ReleaseYear { get; set; }
        public int Runtime { get; set; }

        public ICollection<Actor> Actors { get; set; } = [];
        public ICollection<Rating> Ratings { get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<Genre> Genres { get; set; } = [];
        public ICollection<MovieActor> MovieActor { get; set; } = [];
        public ICollection<MovieListEntry> MovieListEntries { get; set; } = [];

        protected Movie()
        { }

        [SetsRequiredMembers]
        public Movie(string title, string description, int releaseYear, int runtime)
        {
            Guid = Guid.NewGuid();
            Title = title;
            Description = description;
            ReleaseYear = releaseYear;
            Runtime = runtime;
        }
    }
}
