using Microsoft.EntityFrameworkCore;
using MovieNet.Application.Interfaces.Repositories;
using MovieNet.Domain.Entities;

namespace MovieNet.Infrastructure.Repositories
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly MovieNetContext _dbContext;
        public WatchlistRepository(MovieNetContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MovieListEntry>> GetAllByUserAsync(Guid userGuid)
        {
            return await _dbContext.MovieListEntries
                .Include(e => e.Movie)
                .Where(e => e.User.Guid == userGuid)
                .ToListAsync();
        }

        public async Task<MovieListEntry?> GetEntryByGuidAsync(Guid entryGuid)
        {
            return await _dbContext.MovieListEntries
                .Include(e => e.Movie)
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Guid == entryGuid);
        }

        public async Task AddAsync(MovieListEntry entry)
        {
            await _dbContext.MovieListEntries.AddAsync(entry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(MovieListEntry entry)
        {
            _dbContext.MovieListEntries.Update(entry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(MovieListEntry entry)
        {
            _dbContext.MovieListEntries.Remove(entry);
            await _dbContext.SaveChangesAsync();
        }
    }
}
