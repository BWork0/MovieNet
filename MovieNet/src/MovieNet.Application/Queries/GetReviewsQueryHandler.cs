using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Services;

namespace MovieNet.Application.Queries
{
    public class GetReviewsQueryHandler : IQueryHandler<GetReviewsQuery, List<ReviewDto>>
    {
        private readonly IReviewService _reviewService;
        public GetReviewsQueryHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public async Task<List<ReviewDto>> Handle(GetReviewsQuery query)
        {
            return await _reviewService.GetReviews(query.MovieGuid);
        }
    }
}
