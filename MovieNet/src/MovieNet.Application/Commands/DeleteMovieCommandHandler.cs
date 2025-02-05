using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Services;

namespace MovieNet.Application.Commands
{
    public class DeleteMovieCommandHandler : ICommandHandler<DeleteMovieCommand>
    {
        private readonly IMovieService _movieService;

        public DeleteMovieCommandHandler(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public async Task Handle(DeleteMovieCommand command)
        {
            await _movieService.DeleteMovie(command);
        }
    }
}
