using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Interfaces;
using Reservation.Infrastructure.Context;

namespace Reservation.Infrastructure.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly ReservationDbContext _dbContext;

        public UserRepository(ReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(Domain.Entities.User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Domain.Entities.User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<Domain.Entities.User?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _dbContext.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }

        public async Task<Domain.Entities.User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Domain.Entities.User?> GetById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
    }
}
