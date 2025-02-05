using MovieNet.Application.DTOs;
using MovieNet.Domain.Entities;

namespace MovieNet.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> CheckUserExists(string email);
        Task RegisterAsync(RegisterDto dto);
        Task<User?> LoginAsync(string username, string password);
        string GenerateJwtToken(User user);
    }
}
