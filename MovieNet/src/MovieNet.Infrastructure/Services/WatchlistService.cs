using MovieNet.Application.Commands;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces.Repositories;
using MovieNet.Application.Interfaces.Services;
using MovieNet.Domain.Entities;

namespace MovieNet.Infrastructure.Services
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IWatchlistRepository _watchlistRepo;
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;

        public WatchlistService(
            IWatchlistRepository watchlistRepo,
            IUserRepository userRepository,
            IMovieRepository movieRepository)
        {
            _watchlistRepo = watchlistRepo;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
        }

        public async Task<List<WatchlistEntryDto>> GetUserWatchlist(Guid userGuid)
        {
            var entries = await _watchlistRepo.GetAllByUserAsync(userGuid);

            return entries.Select(e => new WatchlistEntryDto
            {
                EntryGuid = e.Guid,
                MovieGuid = e.Movie.Guid,
                MovieTitle = e.Movie.Title,
                Status = (int)e.Status,
                Score = e.Score,
                Notes = e.Notes,
                DateAdded = e.DateAdded,
                DateCompleted = e.DateCompleted
            })
            .ToList();
        }

        public async Task CreateEntry(CreateWatchlistEntryCommand command)
        {
            var user = await _userRepository.GetByGuidAsync(command.UserGuid);
            var movie = await _movieRepository.GetByGuidAsync(command.MovieGuid);

            if (user is null || movie is null)
                throw new Exception("User or movie not found.");

            var entry = new MovieListEntry(
                user,
                movie,
                (WatchlistType)command.Status,
                command.Score,
                command.Notes,
                DateTime.UtcNow
            );

            await _watchlistRepo.AddAsync(entry);
        }

        public async Task UpdateEntry(UpdateWatchlistEntryCommand command)
        {
            var entry = await _watchlistRepo.GetEntryByGuidAsync(command.EntryGuid);
            if (entry is null)
                throw new Exception("Watchlist entry not found.");

            if (entry.User.Guid != command.UserGuid)
                throw new Exception("Not authorized to update this entry.");

            entry.Score = command.Score;
            entry.Notes = command.Notes;
            entry.Status = (WatchlistType)command.Status;

            await _watchlistRepo.UpdateAsync(entry);
        }

        public async Task RemoveEntry(RemoveWatchlistEntryCommand command)
        {
            var entry = await _watchlistRepo.GetEntryByGuidAsync(command.EntryGuid);
            if (entry is null)
                return;

            if (entry.User.Guid != command.UserGuid)
                throw new Exception("Not authorized to remove this entry.");

            await _watchlistRepo.DeleteAsync(entry);
        }
    }

}
