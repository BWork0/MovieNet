using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces;
using MovieNet.Application.Interfaces.Services;

namespace MovieNet.Application.Queries
{
    public class GetUserProfileQueryHandler
        : IQueryHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly IUserProfileService _profileService;

        public GetUserProfileQueryHandler(IUserProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<UserProfileDto> Handle(GetUserProfileQuery query)
        {
            return await _profileService.GetUserProfile(query.UserGuid);
        }
    }

}
