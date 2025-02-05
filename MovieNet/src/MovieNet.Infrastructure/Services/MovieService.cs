using MovieNet.Application.Commands;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces.Repositories;
using MovieNet.Application.Interfaces.Services;
using MovieNet.Domain.Entities;

namespace MovieNet.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieDto>> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllAsync();
            return movies.Select(m => new MovieDto
            {
                Guid = m.Guid,
                Title = m.Title,
                Description = m.Description,
                ReleaseYear = m.ReleaseYear,
                Runtime = m.Runtime
            }).ToList();
        }

        public async Task CreateMovie(CreateMovieCommand command)
        {
            var movie = new Movie(command.Title, command.Description, command.ReleaseYear, command.Runtime);
            await _movieRepository.AddAsync(movie);
        }

        public async Task DeleteMovie(DeleteMovieCommand command)
        {
            var movie = _movieRepository.GetByGuidAsync(command.Guid);
            if (movie is not null)
            {
                await _movieRepository.DeleteAsync(command.Guid);
            }
        }

        public async Task UpdateMovie(UpdateMovieCommand command)
        {
            var movie = await _movieRepository.GetByGuidAsync(command.Guid);
            movie.Title = command.Title;
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task<MovieDto> GetMovie(Guid guid)
        {
            var movie = await _movieRepository.GetByGuidAsync(guid);
            if (movie is null) return null!;

            var movieDto = new MovieDto
            {
                Guid = movie.Guid,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseYear = movie.ReleaseYear,
                Runtime = movie.Runtime
            };
            return movieDto;
        }
    }
}
