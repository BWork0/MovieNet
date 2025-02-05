using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces;
using MovieNet.Application.Queries;
using System.Security.Claims;

namespace MovieNet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyProfile()
        {
            var userGuidString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userGuidString == null) return Unauthorized();

            var query = new GetUserProfileQuery { UserGuid = Guid.Parse(userGuidString) };
            var profile = await _mediator.Query<GetUserProfileQuery, UserProfileDto>(query);
            return Ok(profile);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditUserProfileDto dto)
        {
            var userGuidString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userGuidString == null) return Unauthorized();

            if (Guid.Parse(userGuidString) != dto.UserGuid)
                return Forbid("You can only edit your own profile!");

            return Ok("Profile updated!");
        }
    }

}
