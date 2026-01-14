using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.DTOs.AuthDTOs
{
    public class VerifyEmailRequestDto
    {

        public string Email { get; set; }
        public string Token { get; set; }


    }
}
