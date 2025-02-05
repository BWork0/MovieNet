using Microsoft.EntityFrameworkCore;
using MovieNet.Application.Commands;
using MovieNet.Application.Interfaces.Services;
using MovieNet.Domain.Entities;

namespace MovieNet.Infrastructure.Services
{
    public class RatingService : IRatingService
    {
        private readonly MovieNetContext _context;
        public RatingService(MovieNetContext context) => _context = context;

        public async Task CreateOrUpdateRating(CreateRatingCommand command, Guid userGuid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Guid == userGuid);
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Guid == command.MovieGuid);
            if (user == null || movie == null) throw new Exception("User or movie not found.");

            var existing = await _context.Ratings
                .FirstOrDefaultAsync(r => r.User.Guid == userGuid && r.Movie.Guid == command.MovieGuid);

            if (existing != null)
            {
                existing.Score = command.Score;
            }
            else
            {
                var rating = new Rating(command.Score, DateOnly.FromDateTime(DateTime.UtcNow), user, movie);
                _context.Ratings.Add(rating);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<double> GetAverageRating(Guid movieGuid)
        {
            var ratings = await _context.Ratings
                .Where(r => r.Movie.Guid == movieGuid)
                .Select(r => r.Score)
                .ToListAsync();

            if (ratings.Count == 0) return 0;
            return ratings.Average();
        }
    }
}
