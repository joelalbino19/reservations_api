using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Interfaces;
using Reservation.Infrastructure.Context;

namespace Reservation.Infrastructure.Repository.Reservation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationDbContext _dbContext;

        public ReservationRepository(ReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Domain.Entities.Reservation reservation)
        {
            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Domain.Entities.Reservation?> GetByIdAsync(int id)
        {
            return await _dbContext.Reservations.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Domain.Entities.Reservation reservation)
        {
            _dbContext.Reservations.Remove(reservation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Domain.Entities.Reservation>> GetAllWithFiltersAsync(int? userId, int? spaceId, DateTime? startDate, DateTime? endDate)
        {
            return await _dbContext.Reservations
                        .Include(r => r.User)
                        .Include(r => r.Space)
                        .Where(r => !userId.HasValue || r.UserId == userId.Value)
                        .Where(r => !spaceId.HasValue || r.SpaceId == spaceId.Value)
                        .Where(r => !startDate.HasValue || r.StartTime >= startDate.Value)
                        .Where(r => !endDate.HasValue || r.EndTime <= endDate.Value)
                        .ToListAsync();
        }


        public async Task<Domain.Entities.Reservation?> GetBySpaceAndTimeAsync(int spaceId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Reservations
                .Where(r => r.SpaceId == spaceId && r.StartTime < endDate && r.EndTime > startDate)
                .FirstOrDefaultAsync();
        }
    }
}
