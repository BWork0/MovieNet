using MovieNet.Application.DTOs;

namespace MovieNet.Application.Interfaces.Services
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> GetUserProfile(Guid userGuid);
        Task UpdateUserProfile(EditUserProfileDto dto);
    }
}
