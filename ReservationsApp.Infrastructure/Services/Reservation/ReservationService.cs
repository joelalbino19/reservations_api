using Reservation.Application.CustomExceptions;
using Reservation.Application.Interfaces.Reservation;
using Reservation.Application.Interfaces.User;
using Reservation.Application.Models.Reservation;
using Reservation.Domain.Entities;
using Reservation.Domain.Interfaces;

namespace Reservation.Application.Services.Reservation
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserService _userService;

        public ReservationService(IReservationRepository reservationRepository, IUserService userService)
        {
            _reservationRepository = reservationRepository;
            _userService = userService;
        }

        public async Task Cancel(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id) ?? throw new ConflictException($"No existe una reservación con Id: {id}", "id");

            await _reservationRepository.DeleteAsync(reservation);
        }

        public async Task Create(CreateReservationDto reservation)
        {
            var reservationValidation = await _reservationRepository.GetBySpaceAndTimeAsync(reservation.SpaceId, reservation.StartDate, reservation.EndDate);

            if (reservationValidation != null)
            {
                throw new ConflictException("Ya existe una reservación para este espacio en las fechas seleccionadas", "SpaceId, StartDate, EndDate");
            }

            var newReservation = new Domain.Entities.Reservation
            {
                SpaceId = reservation.SpaceId,
                UserId = reservation.UserId,
                StartTime = reservation.StartDate,
                EndTime = reservation.EndDate,
            };

            await _reservationRepository.AddAsync(newReservation);
        }

        public async Task<List<ReservationDto>> GetAll(ReservationsParams parameters)
        {
            var reservationList = await _reservationRepository.GetAllWithFiltersAsync(parameters.UserId, parameters.SpaceId, parameters.StartDate, parameters.EndDate);

            return reservationList.Select(item => new ReservationDto
            {
                Id = item.Id,
                UserName = item.User.Name,
                SpaceName = item.Space.Name,
                EndDate = item.EndTime.ToString("yyyy/MM/dd"),
                StartDate = item.StartTime.ToString("yyyy/MM/dd"),
            }).ToList();
        }
    }
}
