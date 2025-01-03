using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Interfaces.Space;
using Reservation.Application.Models.Space;

namespace ReservationsAPI.Controllers.Space
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SpacesController : ControllerBase
    {
        private readonly ISpaceService _spaceService;

        public SpacesController(ISpaceService spaceService)
        {
            _spaceService = spaceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ApiGenericResponse<List<SpaceDto>>.GenericResponse(await _spaceService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpaceDto payload)
        {
            await _spaceService.AddAsync(payload);
            return ApiGenericResponse<object>.GenericResponseVoid(statusCode: 201);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _spaceService.DeleteAsync(id);
            return ApiGenericResponse<object>.GenericResponseVoid();
        }
    }
}
