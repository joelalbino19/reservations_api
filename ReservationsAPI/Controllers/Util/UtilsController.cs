using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Interfaces;
using Reservation.Application.Models.Util;

namespace ReservationsAPI.Controllers.Utils
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UtilsController : ControllerBase
    {
        private readonly IUtilService _utilService;

        public UtilsController(IUtilService utilService)
        {
            _utilService = utilService;
        }

        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> GetUsers()
        {
            return ApiGenericResponse<List<UserSelectDto>>.GenericResponse(await _utilService.GetUsers());
        }

        [HttpGet]
        [Route("Spaces")]
        public async Task<IActionResult> GetSpaces()
        {
            return ApiGenericResponse<List<SpaceSelectDto>>.GenericResponse(await _utilService.GetSpaces());
        }
    }
}
