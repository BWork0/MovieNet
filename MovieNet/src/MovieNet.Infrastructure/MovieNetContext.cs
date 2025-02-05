using Bogus;
using Microsoft.EntityFrameworkCore;
using MovieNet.Domain.Entities;

namespace MovieNet.Infrastructure;

public class MovieNetContext : DbContext
{
    public MovieNetContext()
    { }

    public MovieNetContext(DbContextOptions Options) : base(Options) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<MovieListEntry> MovieListEntries => Set<MovieListEntry>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Actor> Actors => Set<Actor>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(@"Data Source=MovieNet.db");
        }
    }

    public void Seed()
    {
        var authors = new Faker<User>("de").CustomInstantiator(f =>
        {
            return new User(
                role: f.Random.Enum<Role>(),
                initialPassword: "1111",
                userName: f.Person.UserName,
                email: f.Person.Email,
                createdAt: new DateOnly(1, 1, 1),
                birthday: new DateOnly(2, 2, 2)

            );
        })
        .Generate(10)
        .GroupBy(a => a.Email).Select(g => g.First())
        .ToList();
        Users.AddRange(authors);
        SaveChanges();
    }
}