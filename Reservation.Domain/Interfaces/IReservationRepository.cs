using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task<Entities.Reservation?> GetByIdAsync(int id);
        Task AddAsync(Entities.Reservation reservation);
        Task DeleteAsync(Entities.Reservation reservation);
        Task<List<Domain.Entities.Reservation>> GetAllWithFiltersAsync(int? userId = null, int? spaceId = null, DateTime? startDate = null, DateTime? endDate = null);
        Task<Entities.Reservation?> GetBySpaceAndTimeAsync(int spaceId, DateTime startDate, DateTime endDate);
    }
}
