using MovieNet.Application.DTOs;
using MovieNet.Application.Queries;

namespace MovieNet.Application.Interfaces.Services
{
    public interface IMovieSearchService
    {
        Task<List<MovieDto>> Search(SearchMoviesQuery query);
    }
}
