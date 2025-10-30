using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Domain.Entities;

namespace AuthService.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<JWTUser?> GetUserByUsernameAsync(string username);
        Task AddAsync(JWTUser user);
    }
}