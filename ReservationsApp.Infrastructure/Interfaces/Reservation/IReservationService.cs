using Reservation.Application.Models.Reservation;

namespace Reservation.Application.Interfaces.Reservation
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAll(ReservationsParams parameters);
        Task Create(CreateReservationDto reservation);
        Task Cancel(int id);
    }
}
