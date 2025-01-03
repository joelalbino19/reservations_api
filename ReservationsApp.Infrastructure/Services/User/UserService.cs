using Mapster;
using Reservation.Application.CustomExceptions;
using Reservation.Application.Interfaces.User;
using Reservation.Application.Models.User.Dto;
using Reservation.Domain.Interfaces;

namespace Reservation.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Create(UserDto user)
        {
            var validUser = await _userRepository.GetUserByEmailAsync(user.Email);

            if (validUser != null)
            {
                throw new ConflictException($"Ya existe un usuario con el Email: {user.Email}", "Email");
            }

            var newUser = user.Adapt<Domain.Entities.User>();
            await _userRepository.AddUserAsync(newUser);

            return newUser.Adapt<UserDto>();
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return users.Adapt<List<UserDto>>();
        }

        public async Task<UserDto?> GetByEmailAndPassword(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(email, password);

            var userDto = user.Adapt<UserDto>();

            return userDto;
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _userRepository.GetById(id) ?? throw new KeyNotFoundException($"No se encontró el usuario con id: {id}");

            return user.Adapt<UserDto>();
        }
    }
}
