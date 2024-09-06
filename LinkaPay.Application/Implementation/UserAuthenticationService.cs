using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LinkaPay.Application.Interface;
using LinkaPay.Application.Security;
using LinkaPay.Application.ServiceModels.Requests;
using LinkaPay.Application.ServiceModels.Responses;
using LinkaPay.Domain.Entities;
using LinkaPay.Domain.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LinkaPay.Application.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IRepository<Users> _userRepository;
        private readonly IConfiguration _configuration;

        public UserAuthenticationService(IRepository<Users> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<Tuple<string, string, ErrorResponse>> Authenticate(UserLoginRequest model)
        {
            var user = await _userRepository.GetUserByEmail(model.Email);

            if (user == null || !PasswordHasher.VerifyPassword(model.Password, user.HashedPassword))
            {
                return new Tuple<string, string, ErrorResponse>(
                    null,
                    null,
                    new ErrorResponse { ErrorMessage = "Invalid email or password" }
                );
            }

            var token = GenerateJwtToken(user);

            return new Tuple<string, string, ErrorResponse>(token, user.EmailAddress, null);
        }

        private string GenerateJwtToken(Users user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["TokenSettings:Issuer"],
                audience: _configuration["TokenSettings:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
