using Microsoft.EntityFrameworkCore;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces.Repositories;
using MovieNet.Domain.Entities;

namespace MovieNet.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieNetContext _dbContext;

        public MovieRepository(MovieNetContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie> GetByGuidAsync(Guid guid)
        {
            return await _dbContext.Movies.FirstOrDefaultAsync(m => m.Guid == guid);
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task AddAsync(Movie movie)
        {
            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _dbContext.Movies.Update(movie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var movie = await GetByGuidAsync(id);
            if (movie is not null)
            {
                _dbContext.Movies.Remove(movie);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
