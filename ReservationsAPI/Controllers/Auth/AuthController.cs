using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Interfaces.Auth;
using Reservation.Application.Models.Auth;

namespace ReservationsAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInPayloadDto signIn)
        {
            return ApiGenericResponse<SignInDto>.GenericResponse(await _authService.SignIn(signIn));
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpPayloadDto signUp)
        {
            return ApiGenericResponse<SignInDto>.GenericResponse(await _authService.SignUp(signUp));
        }
    }
}
