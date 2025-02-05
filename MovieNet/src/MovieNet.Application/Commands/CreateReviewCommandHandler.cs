using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Services;

namespace MovieNet.Application.Commands
{
    public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand>
    {
        private readonly IReviewService _reviewService;

        public CreateReviewCommandHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task Handle(CreateReviewCommand command)
        {
            await _reviewService.CreateReview(command);
        }
    }
}
