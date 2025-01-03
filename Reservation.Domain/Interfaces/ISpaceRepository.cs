
namespace Reservation.Domain.Interfaces
{
    public interface ISpaceRepository
    {
        Task<List<Entities.Space>> GetAll();
        Task<Entities.Space?> GetById(int id);
        Task AddAsync(Entities.Space space);
        Task DeleteAsync(Entities.Space space);
    }
}
