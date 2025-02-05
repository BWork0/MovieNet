using Microsoft.EntityFrameworkCore;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces.Services;
using MovieNet.Application.Queries;

namespace MovieNet.Infrastructure.Services
{
    public class MovieSearchService : IMovieSearchService
    {
        private readonly MovieNetContext _context;
        public MovieSearchService(MovieNetContext context)
        {
            _context = context;
        }

        public async Task<List<MovieDto>> Search(SearchMoviesQuery query)
        {
            var movies = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(query.TitleContains))
            {
                movies = movies.Where(m => m.Title.Contains(query.TitleContains));
            }

            if (query.MinYear.HasValue)
            {
                movies = movies.Where(m => m.ReleaseYear >= query.MinYear.Value);
            }
            if (query.MaxYear.HasValue)
            {
                movies = movies.Where(m => m.ReleaseYear <= query.MaxYear.Value);
            }

            if (!string.IsNullOrEmpty(query.GenreName))
            {
                movies = movies.Where(m => m.Genres.Any(g => g.Name == query.GenreName));
            }

            var result = await movies
                .Select(m => new MovieDto
                {
                    Guid = m.Guid,
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseYear = m.ReleaseYear,
                    Runtime = m.Runtime
                })
                .ToListAsync();

            return result;
        }
    }
}
