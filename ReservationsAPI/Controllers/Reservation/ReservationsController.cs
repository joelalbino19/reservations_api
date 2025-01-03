using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Interfaces.Reservation;
using Reservation.Application.Models.Reservation;

namespace ReservationsAPI.Controllers.Reservation
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ReservationsParams parameters)
        {
            return ApiGenericResponse<List<ReservationDto>>.GenericResponse(await _reservationService.GetAll(parameters));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationDto reservation)
        {
            await _reservationService.Create(reservation);
            return ApiGenericResponse<object>.GenericResponse(statusCode: 201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            await _reservationService.Cancel(id);
            return ApiGenericResponse<object>.GenericResponse();
        }
    }
}
