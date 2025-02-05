using Microsoft.EntityFrameworkCore;
using MovieNet.Application.Commands;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces.Services;
using MovieNet.Domain.Entities;

namespace MovieNet.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly MovieNetContext _context;
        public ReviewService(MovieNetContext context)
        {
            _context = context;
        }

        public async Task CreateReview(CreateReviewCommand command)
        {
            var userGuid = Guid.Empty;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Guid == userGuid);
            if (user == null) throw new Exception("User not found.");

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Guid == command.MovieGuid);
            if (movie == null) throw new Exception("Movie not found.");

            var review = new Review(command.Text, DateOnly.FromDateTime(DateTime.UtcNow), user, movie);
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReviewDto>> GetReviews(Guid movieGuid)
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Where(r => r.Movie.Guid == movieGuid)
                .ToListAsync();

            return reviews.Select(r => new ReviewDto
            {
                Guid = r.Guid,
                MovieGuid = r.Movie.Guid,
                UserName = r.User.UserName,
                Text = r.Text,
                CreatedAt = r.CreatedAt
            })
            .ToList();
        }
    }

}
