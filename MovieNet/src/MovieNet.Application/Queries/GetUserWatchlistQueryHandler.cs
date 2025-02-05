using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Services;

namespace MovieNet.Application.Queries
{
    public class GetUserWatchlistQueryHandler
        : IQueryHandler<GetUserWatchlistQuery, List<WatchlistEntryDto>>
    {
        private readonly IWatchlistService _watchlistService;

        public GetUserWatchlistQueryHandler(IWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        public async Task<List<WatchlistEntryDto>> Handle(GetUserWatchlistQuery query)
        {
            return await _watchlistService.GetUserWatchlist(query.UserGuid);
        }
    }
}
