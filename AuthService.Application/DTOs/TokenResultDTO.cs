using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Application.DTOs
{
    public class TokenResultDTO
    {
        
        public string AppId { get; set; } = Guid.NewGuid().ToString();
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}