using Reservation.Domain.Entities;

namespace Reservation.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetById(int id);
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
    }
}
