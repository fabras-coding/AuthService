using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Application.DTOs;

namespace AuthService.Application.Interfaces
{
    public interface ILoginAppService
    {
        Task<string> AuthenticateAsync(LoginDTO loginDto);
    }
}