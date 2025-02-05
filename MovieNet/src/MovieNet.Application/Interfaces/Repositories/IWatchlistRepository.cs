using MovieNet.Domain.Entities;

namespace MovieNet.Application.Interfaces.Repositories
{
    public interface IWatchlistRepository
    {
        Task<List<MovieListEntry>> GetAllByUserAsync(Guid userGuid);
        Task<MovieListEntry?> GetEntryByGuidAsync(Guid entryGuid);
        Task AddAsync(MovieListEntry entry);
        Task UpdateAsync(MovieListEntry entry);
        Task DeleteAsync(MovieListEntry entry);
    }
}
