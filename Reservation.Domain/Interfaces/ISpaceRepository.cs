
namespace Reservation.Domain.Interfaces
{
    public interface ISpaceRepository
    {
        Task<List<Entities.Space>> GetAll();
    }
}
