using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAppMedicalAssistant_Core.DTO;
using WebAppMedicalAssistant_Data.CQS.Commands;
using WebAppMedicalAssistantAPI.Model.Responses;

namespace WebAppMedicalAssistantAPI.Utils
{
    public class JwtUtilSha256 : IJwtUtil
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public JwtUtilSha256(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        public async Task<TokenResponse> GenerateTokenAsync(UserDto? dto)
        {
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:JwtSecret"]));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var nowUtc = DateTime.Now;
                var exp = nowUtc.AddMinutes(double.Parse(_configuration["Token:ExpiryMinutes"]));

                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Email, dto?.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("D")), //jwt uniq id from spec
                    new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString("D")),
                    new Claim(ClaimTypes.Role, dto.RoleName),
                    new Claim(ClaimTypes.Name, dto.Email)
                };

                var jwtToken = new JwtSecurityToken(_configuration["Token:Issuer"],
                    _configuration["Token:Issuer"],
                    claims,
                    expires: exp,
                    signingCredentials: credentials);

                var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                var refreshTokenValue = Guid.NewGuid();

                await _mediator.Send(new AddRefreshTokenCommand()
                {
                    UserId = dto.Id,
                    TokenValue = refreshTokenValue
                });

                return new TokenResponse()
                {
                    AccessToken = accessToken,
                    Role = dto.RoleName,
                    TokenExpiration = jwtToken.ValidTo,
                    UserId = dto.Id,
                    RefreshToken = refreshTokenValue
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RemoveRefreshTokenAsync(Guid requestRefreshToken)
        {
            await _mediator.Send(new RemoveRefreshTokenCommand()
            {
                TokenValue = requestRefreshToken
            });
        }

    }
}
