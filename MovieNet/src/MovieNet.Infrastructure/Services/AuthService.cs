using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces.Services;
using MovieNet.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieNet.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly MovieNetContext _context;
        private readonly IConfiguration _config;

        public AuthService(MovieNetContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<bool> CheckUserExists(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var user = new User(
                initialPassword: dto.Password,
                userName: dto.UserName,
                email: dto.Email,
                createdAt: DateOnly.FromDateTime(DateTime.UtcNow),
                birthday: dto.Birthday,
                role: Role.User
            );

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null || !user.CheckPassword(password))
            {
                return null;
            }

            return user;
        }

        public string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"] ?? "ReplaceWithSomethingSecure");
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Guid.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = issuer,
                Audience = audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
