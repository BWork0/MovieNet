using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieNet.Application.Commands;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Services;
using MovieNet.Application.Queries;

namespace MovieNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMediator _mediator;

        public MovieController(IMovieService movieService, IMediator mediator)
        {
            _movieService = movieService;
            _mediator = mediator;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _mediator.Query<GetMoviesQuery, List<MovieDto>>(new GetMoviesQuery());
            if (movies.Count == 0) return NoContent();
            return Ok(movies);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMovie(CreateMovieCommand command)
        {
            await _mediator.Send(command);
            return CreatedAtAction(nameof(GetMovies), null);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{guid:guid}")]
        public async Task<IActionResult> DeleteMovie(Guid guid)
        {
            var command = new DeleteMovieCommand { Guid = guid };
            await _mediator.Send(command);
            return NoContent();
        }


        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{guid:guid}")]
        public async Task<IActionResult> UpdateMovie(Guid guid, UpdateMovieCommand command)
        {
            if (guid != command.Guid)
            {
                return BadRequest("Movie Guid mismatch.");
            }

            var movie = _movieService.GetMovie(guid);
            if (movie is null)
            {
                await _mediator.Send(new CreateMovieCommand
                {
                    Title = command.Title,
                    Runtime = command.Runtime,
                    ReleaseYear = command.ReleaseYear,
                    Description = command.Description
                });
            }
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
