using MovieNet.Domain.Entities;

namespace MovieNet.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User?> GetByGuidAsync(Guid userGuid);
    }
}
