using Mapster;
using Reservation.Application.Interfaces;
using Reservation.Application.Interfaces.User;
using Reservation.Application.Models.Util;
using Reservation.Domain.Interfaces;

namespace Reservation.Application.Services.Util
{
    public class UtilService : IUtilService
    {

        private readonly IUserService _userService;
        private readonly ISpaceRepository _spaceRepository;

        public UtilService(IUserService userService, ISpaceRepository spaceRepository)
        {
            _userService = userService;
            _spaceRepository = spaceRepository;
        }

        public async Task<List<SpaceSelectDto>> GetSpaces()
        {
            var spaces = await _spaceRepository.GetAll();

            return spaces.Adapt<List<SpaceSelectDto>>();
        }

        public async Task<List<UserSelectDto>> GetUsers()
        {
            var users = await _userService.GetAll();

            return users.Adapt<List<UserSelectDto>>();
        }
    }
}
