using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs.AuthDTOs
{
    public class AuthResponseDto
    {

        public string Token { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}

