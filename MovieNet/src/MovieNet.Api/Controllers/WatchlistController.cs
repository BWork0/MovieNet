using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieNet.Application.Commands;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Services;
using MovieNet.Application.Queries;
using System.Security.Claims;

namespace MovieNet.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WatchlistController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWatchlistService _watchlistService;

        public WatchlistController(IMediator mediator, IWatchlistService watchlistService)
        {
            _mediator = mediator;
            _watchlistService = watchlistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyWatchlist()
        {
            var userGuidString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userGuidString is null)
                return Unauthorized();

            var userGuid = Guid.Parse(userGuidString);

            var query = new GetUserWatchlistQuery { UserGuid = userGuid };
            var entries = await _mediator.Query<GetUserWatchlistQuery, List<WatchlistEntryDto>>(query);
            return Ok(entries);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWatchlist([FromBody] CreateWatchlistEntryCommand command)
        {
            var userGuidString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userGuidString is null) return Unauthorized();

            command.UserGuid = Guid.Parse(userGuidString);

            await _mediator.Send(command);
            return Ok("Added to watchlist!");
        }


        [HttpPut("{entryGuid:guid}")]
        public async Task<IActionResult> UpdateEntry(Guid entryGuid, [FromBody] UpdateWatchlistEntryCommand command)
        {
            if (entryGuid != command.EntryGuid)
                return BadRequest("Entry Guid mismatch.");

            var userGuidString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userGuidString is null)
                return Unauthorized();

            command.UserGuid = Guid.Parse(userGuidString);

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{entryGuid:guid}")]
        public async Task<IActionResult> RemoveEntry(Guid entryGuid)
        {
            var userGuidString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userGuidString is null)
                return Unauthorized();

            var command = new RemoveWatchlistEntryCommand
            {
                UserGuid = Guid.Parse(userGuidString),
                EntryGuid = entryGuid
            };

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
