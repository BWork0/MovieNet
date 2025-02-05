using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieNet.Application.Commands;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces;
using MovieNet.Application.Queries;
using System.Security.Claims;

namespace MovieNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
        {
            var userGuidString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userGuidString == null) return Unauthorized();

            await _mediator.Send(command);
            return Ok("Review added!");
        }

        [HttpGet("{movieGuid:guid}")]
        public async Task<IActionResult> GetMovieReviews(Guid movieGuid)
        {
            var query = new GetReviewsQuery { MovieGuid = movieGuid };
            var reviews = await _mediator.Query<GetReviewsQuery, List<ReviewDto>>(query);
            return Ok(reviews);
        }
    }
}
