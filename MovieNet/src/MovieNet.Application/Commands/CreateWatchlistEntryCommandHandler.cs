using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Services;

namespace MovieNet.Application.Commands
{
    public class CreateWatchlistEntryCommandHandler
        : ICommandHandler<CreateWatchlistEntryCommand>
    {
        private readonly IWatchlistService _watchlistService;

        public CreateWatchlistEntryCommandHandler(IWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        public async Task Handle(CreateWatchlistEntryCommand command)
        {
            await _watchlistService.CreateEntry(command);
        }
    }

    public class UpdateWatchlistEntryCommandHandler
        : ICommandHandler<UpdateWatchlistEntryCommand>
    {
        private readonly IWatchlistService _watchlistService;

        public UpdateWatchlistEntryCommandHandler(IWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        public async Task Handle(UpdateWatchlistEntryCommand command)
        {
            await _watchlistService.UpdateEntry(command);
        }
    }

    public class RemoveWatchlistEntryCommandHandler
        : ICommandHandler<RemoveWatchlistEntryCommand>
    {
        private readonly IWatchlistService _watchlistService;

        public RemoveWatchlistEntryCommandHandler(IWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        public async Task Handle(RemoveWatchlistEntryCommand command)
        {
            await _watchlistService.RemoveEntry(command);
        }
    }

}
