using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Application.Interfaces;
using AuthService.Application.DTOs;
using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using AutoMapper;



namespace AuthService.Application.Services
{
    public class LoginAppService : ILoginAppService
    {

        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public LoginAppService(IAuthService authService, IUserRepository userRepository, IMapper mapper)
        {
            _authService = authService;
            _userRepository = userRepository;
            _mapper = mapper;

        }

        public async Task<TokenResultDTO> AuthenticateAsync(LoginDTO loginDto)
        {

            var jwtUser = _userRepository.GetUserByUsernameAsync(loginDto.Username).Result;
            
            if (jwtUser == null || jwtUser.Password != loginDto.Password)
                throw new UnauthorizedAccessException("User or password invalid.");

            var user = _mapper.Map<JWTUserDTO>(jwtUser);

            if (user == null || user.Password != loginDto.Password)
                throw new UnauthorizedAccessException("User or password invalid.");
            else
            {
                return await _authService.GenerateTokenAsync(user);
            }

        }
    }
}