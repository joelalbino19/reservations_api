using Reservation.Application.Models.User.Dto;

namespace Reservation.Application.Interfaces.User
{
    public interface IUserService
    {
        Task<UserDto> Create(UserDto user);
        Task<UserDto?> GetByEmailAndPassword(string email, string password);
        Task<UserDto> GetById(int id);
        Task<List<UserDto>> GetAll();
    }
}
