using Microsoft.EntityFrameworkCore;
using MovieNet.Application.Interfaces.Repositories;
using MovieNet.Domain.Entities;

namespace MovieNet.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieNetContext _dbContext;

        public UserRepository(MovieNetContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetByGuidAsync(Guid userGuid)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Guid == userGuid);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users.Where(u => u.UserName.Equals(username)).FirstOrDefaultAsync();
        }
    }
}
