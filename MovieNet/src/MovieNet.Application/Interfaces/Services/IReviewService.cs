using MovieNet.Application.Commands;
using MovieNet.Application.DTOs;

namespace MovieNet.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task CreateReview(CreateReviewCommand command);
        Task<List<ReviewDto>> GetReviews(Guid movieGuid);
    }
}
