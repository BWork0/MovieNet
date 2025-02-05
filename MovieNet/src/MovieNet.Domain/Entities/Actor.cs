namespace MovieNet.Domain.Entities
{
    public class Actor
    {
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; } = [];
        protected Actor()
        { }

        public Actor(string firstName, string lastName)
        {
            Guid = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
