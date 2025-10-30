using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class JWTUser
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = Guid.NewGuid().ToString();
        public string Password { get; set; } = Guid.NewGuid().ToString();
        public List<string> Roles { get; set; } = new();
        
    }
}