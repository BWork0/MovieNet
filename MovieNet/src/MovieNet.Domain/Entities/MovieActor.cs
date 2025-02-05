namespace MovieNet.Domain.Entities
{
    public class MovieActor
    {
        public int Id { get; private set; }
        public required Movie Movie { get; set; }
        public required Actor Actor { get; set; }
        public required string Character { get; set; }
        public MovieActor(Movie movie, Actor actor, string character)
        {
            Movie = movie;
            Actor = actor;
            Character = character;
        }

        protected MovieActor()
        { }
    }
}
