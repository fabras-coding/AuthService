using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Domain.Entities;
using AuthService.Domain.Repositories;
using AuthService.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _context;

        public UserRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(JWTUser user)
        {
            await _context.JWTUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<JWTUser?> GetUserByUsernameAsync(string username)
        {
            return await _context.JWTUsers.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}