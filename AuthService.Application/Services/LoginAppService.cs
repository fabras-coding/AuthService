using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Application.Interfaces;
using AuthService.Application.DTOs;
using AuthService.Domain.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;



namespace AuthService.Application.Services
{
    public class LoginAppService : ILoginAppService
    {

        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginAppService(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<string> AuthenticateAsync(LoginDTO loginDto)
        {
          
            var user = _userRepository.GetUserByUsernameAsync(loginDto.Username).Result;

            if (user == null || user.Password != loginDto.Password)
                throw new UnauthorizedAccessException("User or password invalid.");
            else
            {
                var token = await _authService.GenerateTokenAsync(user);
                return token;
            }

        }
    }
}