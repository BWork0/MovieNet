using MovieNet.Domain.Entities;

namespace MovieNet.Application.Interfaces.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> GetByGuidAsync(Guid id);
        Task<List<Movie>> GetAllAsync();
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(Guid id);
    }
}
