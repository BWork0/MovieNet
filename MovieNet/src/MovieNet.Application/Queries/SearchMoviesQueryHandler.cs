using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Services;

namespace MovieNet.Application.Queries
{
    public class SearchMoviesQueryHandler
    : IQueryHandler<SearchMoviesQuery, List<MovieDto>>
    {
        private readonly IMovieSearchService _searchService;

        public SearchMoviesQueryHandler(IMovieSearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task<List<MovieDto>> Handle(SearchMoviesQuery query)
        {
            return await _searchService.Search(query);
        }
    }
}
