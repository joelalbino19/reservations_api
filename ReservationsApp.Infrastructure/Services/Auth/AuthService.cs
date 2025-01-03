using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Reservation.Application.Interfaces.Auth;
using Reservation.Application.Interfaces.User;
using Reservation.Application.Models.Auth;
using Reservation.Application.Models.User.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Reservation.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;


        public AuthService(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<SignInDto> SignIn(SignInPayloadDto signIn)
        {
            var user = await _userService.GetByEmailAndPassword(signIn.Email, signIn.Password)
                ?? throw new UnauthorizedAccessException("Credenciales invalidas, intente nuevamente"); ;

            string token = GenerateToken(user);

            return new SignInDto
            {
                Email = user.Email,
                Name = user.Name,
                Token = token
            };
        }

        public async Task<SignInDto> SignUp(SignUpPayloadDto signUp)
        {
            var user = new UserDto
            {
                Name = signUp.Name,
                Password = signUp.Password,
                Email = signUp.Email,
            };

            var userResult = await _userService.Create(user);

            var signInPayload = userResult.Adapt<SignInPayloadDto>();

            return await SignIn(signInPayload);
        }

        private string GenerateToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["jwt:Key"] ?? "");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString() ?? ""),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", user.Name),
                new Claim("userId", user.Id.ToString() ?? ""),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuration["jwt:Issuer"] ?? "",
                Audience = _configuration["jwt:Audience"] ?? "",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
