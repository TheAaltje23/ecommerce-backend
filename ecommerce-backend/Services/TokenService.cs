using ecommerce_backend.Exceptions;
using ecommerce_backend.Interfaces;
using ecommerce_backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ecommerce_backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            // Configuration settings
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? throw new NotFoundException("Jwt issuer not found.");
            var jwtAudience = _configuration["Jwt:Audience"] ?? throw new NotFoundException("Jwt audience not found.");
            var jwtKey = _configuration["Jwt:Key"] ?? throw new NotFoundException("Jwt key not found.");
            var jwtExpirationInMinutes = int.Parse(_configuration["Jwt:ExpirationInMinutes"] ?? "60");

            // Claims settings
            var identityClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Sub, user.Username),
                new(ClaimTypes.Role, user.UserRole.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(identityClaims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(jwtExpirationInMinutes),
                Issuer = jwtIssuer,
                Audience = jwtAudience,
                SigningCredentials = creds
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }
    }
}