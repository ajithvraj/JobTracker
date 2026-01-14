using JobTracker.Application.Common;
using JobTracker.Application.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTracker.Application.Auth
{
    public interface IAuthService
    {

        Task RegisterAsync(RegisterRequestDto dto);
        Task VerifyEmailAsync(VerifyEmailRequestDto dto); 
        Task<AuthResponseDto?>LoginAsync(LoginRequestDto dto);




    }
}
