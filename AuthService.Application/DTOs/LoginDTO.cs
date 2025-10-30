using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Application.DTOs
{
    public class LoginDTO
    {
        [Required]
        [MaxLength(36)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(36)]
        public string Password { get; set; } = string.Empty;
        
        public List<string> Roles { get; set; } = new List<string>();
    }
}