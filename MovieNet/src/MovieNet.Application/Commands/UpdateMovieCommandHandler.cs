using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Services;

namespace MovieNet.Application.Commands
{
    public class UpdateMovieCommandHandler : ICommandHandler<UpdateMovieCommand>
    {
        private readonly IMovieService _movieService;

        public UpdateMovieCommandHandler(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public async Task Handle(UpdateMovieCommand command)
        {
            await _movieService.UpdateMovie(command);
        }
    }
}
