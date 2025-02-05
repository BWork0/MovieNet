using MovieNet.Application.Commands;
using MovieNet.Application.DTOs;

namespace MovieNet.Application.Interfaces.Services
{
    public interface IWatchlistService
    {
        Task<List<WatchlistEntryDto>> GetUserWatchlist(Guid userGuid);
        Task CreateEntry(CreateWatchlistEntryCommand command);
        Task UpdateEntry(UpdateWatchlistEntryCommand command);
        Task RemoveEntry(RemoveWatchlistEntryCommand command);
    }
}
