namespace MovieNet.Domain.Entities
{
    public class Genre
    {
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public required string Name { get; set; }
        public ICollection<Movie> Movies { get; set; } = [];

        protected Genre()
        { }

        public Genre(string name)
        {
            Name = name;
        }
    }
}
