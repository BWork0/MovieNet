using MovieNet.Application.Commands;

namespace MovieNet.Application.Interfaces.Services
{
    public interface IRatingService
    {
        Task CreateOrUpdateRating(CreateRatingCommand command, Guid userGuid);
        Task<double> GetAverageRating(Guid movieGuid);
    }
}
