using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Application.DTOs
{
    public class JWTUserDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = Guid.NewGuid().ToString();
        public string Password { get; set; } = Guid.NewGuid().ToString();
        public List<string> Roles { get; set; } = new();
    }
}