using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Interfaces;
using Reservation.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure.Repository.Space
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly ReservationDbContext _dbContext;

        public SpaceRepository(ReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Domain.Entities.Space>> GetAll()
        {
            return await _dbContext.Spaces.ToListAsync();
        }

        public async Task<Domain.Entities.Space?> GetById(int id)
        {
            return await _dbContext.Spaces.FindAsync(id);
        }

        public async Task AddAsync(Domain.Entities.Space space)
        {
            _dbContext.Spaces.Add(space);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Domain.Entities.Space space)
        {
            _dbContext.Spaces.Remove(space);
            await _dbContext.SaveChangesAsync();
        }
    }
}
