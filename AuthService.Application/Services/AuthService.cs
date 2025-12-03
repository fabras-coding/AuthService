using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Domain.Entities;
using AuthService.Application.Interfaces;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AuthService.Application.DTOs;

namespace AuthService.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TokenResultDTO> GenerateTokenAsync(JWTUserDTO user)
        {

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("app-id", user.Id.ToString()),
                new Claim("roles", string.Join(",", (user.Roles ?? new List<string>()).Select(r => r.Trim())))
                
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(5); //It should be UTCNow because the JWT middleware uses UTC time to validate the token expiration

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenResultDTO
            {
                AppId = user.Id.ToString(),
                Token = tokenString,
                Expiration = expires,
                Roles = claims[2].Value.Split(',').ToList()
            };
            

        }

    }
}