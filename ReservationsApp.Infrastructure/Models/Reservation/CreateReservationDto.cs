using Reservation.Application.Models.User.Dto;
using Reservation.Domain.Entities;

namespace Reservation.Application.Models.Reservation
{
    public record CreateReservationDto
    {
        public int UserId { get; set; }

        public int SpaceId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}