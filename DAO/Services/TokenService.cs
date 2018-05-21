using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAO.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DAO.Services
{
    public class TokenService : ITokenService
    {
        private SigningCredentials _creds;
        private JwtSecurityTokenHandler _tokenHandler;

        public TokenService(IConfiguration config, JwtSecurityTokenHandler tokenHandler)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

            _creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            _tokenHandler = tokenHandler;
        }

        public TokenDto GenerateToken(UserDto user)
        {
            var expirationDate = DateTime.UtcNow.AddMinutes(2);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim("username", user.UserName),
                new Claim("id", user.Id.ToString(), ClaimValueTypes.Integer32),
                new Claim("name", user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expirationDate,
                signingCredentials: _creds);

            return new TokenDto {
                Token = _tokenHandler.WriteToken(token),
                ExpirationDate = expirationDate
            };
        }
    }
}
