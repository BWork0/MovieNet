using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Services;

namespace MovieNet.Application.Commands
{
    public class CreateMovieCommandHandler : ICommandHandler<CreateMovieCommand>
    {
        private readonly IMovieService _movieService;


        public CreateMovieCommandHandler(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task Handle(CreateMovieCommand command)
        {
            await _movieService.CreateMovie(command);
        }
    }
}
