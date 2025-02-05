using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Services;

namespace MovieNet.Application.Queries
{
    public class GetMoviesQueryHandler : IQueryHandler<GetMoviesQuery, List<MovieDto>>
    {
        private readonly IMovieService _movieService;
        public GetMoviesQueryHandler(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<List<MovieDto>> Handle(GetMoviesQuery query)
        {
            return await _movieService.GetAllMovies();
        }
    }
}
