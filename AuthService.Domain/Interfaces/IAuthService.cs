using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateTokenAsync(JWTUser user);
    }
}