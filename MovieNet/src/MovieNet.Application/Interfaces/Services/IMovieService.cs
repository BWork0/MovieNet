using MovieNet.Application.Commands;
using MovieNet.Application.DTOs;

namespace MovieNet.Application.Interfaces.Services
{
    public interface IMovieService
    {
        Task<List<MovieDto>> GetAllMovies();
        Task<MovieDto> GetMovie(Guid guid);
        Task CreateMovie(CreateMovieCommand command);
        Task DeleteMovie(DeleteMovieCommand command);
        Task UpdateMovie(UpdateMovieCommand command);
    }
}
