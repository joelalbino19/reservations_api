using Reservation.Application.Models.Auth;

namespace Reservation.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<SignInDto> SignIn(SignInPayloadDto signIn);
        Task<SignInDto> SignUp(SignUpPayloadDto signUp);
    }
}
