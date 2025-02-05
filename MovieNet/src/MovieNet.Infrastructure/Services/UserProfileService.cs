using Microsoft.EntityFrameworkCore;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces.Services;
using MovieNet.Domain.Entities;

namespace MovieNet.Infrastructure.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly MovieNetContext _context;
        public UserProfileService(MovieNetContext context)
        {
            _context = context;
        }

        public async Task<UserProfileDto> GetUserProfile(Guid userGuid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Guid == userGuid);
            if (user == null) throw new Exception("User not found.");

            var totalWatched = await _context.MovieListEntries
                .CountAsync(e => e.User.Guid == userGuid && e.Status == WatchlistType.Watching);

            var avgScore = await _context.Ratings
                .Where(r => r.User.Guid == userGuid)
                .Select(r => r.Score)
                .DefaultIfEmpty(0)
                .AverageAsync();

            return new UserProfileDto
            {
                UserGuid = user.Guid,
                UserName = user.UserName,
                Email = user.Email,
                Birthday = user.Birthday,
                TotalMoviesWatched = totalWatched,
                AverageScoreGiven = (int)avgScore
            };
        }

        public async Task UpdateUserProfile(EditUserProfileDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Guid == dto.UserGuid);
            if (user == null) throw new Exception("User not found.");

            if (!string.IsNullOrEmpty(dto.NewUserName)) user.UserName = dto.NewUserName;
            if (dto.NewBirthday.HasValue) user.Birthday = dto.NewBirthday.Value;

            await _context.SaveChangesAsync();
        }
    }
}
