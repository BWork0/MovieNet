using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieNet.Application.Commands;
using MovieNet.Application.Interfaces.Services;
using System.Security.Claims;

namespace MovieNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RateMovie(CreateRatingCommand command)
        {
            var userGuidString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userGuidString)) return Unauthorized();

            await _ratingService.CreateOrUpdateRating(command, Guid.Parse(userGuidString));
            return Ok("Rating submitted/updated!");
        }

        [HttpGet("average/{movieGuid:guid}")]
        public async Task<IActionResult> GetAverageRating(Guid movieGuid)
        {
            var average = await _ratingService.GetAverageRating(movieGuid);
            return Ok(average);
        }
    }
}
