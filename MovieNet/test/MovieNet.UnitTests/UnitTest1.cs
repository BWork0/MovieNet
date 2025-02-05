using Microsoft.EntityFrameworkCore;
using MovieNet.Infrastructure;

namespace MovieNet.UnitTests;

public class UnitTest1
{
    private readonly MovieNetContext db = GenerateDatabase();

    private static MovieNetContext GenerateDatabase()
    {
        DbContextOptionsBuilder options = new();
        options.UseSqlite("Data Source=../../../MovieNet.db");
        MovieNetContext db = new(options.Options);
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        return db;
    }

    [Fact]
    public void EnsureCreatedSuccessTest()
    {
        db.Database.EnsureCreated();
    }

    [Fact]
    public void EnsureReviewAdded()
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        db.Seed();

        Assert.True(db.Reviews.Any());
    }
}